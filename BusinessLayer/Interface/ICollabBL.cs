using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL 
    {
        public CollabEntity CreateCollab(long userId, long noteId, Collaborator collaborator);
    }
}
