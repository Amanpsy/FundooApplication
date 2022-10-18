using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
     

        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL noteBL, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.distributedCache = distributedCache;
        }
    
        [HttpPost("CreateNote")]
        public IActionResult CreateNote(Note createNote)
        {
            try
            {

                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = this.noteBL.CreateNote(userId, createNote);
                if(result != null)
                {
                    return this.Ok(new { sucess = true, message = "Note is sucessfully created.", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Note Creation is Unsucessfull." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("GetNote")]
        public async Task<IActionResult> GetNote()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var cachekey = Convert.ToString(userId);
                string serializeddata;
                List<NoteEntity> result;
                var distcacheresult = await distributedCache.GetAsync(cachekey);
                if (distcacheresult != null)
                {
                    serializeddata = Encoding.UTF8.GetString(distcacheresult);
                    result = JsonConvert.DeserializeObject<List<NoteEntity>>(serializeddata);

                    return this.Ok(new { success = true, message = "Note Data fetch Successfully", data = result });

                }
                else
                {
                    var userdata = noteBL.GetNote(userId);
                    serializeddata = JsonConvert.SerializeObject(userdata);
                    distcacheresult = Encoding.UTF8.GetBytes(serializeddata);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                    await distributedCache.SetAsync(cachekey, distcacheresult, options);

                    if (userdata != null)
                    {

                        return this.Ok(new { success = true, message = "Note Data fetch Successfully", data = userdata });
                    }
                    else
                    {

                        return this.BadRequest(new { success = false, message = "Not able to fetch notes" });
                    }
                }
                }
            catch (Exception ex)
            {

                throw ex;
            }

         
              
        }
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote(long noteId,Note note)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var userdata = noteBL.UpdateNote(userId, noteId, note);
                if (userdata != null)
                {
                    return this.Ok(new { success = true, message = "Note updated Successfull", data = userdata });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to update note" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpDelete("DeleteNote")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var userdata= noteBL.DeleteNote(userId, noteId);
                if (userdata == true)
                {
                    return this.Ok(new { success = true, message = "Deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to delete the note." });
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("Pin")]
        public IActionResult PinnedNote(long noteId)
        {
            try
            {
                
                var result = this.noteBL.PinnedNote(noteId);
                if (result.Pinned ==true)
                {
                    return this.Ok(new { success = true, message = "Note is successfully pinned.", data = result });
                }
                else if(result.Pinned ==false)
                {
                    return this.Ok(new { success = true, message = "Note is unpinned sucessfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to pin your note." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("Archieve")]
        public IActionResult ArchieveNote(long noteId)
        {
            try
            {
                
                var userdata= noteBL.ArchieveNote(noteId);
                if (userdata.Archive == true)
                {
                    return this.Ok(new { success = true, message = "Archieved successfully", data = userdata });
                }

                else if(userdata.Archive == false)
                {
                    return this.Ok(new { success = true, message = "UnArchieved successfully", data = userdata });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Archieve Operation failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("Trash")]
        public IActionResult Trash(long noteId)
        {
            try
            {
             
                var result= noteBL.Trash(noteId);
                if(result.Trash== true)
                {
                    return this.Ok(new { success = true, message = "Note is  Trash successfully.", data = result });
                }
                else if(result.Trash== false)
                {
                    return this.Ok(new { success = true, message = "Note is untrashed", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to trash the note." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("Colour")]
        public IActionResult NoteColour(long noteId, string Color)
        {
            try
            {
                var result = this.noteBL.NoteColor(noteId, Color);
                if (result != null)
                {

                    return this.Ok(new { success = true, message = "Note colour changed successfully.", data = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Colour changing of note is denied." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("Image")]
        public IActionResult Image(long noteId, IFormFile file)
        {
            try
            {
               
                var result = noteBL.Image( noteId, file); 
                if(result !=null)
                {
                    return this.Ok(new { success = true, message = "Image uploaded successfully", data = result });
                }
              
                else
                {
                    return this.BadRequest(new { success = false, message = "Image upload Operation failed", data = result }); ;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
