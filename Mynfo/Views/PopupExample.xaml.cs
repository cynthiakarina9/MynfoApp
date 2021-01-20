using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Animations;
using Mynfo.ViewModels;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupExample : PopupPage
    {
        public PopupExample(string _label)
        {
            this.InitializeComponent();

            etiqueta1.Text = _label;
        }

        async private void gototest(object sender, EventArgs e)
        {
            var popup = new PopupExample("Ratón");
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Center,
                PositionOut = MoveAnimationOptions.Bottom
            };

            popup.CloseWhenBackgroundIsClicked = true;
            popup.BackgroundColor = Color.White;

            popup.Animation = scaleAnimation;

            await PopupNavigation.Instance.PushAsync(popup);
        }
    }
}