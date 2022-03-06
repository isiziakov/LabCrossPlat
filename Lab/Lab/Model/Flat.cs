using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab.Model
{
    public class Flat : INotifyPropertyChanged
    {
        [Required]
        public int district_id { get; set; }
        [Key]
        public int flat_id { get; set; }
        [Required]
        private double _square;
        public double square
        {
            get { return _square; }
            set
            {
                _square = value;
                OnPropertyChanged("square");
            }
        }
        private byte _room;
        public byte room
        {
            get { return _room; }
            set
            {
                _room = value;
                OnPropertyChanged("room");
            }
        }
        [Required]
        public byte floor { get; set; }
        [Required]
        [StringLength(50)]
        public string material { get; set; }
        public virtual District District { get; set; }

        public Flat()
        {

        }
        public Flat(EFlat flat)
        {
            flat_id = flat.flat_id;
            district_id = flat.district_id;
            square = flat.square;
            floor = flat.floor;
            material = flat.material;
            room = flat.room;
        }
        public Flat(int district_id, double square, byte floor, string material, byte room)
        {
            this.district_id = district_id;
            this.square = square;
            this.floor = floor;
            this.material = material;
            this.room = room;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
