namespace Mynfo.Views
{
    using System;
    using System.IO;
    using Xamarin.Forms;
    using Models;
    public partial class BoxRegisterPage : ContentPage
    {
        public BoxRegisterPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Box)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                // Save
                //var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                var filename = Path.Combine(App.FolderPath, $"{note.Name}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                // Update
                File.WriteAllText(note.Filename, note.Text);
            }

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Box)BindingContext;

            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }

            await Navigation.PopAsync();
        }
    }
}