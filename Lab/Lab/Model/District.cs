using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab.Model
{
    public class District
    {
        [Key]
        public int district_id { get; set; }
        [Required]
        [StringLength(50)]
        public string district { get; set; }

        public District()
        {

        }

        public District(string district)
        {
            this.district = district;
        }
    }
}
