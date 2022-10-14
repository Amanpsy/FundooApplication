using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        private readonly FundooContext fundooContext;

        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;


        }

        public NoteEntity CreateNote(long UserID, Note createnote)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                var result = fundooContext.UserTable.Where(u => u.UserID == UserID).FirstOrDefault();

                noteEntity.Title = createnote.Title;
                noteEntity.Description = createnote.Description;
                noteEntity.Reminder = createnote.Reminder;
                noteEntity.Colour = createnote.Colour;
                noteEntity.Image = createnote.Image;
                noteEntity.Archive = createnote.Archieve;
                noteEntity.Pinned = createnote.Pinned;
                noteEntity.Trash = createnote.Trash;
                noteEntity.Edited = createnote.Edited;
                noteEntity.UserID = UserID;
                fundooContext.NoteTable.Add(noteEntity);
                int update = fundooContext.SaveChanges();
                if (update > 0)
                {
                    return noteEntity;
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

        public List<NoteEntity> GetNote(long userId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(note => note.UserID == userId).ToList();
                return result;

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
                var result = fundooContext.NoteTable.Where(note => note.UserID == userId && note.NoteId == noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = note.Title;
                    result.Description = note.Description;
                    result.Created = note.Created;
                    result.Edited = note.Edited;
                    fundooContext.NoteTable.Update(result);
                    var update = fundooContext.SaveChanges();
                    if (update > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
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

        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(note => note.UserID == userId && note.NoteId == noteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.NoteTable.Remove(result);
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

        public NoteEntity PinnedNote(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserID == userId && u.NoteId == noteId).FirstOrDefault();
                if (result.Pinned == false)
                {
                    result.Pinned = true;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    result.Pinned = false;
                    fundooContext.SaveChanges();
                    return result;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NoteEntity ArchieveNote(long userId, long noteId)
        {
            try
            {

                var result = fundooContext.NoteTable.Where(u => u.UserID == userId && u.NoteId == noteId).FirstOrDefault();
                if (result.Archive == true)
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return result;


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NoteEntity Trash(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserID == userId && u.NoteId == noteId).FirstOrDefault();
                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return result;
                }
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
                var result = fundooContext.NoteTable.Where(u => u.NoteId == noteId).FirstOrDefault();
                if(result.Colour != color)
                {
                    result.Colour = color;
                    fundooContext.NoteTable.Update(result);
                    fundooContext.SaveChanges();
                    return result;
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

        public NoteEntity Image(long noteId, IFormFile file)
        {
            try
            {
                var result = this.fundooContext.NoteTable.Where(u => u.NoteId == noteId).FirstOrDefault();
                if (result != null)

                {
                    CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("dsi4qityp", "357464411992568", "6JOnTmtOoS5S8BqtaIOhT9rX8MM");

                    Cloudinary _cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream())
                    };
                    var uploadresult = _cloudinary.Upload(uploadParams);
                    result.Image = uploadresult.Url.ToString();
                    fundooContext.SaveChanges();
                    return result;

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

