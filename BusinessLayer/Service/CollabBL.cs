using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
    
        public CollabEntity CreateCollab(long UserID, long NoteId, Collaborator collaborator)
        {
            try
            {
                return this.collabRL.CreateCollab(UserID, NoteId, collaborator);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CollabEntity> GetAllCollab(long UserID)
        {
            try
            {
                return this.collabRL.GetAllCollab(UserID);
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
                return this.collabRL.DeleteCollab(collabId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
