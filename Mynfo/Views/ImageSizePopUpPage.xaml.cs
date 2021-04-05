namespace Mynfo.Views
{
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageSizePopUpPage 
    {
        #region Attributes
        public UserLocal User;
        #endregion

        public ImageSizePopUpPage()
        {
            InitializeComponent();
            User = MainViewModel.GetInstance().User;
            ImageSize.Source = User.ImageFullPath;
        }
    }
}