using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PADataToDatabase.Models
{
    public class ElectionMap
    {
        [Key]
        public int RowIndex { get; set; }

        public string County { get; set; }
        public int ElectionIndex { get; set; }
        public string ElectionName { get; set; }
        public DateTime When { get; set; }
    }
}
