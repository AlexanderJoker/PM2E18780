using PM2E18780.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E18780
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaLocalizacion : ContentPage
    {
        public PaginaLocalizacion()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var listaubicaciones = await App.BaseDatos.listaubicaciones();

            ObservableCollection<Ubicaciones> observableCollectionPhotos = new ObservableCollection<Ubicaciones>();
            ListaUbicaciones.ItemsSource = observableCollectionPhotos;
            foreach (Ubicaciones img in listaubicaciones)
            {
                observableCollectionPhotos.Add(img);
            }
        }

        private async void ListaUbicaciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Ubicaciones item = (Ubicaciones)e.Item;

            var alert = await DisplayAlert("Seleccione", "Seleccione que desea hacer a continuacion", "Ir a la Ubicacion", "Borrar Ubicacion");

            if (alert)
            {
                var lvmpDescripcion = await App.BaseDatos.ObtenerDescripcion(item.Descripcion);
                var lvmprLongitud = await App.BaseDatos.ObtenerLongitud(item.Longitud);
                var lvmprLatitud = await App.BaseDatos.ObtenerLatitud(item.Latitud);

                await Xamarin.Essentials.Map.OpenAsync(lvmprLongitud.Longitud, lvmprLatitud.Latitud, new MapLaunchOptions
                {
                    Name = lvmpDescripcion.Descripcion
                });
            }
            else
            {
                var resultado = await App.BaseDatos.EliminarUbicacion(item);

                if (resultado != 0)
                {
                    await DisplayAlert("Aviso", "Sitio eliminado exitosamente!!!", "Ok");
                }
                else
                {
                    await DisplayAlert("Aviso", "Ha ocurrido un error!!!", "Ok");
                }

                await Navigation.PopAsync();
            }
        }
    }
}
