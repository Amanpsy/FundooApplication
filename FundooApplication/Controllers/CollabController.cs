using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Permissions;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [HttpPost("Create")]
        public IActionResult CreateCollab(long NoteId, Collaborator collaborator)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result=this.collabBL.CreateCollab(userId,NoteId,collaborator);
                if(result!=null)
                {
                    return this.Ok(new { Success = true, message = "Collaborated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to collaborate note" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
