using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using System.Collections.Generic;
using System.Text;

namespace FundooApplication.Controllers
{


    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBL labelBL, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.distributedCache = distributedCache;
        }
        [HttpPost("CreateLabel")]
        public IActionResult CreateLabel(long UserId, long NoteId, string LabelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result=this.labelBL.CreateLabel(userId, NoteId, LabelName);
                if(result!= null)
                {
                    return this.Ok(new { success = true, message = "Label is created Successfully", data = result });
                }
                else
                {

                    return this.BadRequest(new { success = false, message = "Unable to Label note" });
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("GetAllLabel")]
        public async Task <IActionResult> GetAllLabel()
        {
            try
            {
                long userId= Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var cachekey = "LabelList";
                string serializedLabelList;
                var labelResult = labelBL.GetAllLabel(userId);

                var redisLabelList = await distributedCache.GetAsync(cachekey);
                //var result = this.labelBL.GetAllLabel(userId);
                if (redisLabelList != null)
                {
                    serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                    labelResult = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
                }
                else
                {
                    serializedLabelList = JsonConvert.SerializeObject(labelResult);
                    redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await distributedCache.SetAsync(cachekey, redisLabelList, options);
                }
                return this.Ok(new { success = true, message = "All Label Fetched Successfully", data = labelResult });


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("UpdateLabel")]
        public IActionResult UpdateLabel(long noteId, long labelId, string labelName)
        {
            try
            {
                var result = this.labelBL.UpdateLabel(noteId, labelId, labelName);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Update Label" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("DeleteLabel")]
        public IActionResult DeleteLabel(string labelName)
        {
            try
            {
                var result= this.labelBL.DeleteLabel(labelName);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Label Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Delete Label note" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
