using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E18780
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Byte[] imgcap { get; set; }
        public Mapa()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            String latitud = Lati.Text;
            String longitud = Longi.Text;
            String descripcion = Desc.Text;
            base.OnAppearing();
            Pin ubicacion = new Pin();
            ubicacion.Label = "Destino";
            ubicacion.Address = descripcion;
            ubicacion.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(latitud), Convert.ToDouble(longitud));
            Map.Pins.Add(ubicacion);

            Map.MoveToRegion(new MapSpan(new Xamarin.Forms.Maps.Position(Convert.ToDouble(latitud), Convert.ToDouble(longitud)), 1, 1));

            var localizar = CrossGeolocator.Current;
            if (localizar != null)
            {
                localizar.PositionChanged += localizacion_positionChanged;

                if (!localizar.IsListening)
                {
                    await localizar.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                }

            }
        }

        private void localizacion_positionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var posi_mapa = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            Map.MoveToRegion(new MapSpan(posi_mapa, 1, 1));
        }

        public async Task Capturar()
        {
              
            var photo = "Photo.jpg";
            var file = Path.Combine(FileSystem.CacheDirectory, photo);
            File.WriteAllBytes(file, imgcap);


            await Share.RequestAsync(new ShareFileRequest { Title = Title, File = new ShareFile(file) });
        }
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }

        }
        
        

        private void btncompartir_Clicked(object sender, EventArgs e)
        {
            Capturar();
        }
    }
}
    

