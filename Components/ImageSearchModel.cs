using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearcher.Components
{
    public class ImageSearchModel: IDisposable
    {
        private readonly BingImageSearchConnector imageSearchConnector;

        // Initialize the default values for filters used in the Bing Image Search API
        private string selectedImageSizeField = "All";
        private string selectedColorField = "All";
        private string selectedTypeField = "All";
        private string selectedLayoutField = "All";
        private string selectedPeopleField = "All";
        private string selectedDateField = "All";
        private string selectedLicenseField = "All";
        private string selectedSafeSearchField = "Moderate";

        private bool isDisposed = false;

        // Used to initiate a new search based on the existing search term when the filters are changed in the UI
        public event EventHandler FilterStateChanged;

        // Pass up events from the BimgImageSearchConnector
        public event EventHandler<bool> TotalEstimatedMatchesChanged;
        public event EventHandler<bool> SearchInProgressStatusChanged;

        public ImageSearchModel()
        {
            this.imageSearchConnector = new BingImageSearchConnector();

            this.SearchResults = new ObservableCollection<ImageCollection>();

            this.imageSearchConnector.TotalEstimatedMatchesChanged += this.TotalEstimatedMatchesChangedEventHandler;
            this.imageSearchConnector.SearchInProgressStatusChanged += this.SearchInProgressStatusChangedEventHandler;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.imageSearchConnector.TotalEstimatedMatchesChanged -= this.TotalEstimatedMatchesChangedEventHandler;
                    this.imageSearchConnector.SearchInProgressStatusChanged -= this.SearchInProgressStatusChangedEventHandler;
                }

                this.isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public ObservableCollection<ImageCollection> SearchResults { get; private set; }

        public string SearchText { get; set; }

        public string SelectedImageSize
        {
            get
            {
                return this.selectedImageSizeField;
            }

            set
            {
                if (this.selectedImageSizeField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedImageSizeField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
                if (this.selectedColorField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedColorField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
                if (this.selectedTypeField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedTypeField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
                if (this.selectedLayoutField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedLayoutField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
                if (this.selectedPeopleField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedPeopleField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
                if (this.selectedDateField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedDateField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
                if (this.selectedLicenseField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedLicenseField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
                if (this.selectedSafeSearchField != value)
                {
                    // Perform a new search with the exiting SearchText term using this new filter
                    this.selectedSafeSearchField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Populate the list of options for Image Size as defined in the Bing Image Search API
        public List<string> ImageSizeList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "Small",
                    "Medium",
                    "Large",
                    "Wallpaper",
                };

                return list;
            }
        }

        // Populate the list of options for Color as defined in the Bing Image Search API
        public List<string> ColorList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "ColorOnly",
                    "Monochrome",
                    "Black",
                    "Blue",
                    "Brown",
                    "Gray",
                    "Green",
                    "Orange",
                    "Pink",
                    "Purple",
                    "Red",
                    "Teal",
                    "White",
                    "Yellow",
                };

                return list;
            }
        }

        // Populate the list of options for Type as defined in the Bing Image Search API
        public List<string> TypeList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "AnimatedGif",
                    "AnimatedGifHttps",
                    "Clipart",
                    "Line",
                    "Photo",
                    "Shopping",
                    "Transparent",
                };

                return list;
            }
        }

        // Populate the list of options for Layout as defined in the Bing Image Search API
        public List<string> LayoutList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "Square",
                    "Wide",
                    "Tall",
                };

                return list;
            }
        }

        // Populate the list of options for People as defined in the Bing Image Search API
        public List<string> PeopleList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "Face",
                    "Portrait",
                };

                return list;
            }
        }

        // Populate the list of options for Freshness as defined in the Bing Image Search API
        public List<string> DateList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "Day",
                    "Week",
                    "Month",
                };

                return list;
            }
        }

        // Populate the list of options for License as defined in the Bing Image Search API
        public List<string> LicenseList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "Any",
                    "Public",
                    "Share",
                    "ShareCommercially",
                    "Modify",
                    "ModifyCommercially",
                };

                return list;
            }
        }

        // Populate the list of options for Safe Search as defined in the Bing Image Search API
        public List<string> SafeSearchList
        {
            get
            {
                var list = new List<string>
                {
                    "Off",
                    "Moderate",
                    "Strict",
                };

                return list;
            }
        }

        // Build the appropriate filter list for the Bing Search API using the current selected filters.
        // Only include the parameter if it is set to something other than the default value.
        private string GetFilters
        {
            get
            {
                var filters = String.Empty;

                if (this.SelectedImageSize != "All")
                {
                    filters += "&size=" + this.SelectedImageSize;
                }

                if (this.SelectedColor != "All")
                {
                    filters += "&color=" + this.SelectedColor;
                }

                if (this.SelectedType != "All")
                {
                    filters += "&imageType=" + this.SelectedType;
                }

                if (this.SelectedLayout != "All")
                {
                    filters += "&aspect=" + this.SelectedLayout;
                }

                if (this.SelectedPeople != "All")
                {
                    filters += "&imageContent=" + this.SelectedPeople;
                }

                if (this.SelectedDate != "All")
                {
                    filters += "&freshness=" + this.SelectedDate;
                }

                if (this.SelectedLicense != "All")
                {
                    filters += "&license=" + this.SelectedLicense;
                }

                if (this.SelectedSafeSearch != "Moderate")
                {
                    filters += "&safeSearch=" + this.SelectedSafeSearch;
                }

                return filters;
            }
        }

        // Called when starting a new search or when changing filters
        public async void DoSearch()
        {
            if (this.SearchResults.Count > 0)
            {
                // For each new search, if a previous search has already generated results, clear those results.
                this.SearchResults.Clear();
            }

            if (String.IsNullOrEmpty(this.SearchText))
            {
                // Ignore requests for searches where no text has been provided in the text box.
                return;
            }

            // Get the collection of image objects returned from the BingImageSearchConnector.
            var imageDataList = await imageSearchConnector.NewImageSearch(this.SearchText, this.GetFilters);

            // Make the collection available in the presentation space.
            foreach (var imageData in imageDataList)
            {
                SearchResults.Add(new ImageCollection { ImageDataCollection = imageData });
            }
        }

        // Called when reaching the bottom of the scroll view
        public void SeeMoreImages()
        {
            // Get the prefetched image objects that have been cached, but not rendered.
            var imageDataList = imageSearchConnector.LoadNextOffset();

            // Add the image objects to be rendered in the presentation space.
            foreach (var imageData in imageDataList)
            {
                SearchResults.Add(new ImageCollection { ImageDataCollection = imageData });
            }
        }

        private void TotalEstimatedMatchesChangedEventHandler(object sender, bool e)
        {
            this.TotalEstimatedMatchesChanged?.Invoke(sender, e);
        }

        
        private void SearchInProgressStatusChangedEventHandler(object sender, bool e)
        {
            this.SearchInProgressStatusChanged?.Invoke(sender, e);
        }
    }
}
