using Microsoft.Extensions.Configuration;
using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RepositoryLayer.Service
{
    public class CollabRL :ICollabRL
    {
        private readonly FundooContext fundooContext;
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
       
        public CollabEntity CreateCollab(long UserID, long NoteId,Collaborator collaborator)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.UserID = UserID;
                collabEntity.NoteId = NoteId;
                collabEntity.CollabEmail = collaborator.CollabEmail;
                collabEntity.Edited = collaborator.Edited;
                fundooContext.CollabTable.Add(collabEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return collabEntity;
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

        public List<CollabEntity>GetAllCollab(long UserID)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(u => u.UserID == UserID).ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteCollab(long collabId)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(u => u.CollabId == collabId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.CollabTable.Remove(result);
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
    }
}
