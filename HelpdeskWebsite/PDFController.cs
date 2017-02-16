using System;
using System.Diagnostics;
using System.Web.Http;

namespace ExercisesWebsite
{
    public class PDFController : ApiController
    {
        [Route("api/helloreport")]
        public IHttpActionResult GetHelloReport()
        {
            try
            {
                Reports.HelloReport rep = new Reports.HelloReport();
                rep.do_it();
                return Ok("report generated");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("error " + ex.Message);
                return BadRequest("Retrieve Failed - couldnt generate sample report");
            }
        }
        [Route("api/callreport")]
        public IHttpActionResult GetCallReport()
        {
            try
            {
                Reports.CallReport rep = new Reports.CallReport();
                rep.do_it();
                return Ok("report generated");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("error " + ex.Message);
                return BadRequest("Retrieve Failed - couldnt generate sample report");
            }
        }
    }
}
