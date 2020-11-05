namespace Mynfo.ViewModels
{
    using Models;
    using System.Collections.Generic;

    public class ListForeignBoxViewModel
    {
        #region Properties
        public IList<ForeingBox> ForeingBoxes { get; private set; }
        #endregion
        #region Contructor
        public ListForeignBoxViewModel()
        {
            ForeingBoxes = new List<ForeingBox>();
            ForeingBoxes.Add(new ForeingBox
            {
                ImagePath = "no_image",
                FirstName = "Cynthia",
                LastName = "De la Cuesta"
            });
        }
        #endregion
    }
}
