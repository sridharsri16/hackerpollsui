using Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {

        // POST api/values
        public Userdetails Post([FromBody] Vote votedetail)
        {
            //file path and count
            string sFileName = HttpContext.Current.Server.MapPath(@"~/Data/candidate.json");
            string json = File.ReadAllText(sFileName);
            List<Userdetails> records = JsonConvert.DeserializeObject<List<Userdetails>>(json);
            var id = records.FindIndex(x => x.candidateid == votedetail.votedfor);
            records[id].votecount = records[id].votecount == null ? 0 : records[id].votecount;
            records[id].votecount = records[id].votecount + 1;
            var id1 = records.FindIndex(x => x.candidateid == votedetail.whovoted);
            records[id1].voted = true;
            var res = JsonConvert.SerializeObject(records, Formatting.Indented);
            File.WriteAllText(sFileName, res);

            return records.Where(x => x.candidateid == votedetail.whovoted).FirstOrDefault();
        }
    }
}
