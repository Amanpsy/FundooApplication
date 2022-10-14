using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRl : IUserRl
    {
        private readonly FundooContext fundooContext;
        public IConfiguration Configuration { get; }

        public UserRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.Configuration = configuration;
        }


        public UserEntity UserRegisteration(Registeration registeration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registeration.FirstName;
                userEntity.LastName = registeration.LastName;
                userEntity.Email = registeration.Email;
                userEntity.Password = registeration.Password;
                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string LoginUser(Login login)
        {
            try
            {
                LoginEntity loginentity = new LoginEntity();
                var result = this.fundooContext.UserTable.Where(u => u.Email == login.Email && u.Password == login.Password).FirstOrDefault();
                if (result != null)
                {
                    return JWTToken(result.Email, result.UserID);
                }
                else
                {
                    return null;
                }

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
                var emailCheck = fundooContext.UserTable.FirstOrDefault(e => e.Email == email);
                if (emailCheck != null)
                {
                    var token = JWTToken(emailCheck.Email, emailCheck.UserID);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return token.ToString();
                }

                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword( string email,string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var Result = fundooContext.UserTable.Where(e => e.Email == email).FirstOrDefault();
                    Result.Password = confirmPassword;
                    fundooContext.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public string JWTToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }


    }
}
    

