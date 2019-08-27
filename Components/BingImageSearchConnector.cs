using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageSearcher.Components
{
    public class BingImageSearchConnector
    {
        private const int count = 50;
        private const string subscriptionKey = "7e18a9a770e7437185aec8841d3dc83d";
        private const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/images/search";

        public BingImageSearchConnector()
        {
            this.PrefetchedImageSearchResults = new List<ImageMetadata>();
        }

        public int TotalEstimatedMatches { get; private set; }

        public int RemainingEstimatedMatches
        {
            get
            {
                return this.TotalEstimatedMatches - this.Offset;
            }
        }

        private int Offset { get; set; }

        private string Query { get; set; }

        private string Filters { get; set; }

        private List<ImageMetadata> PrefetchedImageSearchResults { get; set; }

        public async Task<IEnumerable<ImageMetadata>> NewImageSearch(string query, string filters)
        {
            var metadataResults = new List<ImageMetadata>();

            // Reset properties
            this.PrefetchedImageSearchResults.Clear();
            this.TotalEstimatedMatches = 0;
            this.Offset = 0;
            this.Query = query?.Trim() + "&count=" + count;
            this.Filters = filters;

            // Send a search request
            var jsonResult = await BingImageSearch(this.Query + this.Filters);

            if (jsonResult == null)
            {
                return metadataResults;
            }

            // Deserialize the JSON response from the Bing Image Search API
            dynamic jsonObject = JObject.Parse(jsonResult);

            if (jsonObject.value.Count == 0)
            {
                return metadataResults;
            }

            // Set properties from the JSON response for image processing
            this.TotalEstimatedMatches = jsonObject.totalEstimatedMatches;
            var lastIndex = this.TotalEstimatedMatches < count ? this.TotalEstimatedMatches : count;

            // Collect image metadata for display
            this.SetImageMetadata(metadataResults, jsonObject, lastIndex);

            // Reset the offset for the next prefetch
            this.Offset = jsonObject.nextOffset;

            // Prefetch the next page of results, but don't load them yet.
            this.PrefetchNextImageSearchResults();

            return metadataResults;
        }

        public IEnumerable<ImageMetadata> LoadNextOffset()
        {
            var imageMetadata = new List<ImageMetadata>(this.PrefetchedImageSearchResults);

            Task.Run(() => this.PrefetchNextImageSearchResults());

            return imageMetadata; 
        }

        private async void PrefetchNextImageSearchResults()
        {
            this.PrefetchedImageSearchResults.Clear();

            // Format the query to display the next set of search results
            var offset = "&offset=" + this.Offset;

            // Send a search request
            var jsonResult = await BingImageSearch(this.Query + offset + this.Filters);

            if (jsonResult == null)
            {
                return;
            }

            // Deserialize the JSON response from the Bing Image Search API
            dynamic jsonObject = JObject.Parse(jsonResult);

            // Set properties from the JSON response for image processing
            var lastIndex = this.RemainingEstimatedMatches < count ? this.RemainingEstimatedMatches : count;

            // Collect image metadata for display
            this.SetImageMetadata(this.PrefetchedImageSearchResults, jsonObject, lastIndex);

            // Reset the offset for the next prefetch
            this.Offset = jsonObject.nextOffset;
        }

        private void SetImageMetadata(List<ImageMetadata> list, dynamic jsonObject, int lastIndex)
        {
            for (int resultIndex = 0; resultIndex < lastIndex; resultIndex++)
            {
                list.Add(new ImageMetadata
                {
                    Id = jsonObject.value[resultIndex].imageId,
                    Name = jsonObject.value[resultIndex].name,
                    ThumbnailUrl = jsonObject.value[resultIndex].thumbnailUrl,
                    Width = jsonObject.value[resultIndex].thumbnail.width,
                    Height = jsonObject.value[resultIndex].thumbnail.height,
                    ContentUrl = jsonObject.value[resultIndex].contentUrl,
                });
            }
        }

        private static async Task<string> BingImageSearch(string searchQuery)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {

                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Construct the URI of the search request
                var uriQuery = uriBase + "?q=" + searchQuery;

                // Perform the Web request and get the response
                response = await client.GetAsync(uriQuery);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
            }

            // Create result object for return
            return await response?.Content.ReadAsStringAsync();
        }
    }
}
