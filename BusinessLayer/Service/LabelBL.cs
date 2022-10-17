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
    }
}
