using System;
using System.Web.Http;
using HelpdeskViewModels;
using System.Collections.Generic;

namespace HelpdeskWebsite.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET api/<controller>
        [Route("api/employees")]
        public IHttpActionResult Get()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Id = id;
                emp.GetById();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }


        [Route("api/employees")]
        public IHttpActionResult Put(EmployeeViewModel emp)
        {
            try
            {
                int retVal = emp.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " updated!");
                    case -1:
                        return Ok("Employee " + emp.Lastname + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ", Employee not updated!");
                    default:
                        return Ok("Employee " + emp.Lastname + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        public IHttpActionResult Post(EmployeeViewModel emp)
        {
            try
            {
                emp.Create();
                return Ok("Employee " + emp.Lastname + " created");
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        public IHttpActionResult Delete(EmployeeViewModel emp)
        {
            try
            {
                long retVal = emp.Delete();
                switch (retVal)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " Deleted!");
                    case -1:
                        return Ok("Employee " + emp.Lastname + " not deleted!");
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ", Employee already deleted!");
                    default:
                        return Ok("Employee " + emp.Lastname + " not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }


    }
}