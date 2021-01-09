namespace Mynfo.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Rg.Plugins.Popup.Enums;
    using Rg.Plugins.Popup.Animations;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PupPopPage : PopupPage
    {
        public PupPopPage()
        {
            InitializeComponent();
        }
        
    }
}