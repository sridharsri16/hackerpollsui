using Api.BizService;
using Api.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CandidateController : ApiController
    {
        // GET: api/Candidate
        public List<Userdetails> Get()
        {
            CandidateService candidateService = new CandidateService();
            var data = candidateService.Getdetails();
            return data;
        }

        // GET: api/Candidate/5
        public Userdetails Get(int id)
        {
            CandidateService candidateService = new CandidateService();
            var isexists = candidateService.Getdetailbyid(id);
            return isexists;
        }

        // POST: api/Candidate
        public bool Post([FromBody] List<Userdetails> userdetails)
        {
            CandidateService candidateService = new CandidateService();
            var isexists = candidateService.Adddetails(userdetails);
            return isexists;
        }

        // PUT: api/Candidate/5
        public bool Put(int id, [FromBody] Userdetails value)
        {
            CandidateService candidateService = new CandidateService();
            var isexists = candidateService.Editdetails(value);
            return isexists;
        }

        // DELETE: api/Candidate/5
        public bool Delete(int id)
        {
            CandidateService candidateService = new CandidateService();
            var isexists = candidateService.Deletebyid(id);
            return isexists;
        }
    }
}
