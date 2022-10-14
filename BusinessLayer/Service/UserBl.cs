using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBl : IUserBl

    {
        private readonly IUserRl userRl;

        public UserBl(IUserRl userRl)
        {
            this.userRl =  userRl;
        }

        public UserEntity UserRegisteration(Registeration registeration)
        {
            try
            {
                return userRl.UserRegisteration(registeration);

            }
            catch (Exception)
            {

                throw;
            }
        }
    public string LoginUser(Login login)
        {
            try
            {
                return userRl.LoginUser(login);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string JWTToken(string email, long userId)
        {
            try
            {
                return this.userRl.JWTToken(email, userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string ForgetPassword(string email)
        {
            try
            {
                return this.userRl.ForgetPassword(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ResetPassword(string email,string password,string confirmPassword)
        {
            try
            {
                return userRl.ResetPassword(email,password, confirmPassword);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

    }

}
