using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity CreateNote(long UserID, Note createnote);
        public List<NoteEntity>GetNote(long userId);

        public NoteEntity UpdateNote(long userId, long noteId, Note note);
        public bool DeleteNote(long userId, long noteId);
        public NoteEntity PinnedNote(long userId,long noteId);

        public NoteEntity ArchieveNote(long userId, long noteId);
        public NoteEntity Trash(long userId, long noteId);

        public NoteEntity NoteColor(long noteId, string color);

        public NoteEntity Image(long noteId, IFormFile file);

    }
}
