using Mynfo.Domain;
using Mynfo.Helpers;
using Mynfo.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerPopUp : PopupPage
    {
        public ColorPickerPopUp(Box _Box)
        {
            InitializeComponent();
            ColorPicker.CloseWhenBackgroundIsClicked = true;

            string colorHex = _Box.ColorBox;

            switch (colorHex)
            {
                case "#12947f":
                    Green.IsChecked = true;// "Verde";
                    break;
                case "#2fc4b2":
                    Cyan.IsChecked = true;// "Verde Agua";
                    break;
                case "#404a7f":
                    DarkBlue.IsChecked = true;// "Azul oscuro";
                    break;
                case "#FF5521":
                    Orange.IsChecked = true;// "Anaranjado";
                    break;
                case "#508ed8":
                    LightBlue.IsChecked = true;// "Azul claro";
                    break;
                case "#d89a00":
                    Yellow.IsChecked = true;// "Amarillo";
                    break;
                case "#ff0033":
                    Fuschia.IsChecked = true;// "Fuchsia";
                    break;
                case "#008445":
                    DarkGreen.IsChecked = true;// "Verde Oscuro";
                    break;
                case "#7f416a":
                    Purple.IsChecked = true;// "Morado";
                    break;
                case "#6f50ff":
                    Lilac.IsChecked = true;// "Lila";
                    break;
                case "#c1271f":
                    Red.IsChecked = true;// "Rojo";
                    break;
                case "#ce7d7d":
                    Pink.IsChecked = true;// "Rosa";
                    break;
                default:
                    Green.IsChecked = true;// "Sin color";
                    break;
            }

            SaveColorBtn.Clicked += new EventHandler((sender, e) => SaveColor(sender, e, _Box));
        }

        async void SaveColor(object sender, EventArgs e, Box _Box)
        {
            if(Green.IsChecked)
            {
                _Box.ColorBox = "#12947f";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Green;
            }
            if (Cyan.IsChecked)
            {
                _Box.ColorBox = "#2fc4b2";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Cyan;
            }
            if (DarkBlue.IsChecked)
            {
                _Box.ColorBox = "#404a7f";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.DarkBlue;
            }
            if (Orange.IsChecked)
            {
                _Box.ColorBox = "#FF5521";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Orange;
            }
            if (LightBlue.IsChecked)
            {
                _Box.ColorBox = "#508ed8";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.LightBlue;
            }
            if (Yellow.IsChecked)
            {
                _Box.ColorBox = "#d89a00";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Yellow;
            }
            if (Fuschia.IsChecked)
            {
                _Box.ColorBox = "#ff0033";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Fuchsia;
            }
            if (DarkGreen.IsChecked)
            {
                _Box.ColorBox = "#008445";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.DarkGreen;
            }
            if (Purple.IsChecked)
            {
                _Box.ColorBox = "#7f416a";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Purple;
            }
            if (Lilac.IsChecked)
            {
                _Box.ColorBox = "#6f50ff";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Lilac;
            }
            if (Red.IsChecked)
            {
                _Box.ColorBox = "#c1271f";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Red;
            }
            if (Pink.IsChecked)
            {
                _Box.ColorBox = "#ce7d7d";
                MainViewModel.GetInstance().DetailsBoxEdith.Color = Languages.Pink;
            }

            MainViewModel.GetInstance().DetailsBoxEdith.EdithBox(_Box);
            await Navigation.PopPopupAsync();

        }
    }
}