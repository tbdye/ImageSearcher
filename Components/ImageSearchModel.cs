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

        public async void DoSearch()
        {
            var query = this.SearchText?.Trim();

            if (this.SearchResults.Count > 0)
            {
                this.SearchResults.Clear();
            }

            if (String.IsNullOrEmpty(query))
            {
                return;
            }

            var results = await ImageSearch(query);

            foreach (var result in results)
            {
                SearchResults.Add(new ImageCollection { ImageMetaDataCollection = result });
            }
        }

        private async Task<IEnumerable<ImageMetaData>> ImageSearch(string query)
        {
            var results = new List<ImageMetaData>();
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7e18a9a770e7437185aec8841d3dc83d");

            // Request parameters
            var count = 50;
            var offset = 0;
            var mkt = "en-us";

            var ImgSearchEndPoint = "https://imagesearcherapp.cognitiveservices.azure.com/bing/v7.0/images/search?";

            var request = string.Format("{0}q={1}&count={2}&offset={3}&mkt={4}",
                ImgSearchEndPoint,
                WebUtility.UrlEncode(query),
                count.ToString(),
                offset.ToString(),
                mkt);

            var result = await client.GetAsync(request + this.GetFilters());

            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();

            dynamic data = JObject.Parse(json);

            for (int i = offset; i < count; i++)
            {
                try
                {
                    results.Add(new ImageMetaData
                    {
                        Name = data.value[i].name,
                        ContentUrl = data.value[i].contentUrl,
                    });
                }
                catch
                {
                    break;
                }
            }

            return results;
        }

        private string GetFilters()
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
}
