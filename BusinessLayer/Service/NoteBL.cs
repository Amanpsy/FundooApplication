using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public NoteEntity CreateNote(long UserID, Note createnote)
        {
            try
            {
               return noteRL.CreateNote(UserID, createnote);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<NoteEntity>GetNote(long userId)
        {
            try
            {
                return this.noteRL.GetNote(userId);

            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }
        public NoteEntity UpdateNote(long userId, long noteId, Note note)
        {
            try
            {
                return this.noteRL.UpdateNote(userId, noteId, note);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                return this.noteRL.DeleteNote(userId,noteId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public NoteEntity PinnedNote( long noteId)
        {
            try
            {
                return this.noteRL.PinnedNote(noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NoteEntity ArchieveNote(long noteId)
        {
            try
            {  
                return this.noteRL.ArchieveNote(noteId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public NoteEntity Trash( long noteId)
        {
            try
            {
                return this.noteRL.Trash( noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NoteEntity NoteColor(long noteId, string color)
        {
            try
            {
                return this.noteRL.NoteColor(noteId, color);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NoteEntity Image( long noteId, IFormFile file)
        {
            try
            {
                return this.noteRL.Image( noteId, file);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
      
    }
}
