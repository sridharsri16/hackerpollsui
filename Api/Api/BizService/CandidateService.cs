using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Api.BizService
{
    public class CandidateService
    {
        public bool Adddetails(List<Userdetails> userdetails)
        {
            try
            {
                int? lastid;
                //for user details insert 
                List<Authenticate> insertdata = new List<Authenticate>();
                //file path and count
                string sUFileName = HttpContext.Current.Server.MapPath(@"~/Data/user.json");
                string jsondata = File.ReadAllText(sUFileName);
                insertdata = JsonConvert.DeserializeObject<List<Authenticate>>(jsondata);
                if (insertdata.Count == 0)
                {
                    lastid = 0;
                }
                else
                {
                    lastid = insertdata.LastOrDefault().userid;
                }

                //file path and count
                string sFileName = HttpContext.Current.Server.MapPath(@"~/Data/candidate.json");
                string json = File.ReadAllText(sFileName);
                List<Userdetails> records = JsonConvert.DeserializeObject<List<Userdetails>>(json);
                userdetails[0].candidateid = lastid + 1;
                //if not zero
                try
                {
                    if (records == null || records.Count == 0)
                    {
                        File.WriteAllText(sFileName, JsonConvert.SerializeObject(userdetails));
                    }
                    else
                    {
                        records.AddRange(userdetails);
                        var res = JsonConvert.SerializeObject(records, Formatting.Indented);
                        File.WriteAllText(sFileName, res);
                    }
                }
                catch (Exception e) { return false; }

                Authenticate authenticate = new Authenticate();
                authenticate.username = userdetails[0].name;
                authenticate.password = userdetails[0].password;
                authenticate.userid = userdetails[0].candidateid;
                insertdata.Add(authenticate);
                var usrres = JsonConvert.SerializeObject(insertdata, Formatting.Indented);
                File.WriteAllText(sUFileName, usrres);
            }
            catch (Exception e) { return false; }
            return true;
        }

        public List<Userdetails> Getdetails()
        {
            string sFileName = HttpContext.Current.Server.MapPath(@"~/Data/candidate.json");
            string json = File.ReadAllText(sFileName);
            List<Userdetails> records = JsonConvert.DeserializeObject<List<Userdetails>>(json);
            if (records.Count > 0 || records != null)
            {
                foreach (var data in records)
                {
                    data.algorithm = data.algorithm == null ? 0 : data.algorithm;
                    data.ds = data.ds == null ? 0 : data.ds;
                    data.c = data.c == null ? 0 : data.c;
                    data.java = data.java == null ? 0 : data.java;
                    data.phyton = data.phyton == null ? 0 : data.phyton;
                    data.expertlevel = data.expertlevel == 0 ? 0 : data.expertlevel;
                    data.challengessolved = data.challengessolved == 0 ? 0 : data.challengessolved;
                }
                return records;
            }
            else
            {
                return new List<Userdetails>();
            }
        }

        public Userdetails Getdetailbyid(int id)
        {
            string sFileName = HttpContext.Current.Server.MapPath(@"~/Data/candidate.json");
            string json = File.ReadAllText(sFileName);
            List<Userdetails> records = JsonConvert.DeserializeObject<List<Userdetails>>(json);
            var data = records.Where(x => x.candidateid == id).FirstOrDefault();
            data.algorithm = data.algorithm == null ? 0 : data.algorithm;
            data.ds = data.ds == null ? 0 : data.ds;
            data.c = data.c == null ? 0 : data.c;
            data.java = data.java == null ? 0 : data.java;
            data.phyton = data.phyton == null ? 0 : data.phyton;
            data.expertlevel = data.expertlevel == null ? 0 : data.expertlevel;
            data.challengessolved = data.challengessolved == null ? 0 : data.challengessolved;
            return data;
        }

        public bool Deletebyid(int id)
        {
            string sFileName = HttpContext.Current.Server.MapPath(@"~/Data/candidate.json");
            string json = File.ReadAllText(sFileName);
            List<Userdetails> records = JsonConvert.DeserializeObject<List<Userdetails>>(json);
            var data = records.Where(x => x.candidateid == id).FirstOrDefault();
            var datatobedeleted = records.Remove(data);

            //user data removal 
            List<Authenticate> insertdata = new List<Authenticate>();
            string sUFileName = HttpContext.Current.Server.MapPath(@"~/Data/user.json");
            string jsondata = File.ReadAllText(sUFileName);
            insertdata = JsonConvert.DeserializeObject<List<Authenticate>>(jsondata);
            var usrdata = insertdata.Where(x => x.userid == id).FirstOrDefault();
            var usrdatatobedeleted = insertdata.Remove(usrdata);


            if (datatobedeleted && usrdatatobedeleted)
            {
                var res = JsonConvert.SerializeObject(records, Formatting.Indented);
                File.WriteAllText(sFileName, res);

                var usrres = JsonConvert.SerializeObject(insertdata, Formatting.Indented);
                File.WriteAllText(sUFileName, usrres);
            }
            return datatobedeleted == true && usrdatatobedeleted == true ? true : false;
        }

        public bool Editdetails(Userdetails details)
        {
            try
            {
                string sFileName = HttpContext.Current.Server.MapPath(@"~/Data/candidate.json");
                string json = File.ReadAllText(sFileName);
                List<Userdetails> records = JsonConvert.DeserializeObject<List<Userdetails>>(json);
                var id = records.FindIndex(x => x.candidateid == details.candidateid);
                records[id].candidateid = details.candidateid;
                records[id].name = details.name;
                records[id].password = details.password;
                records[id].expertlevel = details.expertlevel;
                records[id].c = details.c;
                records[id].ds = details.ds;
                records[id].algorithm = details.algorithm;
                records[id].phyton = details.phyton;
                records[id].java = details.java;

                //user data removal 
                List<Authenticate> insertdata = new List<Authenticate>();
                string sUFileName = HttpContext.Current.Server.MapPath(@"~/Data/user.json");
                string jsondata = File.ReadAllText(sUFileName);
                insertdata = JsonConvert.DeserializeObject<List<Authenticate>>(jsondata);
                var usrid = insertdata.FindIndex(x => x.userid == details.candidateid);
                insertdata[usrid].userid = details.candidateid;
                insertdata[usrid].username = details.name;
                insertdata[usrid].password = details.password;

                var res = JsonConvert.SerializeObject(records, Formatting.Indented);
                File.WriteAllText(sFileName, res);

                var usrres = JsonConvert.SerializeObject(insertdata, Formatting.Indented);
                File.WriteAllText(sUFileName, usrres);
            }
            catch (Exception e) { return false; }

            return true;

        }
    }
}