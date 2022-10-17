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
    }
}