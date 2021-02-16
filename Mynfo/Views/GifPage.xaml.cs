namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GifPage
    {
        public GifPage()
        {
            InitializeComponent();
            var F = MainViewModel.GetInstance().CreateProfileFacebook;
            var L = MainViewModel.GetInstance().CreateProfileLinkedin;
            var S = MainViewModel.GetInstance().CreateProfileSpotify;
            var Y = MainViewModel.GetInstance().CreateProfileYoutube;
            if (F != null)
            {
                GIFImage.Source = "GIF_Facebook.gif";
            }
            if (L != null)
            {
                GIFImage.Source = "GIF_LinkedIn.gif";
            }
            if (S != null)
            {
                GIFImage.Source = "GIF_Spotify.gif";
            }
            if (Y != null)
            {
                GIFImage.Source = "GIF_Youtube.gif";
            }
        }
    }
}