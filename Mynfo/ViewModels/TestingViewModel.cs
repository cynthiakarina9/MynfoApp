namespace Mynfo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    public class TestingViewModel : BaseViewModel
    {

        #region Attributes
        private ObservableCollection<Network> listOfNetworks;

        private List<Network> networks;
        #endregion

        #region Properties
        public ObservableCollection<Network> ListOfNetworks
        {
            get { return listOfNetworks; }
            private set
            {
                SetValue(ref listOfNetworks, value);

            }
        }
        public List<Network> Networks
        {
            get { return networks; }
            private set
            {
                SetValue(ref networks, value);
            }
        }
        #endregion

        #region Contructors
        public TestingViewModel()
        {
            GetNetworks();
        }
        #endregion

        #region Commands
        public ObservableCollection<Network> GetNetworks()
        {
            networks = new List<Network>();
            //Llenado de lista de networks
            networks.Add(new Network() { Name = "Primera", Icon = "facebook2", NetworkId = 1 });
            networks.Add(new Network() { Name = "Segunda", Icon = "tel2", NetworkId = 2 });
            networks.Add(new Network() { Name = "Tercera", Icon = "whatsapp", NetworkId = 3 });
            networks.Add(new Network() { Name = "Cuarta", Icon = "mail2", NetworkId = 4 });
            networks.Add(new Network() { Name = "Quinta", Icon = "mail2", NetworkId = 5 });
            networks.Add(new Network() { Name = "Sexta", Icon = "facebook2", NetworkId = 1 });
            networks.Add(new Network() { Name = "Septima", Icon = "tel2", NetworkId = 2 });
            networks.Add(new Network() { Name = "Octava", Icon = "whatsapp", NetworkId = 3 });
            networks.Add(new Network() { Name = "Novena", Icon = "mail2", NetworkId = 4 });
            networks.Add(new Network() { Name = "Decima", Icon = "mail2", NetworkId = 5 });

            ListOfNetworks = new ObservableCollection<Network>();

            foreach (Network n in networks)
            {
                ListOfNetworks.Add(n);
            }

            return ListOfNetworks;

        }
        #endregion

    }
}
