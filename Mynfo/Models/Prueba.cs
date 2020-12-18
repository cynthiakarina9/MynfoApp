
namespace Mynfo.Models
{
    using Mynfo.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;

    public class Prueba : BaseViewModel
    {
        #region Attributes
        private string name;
        private ImageSource image;
        #endregion

        #region Properties
        public ImageSource Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
        }

        public string Name
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }
        #endregion

    }
}
