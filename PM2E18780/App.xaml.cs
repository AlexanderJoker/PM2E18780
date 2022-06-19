using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E18780.Controles;
using System.IO;
namespace PM2E18780
{
    public partial class App : Application
    {
        static DataBase basedatos;

        public static DataBase BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UbicacionesDB.db3"));
                }

                return basedatos;
            }
        }

        public App()
        {

            InitializeComponent();

            MainPage = new MainPage();
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
