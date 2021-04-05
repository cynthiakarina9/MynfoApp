using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfileSMPage : ContentPage
    {
        #region Constructor
        public CreateProfileSMPage()
        {
            InitializeComponent();
            #region Logo Superior
            OSAppTheme currentTheme = App.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Logosuperior.Source = "logo_superior2.png";
            }
            else
            {
                Logosuperior.Source = "logo_superior3.png";
            }
            #endregion
            //ProfilesEmail.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Email", _boxDefault, _boxName));
        }
        #endregion
        

        #region Commands
        
        #endregion
    }
}