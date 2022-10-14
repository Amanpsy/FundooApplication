using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBl userBL;
        public UserController(IUserBl userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Registeration(Registeration registeration)
        {
            try
            {
                var result = userBL.UserRegisteration(registeration);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "User is sucessfully registered", data = result });
                }

                else
                {
                    return this.BadRequest(new { sucess = false, message = "User registeration is unsucessful" });
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
                var result = this.userBL.LoginUser(login);
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
                var result = userBL.ForgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Password reset mail has sent sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Failed to send the email. Please enter registred email ID." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result= userBL.ResetPassword(email,password, confirmPassword);
                if (result)
                {
                    return this.Ok(new {sucess=true,message="Password is sucessfully reset"});

                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "password reset failed" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       
        }
    }
    

