namespace Mynfo.Views
{
    using Rg.Plugins.Popup.Services;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Testing : ContentPage
    {
        public Testing()
        {
            InitializeComponent();


            //AbrirPopUp.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender,e));
        }

        private async void OpenPopupTest(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupExample());
        }

    }
}