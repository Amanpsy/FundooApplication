using BusinessLayer.Interface;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public LabelEntity CreateLabel(long UserID, long NoteId, string LabelName)
        {
            try
            {
                return this.labelRL.CreateLabel(UserID, NoteId, LabelName);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<LabelEntity> GetAllLabel(long userId)
        {
            try
            {
                return this.labelRL.GetAllLabel(userId);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LabelEntity UpdateLabel(long noteId, long labelId, string labelName)
        {
            try
            {
                return this.labelRL.UpdateLabel(noteId, labelId, labelName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteLabel(string labelName)
        {
            try
            {
                return this.labelRL.DeleteLabel(labelName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
