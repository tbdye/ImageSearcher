using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher.Components
{
    public class ImageSearchModel
    {
        private readonly BingImageSearchConnector imageSearchConnector;

        private string selectedImageSizeField = "All";
        private string selectedColorField = "All";
        private string selectedTypeField = "All";
        private string selectedLayoutField = "All";
        private string selectedPeopleField = "All";
        private string selectedDateField = "All";
        private string selectedLicenseField = "All";
        private string selectedSafeSearchField = "Moderate";

        public event EventHandler FilterStateChanged;

        public ImageSearchModel()
        {
            this.imageSearchConnector = new BingImageSearchConnector();

            this.SearchResults = new ObservableCollection<ImageCollection>();
        }

        public ObservableCollection<ImageCollection> SearchResults { get; private set; }

        public string SearchText { get; set; }

        public string SelectedImageSize
        {
            get
            {
                return selectedImageSizeField;
            }

            set
            {
                if (this.selectedImageSizeField != value)
                {
                    this.selectedImageSizeField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string SelectedColor
        {
            get
            {
                return selectedColorField;
            }

            set
            {
                if (this.selectedColorField != value)
                {
                    this.selectedColorField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string SelectedType
        {
            get
            {
                return selectedTypeField;
            }

            set
            {
                if (this.selectedTypeField != value)
                {
                    this.selectedTypeField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string SelectedLayout
        {
            get
            {
                return selectedLayoutField;
            }

            set
            {
                if (this.selectedLayoutField != value)
                {
                    this.selectedLayoutField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string SelectedPeople
        {
            get
            {
                return selectedPeopleField;
            }

            set
            {
                if (this.selectedPeopleField != value)
                {
                    this.selectedPeopleField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string SelectedDate
        {
            get
            {
                return selectedDateField;
            }

            set
            {
                if (this.selectedDateField != value)
                {
                    this.selectedDateField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string SelectedLicense
        {
            get
            {
                return selectedLicenseField;
            }

            set
            {
                if (this.selectedLicenseField != value)
                {
                    this.selectedLicenseField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string SelectedSafeSearch
        {
            get
            {
                return selectedSafeSearchField;
            }

            set
            {
                if (this.selectedSafeSearchField != value)
                {
                    this.selectedSafeSearchField = value;
                    this.FilterStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

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

        public async void DoSearch()
        {
            if (this.SearchResults.Count > 0)
            {
                this.SearchResults.Clear();
            }

            if (String.IsNullOrEmpty(this.SearchText))
            {
                return;
            }

            var imageDataList = await imageSearchConnector.NewImageSearch(this.SearchText, this.GetFilters);

            foreach (var imageData in imageDataList)
            {
                SearchResults.Add(new ImageCollection { ImageDataCollection = imageData });
            }
        }

        public void SeeMoreImages()
        {
            var imageDataList = imageSearchConnector.LoadNextOffset();

            foreach (var imageData in imageDataList)
            {
                SearchResults.Add(new ImageCollection { ImageDataCollection = imageData });
            }
        }
    }
}
