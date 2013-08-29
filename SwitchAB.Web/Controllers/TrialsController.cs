using SwitchAB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwitchAB.Web.Controllers
{
    public class TrialsController : ApiController
    {
        public HttpResponseMessage UpdateTrials(List<Trial> trials)
        {
            if (trials != null && trials.Count() > 0)
            {
                if (ModelState.IsValid)
                {
                    //do some stuff
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);

        }

        public Trial GetTrialById(string id)
        {
            var trial = new Trial() { Id = id, TargetId = "2" };
            if (trial == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return trial;
        }
    }
}
