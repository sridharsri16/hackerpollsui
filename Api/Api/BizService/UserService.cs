using Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Api.BizService
{
    public class UserService
    {
        public Userdetails Autheticate(Authenticate authentication)
        {
            //convert response to object
            //C: \Users\Lenovo\source\repos\Api\Api\Data\user.json
            string sFileName = HttpContext.Current.Server.MapPath(@"~/Data/user.json");
            string json = File.ReadAllText(sFileName);
            List<Authenticate> records = JsonConvert.DeserializeObject<List<Authenticate>>(json);

            var isexists = records.Where(x => x.username.ToLower() == authentication.username.ToLower() && x.password.ToLower() == authentication.password.ToLower()).FirstOrDefault();

            if (isexists != null)
            {
                //file path and count
                string scFileName = HttpContext.Current.Server.MapPath(@"~/Data/candidate.json");
                string jsonc = File.ReadAllText(scFileName);
                List<Userdetails> crecords = JsonConvert.DeserializeObject<List<Userdetails>>(jsonc);
                var data = crecords.Where(x => x.candidateid == isexists.userid).FirstOrDefault();
                data.algorithm = data.algorithm == null ? 0 : data.algorithm;
                data.ds = data.ds == null ? 0 : data.ds;
                data.c = data.c == null ? 0 : data.c;
                data.java = data.java == null ? 0 : data.java;
                data.phyton = data.phyton == null ? 0 : data.phyton;
                data.expertlevel = data.expertlevel == null ? 0 : data.expertlevel;
                data.challengessolved = data.challengessolved == null ? 0 : data.challengessolved;
                return data;
            }
            else

                return new Userdetails();
        }
    }

}