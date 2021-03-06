namespace Mynfo.ViewModels
{
    public class IntroductionGifViewModel : BaseViewModel
    {

        private string gitImage;
        private bool intro;
        public string GitImage
        {
            get { return this.gitImage; }
            set { SetValue(ref this.gitImage, value); }
        }
        public bool Intro
        {
            get { return this.intro; }
            set { SetValue(ref this.intro, value); }
        }
        public IntroductionGifViewModel()
        {
            GitImage = "GIF_mynfo_general.gif";
            Intro = MainViewModel.GetInstance().User.MostrarTutorial;
        }
    }
}
