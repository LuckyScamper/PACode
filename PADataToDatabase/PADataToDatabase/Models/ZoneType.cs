using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PADataToDatabase.Models
{
    public class ZoneType
    {
        [Key]
        public int RowIndex { get; set; }

        public string County { get; set; }
        public int TypeIndex { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
