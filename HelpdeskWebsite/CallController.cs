using HelpdeskViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelpdeskWebsite
{
    public class CallController : ApiController
    {
        [Route("api/Calls")]
        public IHttpActionResult Get()
        {
            try
            {
                CallViewModel call = new CallViewModel();
                List<CallViewModel> allCalls = call.GetAll();
                return Ok(allCalls);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/Calls/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                CallViewModel call = new CallViewModel();
                call.Id = id;
                call.GetById();
                return Ok(call);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + "error in GetById()");
            }
        }


        [Route("api/Calls")]
        public IHttpActionResult Put(CallViewModel call)
        {
            try
            {
                int retVal = call.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Call " + call.Notes + " updated!");
                    case -1:
                        return Ok("Call " + call.Notes + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + call.Notes + ", Call not updated!");
                    default:
                        return Ok("Call " + call.Notes + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/Calls")]
        public IHttpActionResult Post(CallViewModel call)
        {
            try
            {
                call.Create();
                return Ok("Call " + call.Notes + " created");
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/Calls")]
        public IHttpActionResult Delete(CallViewModel call)
        {
            try
            {
                long retVal = call.Delete();
                switch (retVal)
                {
                    case 1:
                        return Ok("Call " + call.Notes + " Deleted!");
                    case -1:
                        return Ok("Call " + call.Notes + " not deleted!");
                    case -2:
                        return Ok("Data is stale for " + call.Notes + ", Call already deleted!");
                    default:
                        return Ok("Call " + call.Notes + " not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        [Route("api/Callname/{name}")]
        public IHttpActionResult GetByDescription(string name)
        {
            try
            {
                CallViewModel call = new CallViewModel();
                call.Notes = name;
                call.GetById();
                return Ok(call);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - Contact Tech Support");
            }
        }
    }
}
