namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using System.IO;
    using System.Windows.Input;

    public class BoxRegisterViewModel
    {
        #region Commands
        public ICommand CreateBoxCommand
        {
            get
            {
                return new RelayCommand(CreateBox);
            }
        }
        private void CreateBox()
        {
            var note = new Box();

            if (string.IsNullOrWhiteSpace(note.Name))
            {
                // Save
                //var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                var filename = Path.Combine(App.FolderPath, $"{note.Name}.notes.txt");
                File.WriteAllText(filename, note.Name);
            }
            else
            {
                // Update
                //File.WriteAllText(note.Filename, note.Name);
            }

            App.Navigator.PopAsync();
        }
        #endregion

        //async void OnSaveButtonClicked(object sender, EventArgs e)
        //{
        //    var note = (Box)BindingContext;

        //    if (string.IsNullOrWhiteSpace(note.Filename))
        //    {
        //        // Save
        //        //var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
        //        var filename = Path.Combine(App.FolderPath, $"{note.Name}.notes.txt");
        //        File.WriteAllText(filename, note.Text);
        //    }
        //    else
        //    {
        //        // Update
        //        File.WriteAllText(note.Filename, note.Text);
        //    }

        //    await Navigation.PopAsync();
        //}

        //async void OnDeleteButtonClicked(object sender, EventArgs e)
        //{
        //    var note = (Box)BindingContext;

        //    if (File.Exists(note.Filename))
        //    {
        //        File.Delete(note.Filename);
        //    }

        //    await Navigation.PopAsync();
        //}
    }
}
