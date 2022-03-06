using Lab.Model;
using Lab.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab
{
    public partial class MainPage : ContentPage
    {
        public MainPage(Context db)
        {
            Label flats = new Label { FontSize = 18, WidthRequest = 70 };
            flats.SetBinding(Label.TextProperty, "Count");
            var MainVM = new MainViewModel(db);
            var list = new ListView()
            {
                RowHeight = 60,
                ItemsSource = MainVM.AllFlats,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label id = new Label { FontSize = 18 };
                    id.SetBinding(Label.TextProperty, "flat_id");
                    Label room = new Label { FontSize = 18 };
                    room.SetBinding(Label.TextProperty, "room");
                    Label square = new Label { FontSize = 18 };
                    square.SetBinding(Label.TextProperty, "square");
                    return new ViewCell()
                    {
                        View = new StackLayout()
                        {
                            Children =
                            {
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    Children =
                                    {
                                        new Label()
                                        {
                                            Text = "Номер: ",
                                            FontSize = 18
                                        },
                                        id
                                    }
                                },
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    Children =
                                    {
                                        new Label()
                                        {
                                            Text = "Площадь: ",
                                            FontSize = 18
                                        },
                                        square,
                                        new Label()
                                        {
                                            Text = "Комнат: ",
                                            FontSize = 18
                                        },
                                        room
                                    }
                                }
                            }
                        }
                    };
                })
            };
            list.SetBinding(ListView.SelectedItemProperty, "SelectedFlat");
            var picker = new Picker()
            {
                WidthRequest = 280,
                ItemsSource = MainVM.Districts,
                ItemDisplayBinding = new Binding("district")
            };
            picker.SetBinding(Picker.SelectedItemProperty, "SelectedDistrict");
            var ematerial = new Entry()
            {
                FontSize = 18,
                WidthRequest = 120,
                Keyboard = Keyboard.Numeric
            };
            ematerial.SetBinding(Entry.TextProperty, "CurrentMaterial");
            var eroom = new Entry()
            {
                FontSize = 18,
                WidthRequest = 75,
                Keyboard = Keyboard.Numeric
            };
            eroom.SetBinding(Entry.TextProperty, "CurrentRoom");
            var esquare = new Entry()
            {
                FontSize = 18,
                WidthRequest = 95,
                Keyboard = Keyboard.Numeric
            };
            esquare.SetBinding(Entry.TextProperty, "CurrentSquare");
            var emax = new Entry()
            {
                FontSize = 18,
                WidthRequest = 70,
                Keyboard = Keyboard.Numeric
            };
            emax.SetBinding(Entry.TextProperty, "Max");  
            var efloor = new Entry()
            {
                FontSize = 18,
                WidthRequest = 70,
                Keyboard = Keyboard.Numeric
            };
            efloor.SetBinding(Entry.TextProperty, "CurrentFloor");
            Content = new StackLayout()
            {
                Margin = 8,
                Children = { new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new Label()
                        {
                            Text = "Выбранная квартира",
                            FontSize = 28,
                            HorizontalTextAlignment = TextAlignment.Center
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new Label()
                                {
                                    Text = "Комнат:",
                                    FontSize = 18
                                },
                                eroom,
                                new Label()
                                {
                                    Text = "Площадь:",
                                    FontSize = 18
                                },
                                esquare
                            }
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new Label()
                                {
                                    Text = "Этаж:",
                                    FontSize = 18
                                },
                                efloor,
                                new Label()
                                {
                                    Text = "Материал:",
                                    FontSize = 18
                                },
                                ematerial
                            }
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new Label()
                                {
                                    Text = "Район:",
                                    FontSize = 18
                                },
                                picker
                            }
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new Button()
                                {
                                    Text = "Сохранить",
                                    FontSize = 18,
                                    Command = MainVM.Update
                                },
                                new Button()
                                {
                                    Text = "Удалить",
                                    FontSize = 18,
                                    Command = MainVM.Delete
                                },
                                new Button()
                                {
                                    Text = "Создать",
                                    FontSize = 18,
                                    Command = MainVM.Create
                                }
                            }
                        }
                    }
                },
                list,
                new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new Label()
                                {
                                    Text = "Площадь",
                                    FontSize = 18
                                },
                                emax
                            }
                        },
                        new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new Label()
                                {
                                    Text = "Квартир с не большей площадью:",
                                    FontSize = 18
                                },
                                flats
                            }
                        }
                    }
                }
                }
            };
            //InitializeComponent();
            BindingContext = MainVM;
        }
    }
}
