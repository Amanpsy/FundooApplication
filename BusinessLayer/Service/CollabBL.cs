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
        public CollabEntity CreateCollab(long userId, long noteId, Collaborator collaborator)
        {
            try
            {
                return this.collabRL.CreateCollab(userId, noteId, collaborator);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
