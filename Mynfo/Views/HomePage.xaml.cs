namespace Mynfo.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;
    using Models;
    using Xamarin.Forms.Xaml;

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            var Default = new Button();
            Default.Text = "Prueba 1";
            Default.BackgroundColor = Color.FromHex("#f9a589");
            Default.CornerRadius = 70;
            Default.FontAttributes = FontAttributes.Bold;
            Default.FontSize = 20;
            Default.HeightRequest = 140;
            Default.TextColor = Color.FromHex("#fff");
            Default.WidthRequest = 140;


            DefaultButton.Children.Add(Default);
        }

    }
}