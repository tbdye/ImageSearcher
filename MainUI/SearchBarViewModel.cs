using ImageSearcher.Components;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ImageSearcher.MainUI
{
    public class SearchBarViewModel : UiToolsBase, IDisposable
    {
        private bool isDisposed = false;
        private readonly ImageSearchModel imageSearchModel;

        public SearchBarViewModel(ImageSearchModel imageSearchModel)
        {
            this.imageSearchModel = imageSearchModel;

            this.DoSearch = new DelegateCommand(this.DoSearchCommandHandler);

            this.imageSearchModel.FilterStateChanged += this.FilterStateChangedEventHandler;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.imageSearchModel.FilterStateChanged -= this.FilterStateChangedEventHandler;
                }

                this.isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public ICommand DoSearch { get; }

        public string SearchText
        {
            get
            {
                return this.imageSearchModel.SearchText;
            }

            set
            {
                if (this.imageSearchModel.SearchText != value)
                {
                    this.imageSearchModel.SearchText = value;
                    this.OnPropertyChanged(nameof(this.SearchText));
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
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedImageSize == "All" ? "Image size" : this.imageSearchModel.SelectedImageSize;
            }

            set
            {
                // The SearchBarView codebehind will force an invalid selection when the default value is reset.  Do not null the selected string.
                if (value != null)
                {
                    this.imageSearchModel.SelectedImageSize = value;
                }
            }
        }

        public string SelectedColor
        {
            get
            {
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedColor == "All" ? "Color" : this.imageSearchModel.SelectedColor;
            }

            set
            {
                // The SearchBarView codebehind will force an invalid selection when the default value is reset.  Do not null the selected string.
                if (value != null)
                {
                    this.imageSearchModel.SelectedColor = value;
                }
            }
        }

        public string SelectedType
        {
            get
            {
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedType == "All" ? "Type" : this.imageSearchModel.SelectedType;
            }

            set
            {
                // The SearchBarView codebehind will force an invalid selection when the default value is reset.  Do not null the selected string.
                if (value != null)
                {
                    this.imageSearchModel.SelectedType = value;
                }
            }
        }

        public string SelectedLayout
        {
            get
            {
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedLayout == "All" ? "Layout" : this.imageSearchModel.SelectedLayout;
            }

            set
            {
                // The SearchBarView codebehind will force an invalid selection when the default value is reset.  Do not null the selected string.
                if (value != null)
                {
                    this.imageSearchModel.SelectedLayout = value;
                }
            }
        }

        public string SelectedPeople
        {
            get
            {
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedPeople == "All" ? "People" : this.imageSearchModel.SelectedPeople;
            }

            set
            {
                // The SearchBarView codebehind will force an invalid selection when the default value is reset.  Do not null the selected string.
                if (value != null)
                {
                    this.imageSearchModel.SelectedPeople = value;
                }
            }
        }

        public string SelectedDate
        {
            get
            {
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedDate == "All" ? "Date" : this.imageSearchModel.SelectedDate;
            }

            set
            {
                // The SearchBarView codebehind will force an invalid selection when the default value is reset.  Do not null the selected string.
                if (value != null)
                {
                    this.imageSearchModel.SelectedDate = value;
                }
            }
        }

        public string SelectedLicense
        {
            get
            {
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedLicense == "All" ? "License" : this.imageSearchModel.SelectedLicense;
            }

            set
            {
                // The SearchBarView codebehind will force an invalid selection when the default value is reset.  Do not null the selected string.
                if (value != null)
                {
                    this.imageSearchModel.SelectedLicense = value;
                }
            }
        }

        public string SelectedSafeSearch
        {
            get
            {
                // If the current selected value is the default value, force the ComboBox to display the PlaceholderText.
                return this.imageSearchModel.SelectedSafeSearch == "Moderate" ? "SafeSearch" : this.imageSearchModel.SelectedSafeSearch;
            }

            set
            {
                this.imageSearchModel.SelectedSafeSearch = value;
            }
        }

        // Do a new image search when pressing the search button or when pressing enter from inside the search textbox.
        private void DoSearchCommandHandler(object o)
        {
            this.imageSearchModel.DoSearch();
        }

        // Do a new image search with the existing search term when changing filters.
        private void FilterStateChangedEventHandler(object o, EventArgs e)
        {
            this.imageSearchModel.DoSearch();
        }
    }
}
