namespace Mynfo.Views
{
    using Rg.Plugins.Popup.Animations;
    using Rg.Plugins.Popup.Enums;
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


            AbrirPopUp1.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender,e,"Perro"));
            AbrirPopUp2.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender, e,"Gato"));
            AbrirPopUp3.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender, e,"Jirafa"));
            AbrirPopUp4.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender, e,"Serpiente"));
            AbrirPopUp5.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender, e,"Foca"));
            AbrirPopUp6.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender, e,"Ratón"));
        }

        private async void OpenPopupTest(object sender, EventArgs e, string _label)
        {
            var popup = new PopupExample(_label);
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Center,
                PositionOut = MoveAnimationOptions.Bottom
            };

            popup.CloseWhenBackgroundIsClicked = true;
            popup.BackgroundColor = Color.Orange;

            popup.Animation = scaleAnimation;

            await PopupNavigation.Instance.PushAsync(popup);
        }

    }
}