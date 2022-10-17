using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Permissions;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost("CreateCollab")]
        public IActionResult CreateCollab(long UserID, long NoteId, Collaborator collaborator)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = this.collabBL.CreateCollab(UserID, NoteId, collaborator);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collaborated Successfully", Response = result });

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

        [HttpGet("GetCollab")]
        public IActionResult GetAllCollab()
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result= this.collabBL.GetAllCollab(UserID);
                if(result != null)
                {
                    return this.Ok(new { Success = true, message = "All Collab are fetched Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to show the collaborates" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("DeleteCollaborator")]
        public IActionResult DeleteCollab(long collabId)
        {
            var result= this.collabBL.DeleteCollab(collabId);
            if (result)
            {
                return this.Ok(new { Success = true, message = "Deleted Successfully" });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Unable to delete" });
            }
        }
    }
}
