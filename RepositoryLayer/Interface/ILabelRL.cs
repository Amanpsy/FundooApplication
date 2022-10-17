using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(long UserID, long NoteId, string LabelName);
        public List<LabelEntity> GetAllLabel(long userId);
        public LabelEntity UpdateLabel(long noteId, long labelId, string labelName);
        public bool DeleteLabel(string labelName);
    }
}
