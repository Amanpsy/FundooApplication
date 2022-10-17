﻿using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundooApplication.Controllers
{


    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
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
        public IActionResult GetAllLabel()
        {
            try
            {
                long userId= Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = this.labelBL.GetAllLabel(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label Fetched Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Fetch Label" });
                }

              
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
