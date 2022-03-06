using Lab.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Lab.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath("dbxamarin.db");
        public List<District> Districts { get; set; }
        private District selectedDistrict;
        public District SelectedDistrict
        {
            get { return selectedDistrict; }
            set
            {
                selectedDistrict = value;
                OnPropertyChanged("SelectedDistrict");
            }
        }
        private string currentSquare;
        public string CurrentSquare
        {
            get { return currentSquare; }
            set
            {
                currentSquare = value;
                OnPropertyChanged("CurrentSquare");
            }
        }
        private string currentFloor;
        public string CurrentFloor
        {
            get { return currentFloor; }
            set
            {
                currentFloor = value;
                OnPropertyChanged("CurrentFloor");
            }
        }
        private string currentMaterial;
        public string CurrentMaterial
        {
            get { return currentMaterial; }
            set
            {
                currentMaterial = value;
                OnPropertyChanged("CurrentMaterial");
            }
        }
        private string max;
        public string Max
        {
            get { return max; }
            set
            {
                max = value;
                if (Double.TryParse(max, out double buf))
                {
                    Count = allFlats.Where(i => i.square <= Double.Parse(max)).Count().ToString();
                }
                else
                {
                    Count = allFlats.Count().ToString();
                }
                OnPropertyChanged("Max");
            }
        }
        private string count;
        public string Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        private string currentRoom;
        public string CurrentRoom
        {
            get { return currentRoom; }
            set
            {
                currentRoom = value;
                OnPropertyChanged("CurrentRoom");
            }
        }

        private Flat selectedFlat;
        public Flat SelectedFlat
        {
            get { return selectedFlat; }
            set
            {
                selectedFlat = value;
                if (selectedFlat != null)
                {
                    SelectedDistrict = Districts.Where(i => i.district_id == selectedFlat.district_id).FirstOrDefault();
                    CurrentRoom = SelectedFlat.room.ToString();
                    CurrentFloor = SelectedFlat.floor.ToString();
                    CurrentMaterial = SelectedFlat.material.ToString();
                    CurrentSquare = SelectedFlat.square.ToString();
                }
                OnPropertyChanged("SelectedFlat");
            }
        }
        private ObservableCollection<Flat> allFlats;
        public ObservableCollection<Flat> AllFlats
        {
            get { return allFlats; }
            set
            {
                allFlats = value;
                OnPropertyChanged("AllFlats");
            }
        }
        public MainViewModel(Context daaaab)
        {
            using (Context db = new Context(dbPath))
            {
                Districts = db.Districts.ToList();
                AllFlats = new ObservableCollection<Flat>(db.Flats.Select(i => new Flat(i)));
            }
            //Districts = new List<District>()
            //{
            //    new District("Район 1")
            //    {
            //        district_id = 0
            //    },
            //    new District("Район 2")
            //    {
            //        district_id = 1
            //    },
            //    new District("Район 3")
            //    {
            //        district_id = 2
            //    }
            //};
            //AllFlats = new ObservableCollection<Flat>()
            //{
            //    new Flat(0, 20, 2, "кирпич", 2)
            //    {
            //        flat_id = 0
            //    },
            //    new Flat(0, 50, 1, "кирпич", 4)
            //    {
            //        flat_id = 1
            //    },
            //    new Flat(1, 18, 3, "кирпич", 1)
            //    {
            //        flat_id = 2
            //    },
            //    new Flat(1, 44, 2, "кирпич", 2)
            //    {
            //        flat_id = 3
            //    },
            //    new Flat(2, 40, 4, "дерево", 3)
            //    {
            //        flat_id = 4
            //    },
            //};
            setCommands();
            Max = "";
        }

        private bool Check()
        {
            return Byte.TryParse(currentFloor, out byte buf1) && Byte.TryParse(currentRoom, out byte buf2)
                && Double.TryParse(currentSquare, out double buf3) && currentMaterial != "" && selectedDistrict != null;
        }
        private void setCommands()
        {
            Create = new RelayCommand(() =>
            {
                if (Check())
                {
                    EFlat buf = new EFlat(selectedDistrict.district_id, Double.Parse(currentSquare),
                        Byte.Parse(currentFloor), currentMaterial, Byte.Parse(currentRoom));
                    using (Context db = new Context(dbPath))
                    {
                        db.Flats.Add(buf);
                        db.SaveChanges();
                    }
                    AllFlats.Add(new Flat(buf) { flat_id = 5 });
                    Max = max;
                }
            });
            Update = new RelayCommand(() =>
            {
                if (Check() && selectedFlat != null)
                {
                    selectedFlat.district_id = selectedDistrict.district_id;
                    selectedFlat.floor = Byte.Parse(currentFloor);
                    selectedFlat.square = Double.Parse(currentSquare);
                    selectedFlat.room = Byte.Parse(currentRoom);
                    selectedFlat.material = currentMaterial;
                    using (Context db = new Context(dbPath))
                    {
                        db.Flats.Update(new EFlat(selectedFlat));
                        db.SaveChanges();
                    }
                    Max = max;
                }
            });
            Delete = new RelayCommand(() =>
            {
                if (selectedFlat != null)
                {
                    using (Context db = new Context(dbPath))
                    {
                        db.Flats.Remove(new EFlat(selectedFlat));
                        db.SaveChanges();
                    }
                    AllFlats.Remove(selectedFlat);
                    selectedFlat = null;
                    Max = max;
                }
            });
        }

        public RelayCommand Create { get; set; }
        public RelayCommand Update { get; set; }
        public RelayCommand Delete { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
