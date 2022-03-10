using Lab.Model;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string dbPath = DependencyService.Get<IPath>().GetDatabasePath("dbxamarin.db");
            var db = new Context();
            db.Database.EnsureCreated();

            if (db.Districts.Count() == 0)
            {
                db.Districts.Add(new District { district = "район 1" });
                db.Districts.Add(new District { district = "район 2" });
                db.Districts.Add(new District { district = "район 3" });
                db.SaveChanges();
            }
            MainPage = new MainPage(db);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
