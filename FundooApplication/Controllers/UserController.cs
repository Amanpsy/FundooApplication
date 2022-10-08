using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBl UserBl;
        public UserController(IUserBl userBl)
        {
            UserBl = userBl;
        }
        [HttpPost("Register")]
        public IActionResult Registeration(Registeration registeration)
        {
            try
            {
                var result = UserBl.UserRegisteration(registeration);
                if (result != null)
                {
                    return this.Ok(new {sucess=true, message="User is sucessfully registered", data=result});
                }

                else
                {
                    return this.BadRequest(new { sucess = false, message = "User registeration is unsucessful"});
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpPost("Login")]
        public IActionResult LoginUser(Login login)
        {
            try
            {
                var result = this.UserBl.LoginUser(login);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Login Sucessfull.", data = result });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Login Unsucessfull. Email or password is Invalid." });
                }

            }
            catch (System.Exception)
            {

                throw;
            }

        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result=this.UserBl.ForgetPassword(email);
                if (result !=null)
                {
                    return this.Ok(new { sucess = true, message = "Password reset mail has sent sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Failed to send the email. Please enter registred email ID." });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
