using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CodeWarrior.App.Controllers
{
    public class UploadController : ApiController
    {
        public IHttpActionResult Post(HttpPostedFile resource)
        {
            if (resource.ContentLength == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
