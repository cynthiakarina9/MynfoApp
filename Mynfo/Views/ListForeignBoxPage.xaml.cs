namespace Mynfo.Views
{
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Models;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListForeignBoxPage : ContentPage
    {
        public IList<ForeingBox> foreingBoxe { get; private set; }
        public ListForeignBoxPage()
        {
            
            InitializeComponent();
            foreingBoxe = new List<ForeingBox>();
            foreingBoxe.Add(new ForeingBox
            {
                

            });

        }
    }
}