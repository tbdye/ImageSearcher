using ImageSearcher.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageSearcher.MainUI
{
    public class SearchBarViewModel : UiToolsBase, IDisposable
    {
        private readonly ImageSearchModel imageSearchModel;

        private string searchTextField;

        public SearchBarViewModel(ImageSearchModel imageSearchModel)
        {
            this.imageSearchModel = imageSearchModel;

            this.DoSearch = new RelayCommand(this.DoSearchCommandHandler);

            this.imageSearchModel.FilterStateChanged += this.FilterStateChangedEventHandler;
        }

        public void Dispose()
        {
            this.imageSearchModel.FilterStateChanged -= this.FilterStateChangedEventHandler;
        }

        public ICommand DoSearch { get; }

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
                return this.imageSearchModel.SelectedImageSize == "All" ? "Image size" : this.imageSearchModel.SelectedImageSize;
            }

            set
            {
                if (this.imageSearchModel.SelectedImageSize != value)
                {
                    this.imageSearchModel.SelectedImageSize = value;
                    this.OnPropertyChanged(nameof(SelectedImageSize));
                }
            }
        }

        public string SelectedColor
        {
            get
            {
                return this.imageSearchModel.SelectedColor == "All" ? "Color" : this.imageSearchModel.SelectedColor;
            }

            set
            {
                if (this.imageSearchModel.SelectedColor != value)
                {
                    this.imageSearchModel.SelectedColor = value;
                    this.OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }

        public string SelectedType
        {
            get
            {
                return this.imageSearchModel.SelectedType == "All" ? "Type" : this.imageSearchModel.SelectedType;
            }

            set
            {
                if (this.imageSearchModel.SelectedType != value)
                {
                    this.imageSearchModel.SelectedType = value;
                    this.OnPropertyChanged(nameof(SelectedType));
                }
            }
        }

        public string SelectedLayout
        {
            get
            {
                return this.imageSearchModel.SelectedLayout == "All" ? "Layout" : this.imageSearchModel.SelectedLayout;
            }

            set
            {
                if (this.imageSearchModel.SelectedLayout != value)
                {
                    this.imageSearchModel.SelectedLayout = value;
                    this.OnPropertyChanged(nameof(SelectedLayout));
                }
            }
        }

        public string SelectedPeople
        {
            get
            {
                return this.imageSearchModel.SelectedPeople == "All" ? "People" : this.imageSearchModel.SelectedPeople;
            }

            set
            {
                if (this.imageSearchModel.SelectedPeople != value)
                {
                    this.imageSearchModel.SelectedPeople = value;
                    this.OnPropertyChanged(nameof(SelectedPeople));
                }
            }
        }

        public string SelectedDate
        {
            get
            {
                return this.imageSearchModel.SelectedDate == "All" ? "Date" : this.imageSearchModel.SelectedDate;
            }

            set
            {
                if (this.imageSearchModel.SelectedDate != value)
                {
                    this.imageSearchModel.SelectedDate = value;
                    this.OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public string SelectedLicense
        {
            get
            {
                return this.imageSearchModel.SelectedLicense == "All" ? "License" : this.imageSearchModel.SelectedLicense;
            }

            set
            {
                if (this.imageSearchModel.SelectedLicense != value)
                {
                    this.imageSearchModel.SelectedLicense = value;
                    this.OnPropertyChanged(nameof(SelectedLicense));
                }
            }
        }

        public string SelectedSafeSearch
        {
            get
            {
                return this.imageSearchModel.SelectedSafeSearch == "All" ? "Moderate" : this.imageSearchModel.SelectedSafeSearch;
            }

            set
            {
                if (this.imageSearchModel.SelectedSafeSearch != value)
                {
                    this.imageSearchModel.SelectedSafeSearch = value;
                    this.OnPropertyChanged(nameof(SelectedSafeSearch));
                }
            }
        }

        private void DoSearchCommandHandler()
        {
            this.imageSearchModel.DoSearch();
        }

        private void FilterStateChangedEventHandler(object o, EventArgs e)
        {
            this.imageSearchModel.DoSearch();
        }
    }
}
