using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity CreateCollab(long UserID, long NoteId, Collaborator collaborator);
        public List<CollabEntity> GetAllCollab(long UserID);
        public bool DeleteCollab(long collabId);

    }
}
