using Mynfo.ViewModels;
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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuItemViewModel selectedItem = e.SelectedItem as MenuItemViewModel;
        }
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            MenuItemViewModel tappedItem = e.Item as MenuItemViewModel;
        }        
    }
}

public class DetectShakeTest
{
    // Set speed delay for monitoring changes.
    Xamarin.Essentials.SensorSpeed speed = Xamarin.Essentials.SensorSpeed.Game;

    public DetectShakeTest()
    {
        // Register for reading changes, be sure to unsubscribe when finished
        Xamarin.Essentials.Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
    }

    void Accelerometer_ShakeDetected(object sender, EventArgs e)
    {
        // Process shake event
    }

    public void ToggleAccelerometer()
    {
        try
        {
            if (Xamarin.Essentials.Accelerometer.IsMonitoring)
                Xamarin.Essentials.Accelerometer.Stop();
            else
                Xamarin.Essentials.Accelerometer.Start(speed);
        }
        catch (Xamarin.Essentials.FeatureNotSupportedException fnsEx)
        {
            // Feature not supported on device
        }
        catch (Exception ex)
        {
            // Other error has occurred.
        }
    }
}