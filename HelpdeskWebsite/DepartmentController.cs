using System;
using System.Collections.Generic;
using System.Web.Http;
using HelpdeskViewModel;

namespace HelpdeskWebsite
{
    public class DepartmentController : ApiController
    {
        //Get all departments
        [Route("api/departments")]
        public IHttpActionResult Get()
        {
            try
            {
                DepartmentViewModel dep = new DepartmentViewModel();
                List<DepartmentViewModel> allDepartments = dep.GetAll();
                return Ok(allDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        //Get department by id
        [Route("api/departments/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DepartmentViewModel dep = new DepartmentViewModel();
                dep.Id = id;
                dep.GetById();
                return Ok(dep);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        // delete selected department object
        [Route("api/departments")]
        public IHttpActionResult Delete(DepartmentViewModel dep)
        {
            try
            {
                long retVal = dep.Delete();
                switch (retVal)
                {
                    case 1:
                        return Ok("Department " + dep.DepartmentName + " Deleted!");
                    case 0:
                        return Ok("Department " + dep.DepartmentName + " not deleted!");
                    default:
                        return Ok("Department " + dep.DepartmentName + " not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        // update selected department object
        [Route("api/departments")]
        public IHttpActionResult Put(DepartmentViewModel dep)
        {
            try
            {
                int retVal = dep.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Department " + dep.DepartmentName + " updated!");
                    case -1:
                        return Ok("Department " + dep.DepartmentName + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + dep.DepartmentName + ", Department not updated!");
                    default:
                        return Ok("Department " + dep.DepartmentName + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        // create a new department object
        [Route("api/departments")]
        public IHttpActionResult Post(DepartmentViewModel dep)
        {
            try
            {
                dep.Create();
                return Ok("Department " + dep.DepartmentName + " created");
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}