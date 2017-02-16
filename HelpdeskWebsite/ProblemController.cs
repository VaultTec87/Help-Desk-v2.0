using System;
using System.Collections.Generic;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite
{
    public class ProblemController : ApiController
    {
        //Get all Problems
        [Route("api/problems")]
        public IHttpActionResult Get()
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                List<ProblemViewModel> allProblems = prob.GetAll();
                return Ok(allProblems);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        //Get Problem by id
        [Route("api/problems/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                prob.Id = id;
                prob.GetById();
                return Ok(prob);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + "error is in GetById");
            }
        }
        // delete selected Problem object
        [Route("api/problems/{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                ProblemViewModel vm = new ProblemViewModel();
                vm.Id = id;
                vm.GetById();
                long retVal = vm.Delete();
                switch (retVal)
                {
                    case 1:
                        return Ok("Problem deleted!");
                    case 0:
                        return Ok("Problem not deleted!");
                    default:
                        return Ok("Problem not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        // update selected Problem object
        [Route("api/problems")]
        public IHttpActionResult Put(ProblemViewModel prob)
        {
            try
            {
                int retVal = prob.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Problem " + prob.Description + " updated!");
                    case -1:
                        return Ok("Problem " + prob.Description + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + prob.Description + ", Problem not updated!");
                    default:
                        return Ok("Problem " + prob.Description + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        // create a new Problem object
        [Route("api/problems")]
        public IHttpActionResult Post(ProblemViewModel prob)
        {
            try
            {
                prob.Create();
                return Ok("Problem created");
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}