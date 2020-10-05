namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Views;
    using System;
    using System.IO;
    using System.Windows.Input;


    public class HomeViewModel
    {
        HomePage homePage;
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");
        public HomeViewModel()
        {
            if (File.Exists(_fileName))
            {
                //homePage.editor2 = File.ReadAllText(_fileName);
            }
        }

        public ICommand SaveNotesCommand
        {
            get
            {
                return new RelayCommand(SaveNotes);
            }
        }

        public void SaveNotes()
        {
            //File.WriteAllText(_fileName, homePage.editor2);
        }


        public ICommand DeleteNotesCommand
        {
            get
            {
                return new RelayCommand(DeleteNotes);
            }
        }

        public void DeleteNotes()
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            //homePage.editor2 = string.Empty;
        }
    }
}