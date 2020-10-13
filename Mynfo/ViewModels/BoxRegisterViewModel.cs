namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Views;

    public class BoxRegisterViewModel
    {
        #region Commands
        public ICommand SaveBoxCommand
        {
            get
            {
                return new RelayCommand(SaveBox);
            }
        }
        private void SaveBox()
        {

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.AddProfiles = new AddProfilesViewModel();
            App.Navigator.PushAsync(new AddProfilesPage());
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
