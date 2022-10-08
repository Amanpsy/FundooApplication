using CommonLayer.Model;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRl : IUserRl
    {
        private readonly FundooContext fundooContext;

        public UserRl(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
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
                if(result > 0)
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
                    return JWTToken(result.Email, result.UserId);
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
                var emailCheck= fundooContext.UserTable.FirstOrDefault(a => a.Email == email);
                if (emailCheck != null)
                {
                    var Token= JWTToken(emailCheck.Email, emailCheck.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(Token);
                    return Token.ToString();
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



        private static string JWTToken(string Email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("My_precious_key_z_security");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }


    }
    }
    

