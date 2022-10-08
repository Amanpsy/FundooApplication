using BusinessLayer.Interface;
using CommonLayer.Model;
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


    }

}
