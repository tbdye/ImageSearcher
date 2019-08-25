using ImageSearcher.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher.MainUI
{
    public class SearchBarViewModel : UiToolsBase
    {
        private readonly ImageSearchModel imageSearchModel;

        private string searchTextField;
        private string selectedImageSizeField;
        private string selectedColorField;
        private string selectedTypeField;
        private string selectedLayoutField;
        private string selectedPeopleField;
        private string selectedDateField;
        private string selectedLicenseField;
        private string selectedSafeSearchField;

        public SearchBarViewModel(ImageSearchModel imageSearchModel)
        {
            this.imageSearchModel = imageSearchModel;
        }

        public string SearchText
        {
            get
            {
                return searchTextField;
            }

            set
            {
                if (SetProperty(ref this.searchTextField, value))
                {
                    this.imageSearchModel.SearchText = value;
                }
            }
        }

        public List<string> ImageSizeList => this.imageSearchModel.ImageSizeList;

        public List<string> ColorList => this.imageSearchModel.ColorList;

        public List<string> TypeList => this.imageSearchModel.TypeList;

        public List<string> LayoutList => this.imageSearchModel.LayoutList;

        public List<string> PeopleList => this.imageSearchModel.PeopleList;

        public List<string> DateList => this.imageSearchModel.DateList;

        public List<string> LicenseList => this.imageSearchModel.LicenseList;

        public List<string> SafeSearchList => this.imageSearchModel.SafeSearchList;

        public string SelectedImageSize
        {
            get
            {
                return this.selectedImageSizeField;
            }

            set
            {
                this.SetProperty(ref this.selectedImageSizeField, value);
            }
        }

        public string SelectedColor
        {
            get
            {
                return this.selectedColorField;
            }

            set
            {
                this.SetProperty(ref this.selectedColorField, value);
            }
        }

        public string SelectedType
        {
            get
            {
                return this.selectedTypeField;
            }

            set
            {
                this.SetProperty(ref this.selectedTypeField, value);
            }
        }

        public string SelectedLayout
        {
            get
            {
                return this.selectedLayoutField;
            }

            set
            {
                this.SetProperty(ref this.selectedLayoutField, value);
            }
        }

        public string SelectedPeople
        {
            get
            {
                return this.selectedPeopleField;
            }

            set
            {
                this.SetProperty(ref this.selectedPeopleField, value);
            }
        }

        public string SelectedDate
        {
            get
            {
                return this.selectedDateField;
            }

            set
            {
                this.SetProperty(ref this.selectedDateField, value);
            }
        }

        public string SelectedLicense
        {
            get
            {
                return this.selectedLicenseField;
            }

            set
            {
                this.SetProperty(ref this.selectedLicenseField, value);
            }
        }

        public string SelectedSafeSearch
        {
            get
            {
                return this.selectedSafeSearchField;
            }

            set
            {
                this.SetProperty(ref this.selectedSafeSearchField, value);
            }
        }
    }
}
