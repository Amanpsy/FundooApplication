using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRl
    {
        public UserEntity UserRegisteration(Registeration registeration);
        public string LoginUser(Login login);
        public string ForgetPassword(string email);


    }
}
