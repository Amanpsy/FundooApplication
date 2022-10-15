using Microsoft.Extensions.Configuration;
using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class CollabRL :ICollabRL
    {
        private readonly FundooContext fundooContext;
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollabEntity CreateCollab(long userId,long noteId, Collaborator collaborator)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.UserId = userId;
                collabEntity.NoteId = noteId;
                collabEntity.CollabEmail= collaborator.CollabEmail;
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
    }
}
