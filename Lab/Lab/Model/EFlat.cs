using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab.Model
{
    public class EFlat
    {
        [Required]
        public int district_id { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 flat_id { get; set; }
        [Required]
        public double square { get; set; }
        [Required]
        public byte room { get; set; }
        [Required]
        public byte floor { get; set; }
        [Required]
        [StringLength(50)]
        public string material { get; set; }
        public virtual District District { get; set; }

        public EFlat(int district_id, double square, byte floor, string material, byte room)
        {
            this.district_id = district_id;
            this.square = square;
            this.floor = floor;
            this.material = material;
            this.room = room;
        }

        public EFlat(Flat flat)
        {
            flat_id = flat.flat_id;
            district_id = flat.district_id;
            square = flat.square;
            floor = flat.floor;
            material = flat.material;
            room = flat.room;
        }

        public EFlat()
        {

        }
    }
}
