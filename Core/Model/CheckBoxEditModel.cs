using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class CheckBoxEditModel
    {
        public long Id { get; set; }

        public string Value { get; set; }

        public bool IsSelected { get; set; }


        public CheckBoxEditModel(long id, string value, bool isSelected)
        {
            this.Id = id;
            this.Value = value;
            this.IsSelected = isSelected;
        }
    }
}
