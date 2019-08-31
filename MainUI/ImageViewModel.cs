using ImageSearcher.Components;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ImageSearcher.MainUI
{
    public class ImageViewModel : UiToolsBase, IDisposable
    {
        private readonly ImageSearchModel imageSearchModel;

        private bool isDisposed = false;
        private bool isVisibleNoResultsFoundTextField;
        private bool isLoadingResultsField;

        // Used to block the application's regular UI with an overlay to display a clicked image in full screen.
        public event EventHandler DisplayImageFullScreenStateChanged;

        public ImageViewModel(ImageSearchModel imageSearchModel)
        {
            this.imageSearchModel = imageSearchModel;

            this.DisplayImageFullScreen = new DelegateCommand(this.DisplayImageFullScreenCommandHandler);

            this.imageSearchModel.TotalEstimatedMatchesChanged += this.TotalEstimatedMatchesChangedEventHandler;
            this.imageSearchModel.SearchInProgressStatusChanged += this.SearchInProgressStatusChangedEventHandler;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.imageSearchModel.TotalEstimatedMatchesChanged -= this.TotalEstimatedMatchesChangedEventHandler;
                    this.imageSearchModel.SearchInProgressStatusChanged -= this.SearchInProgressStatusChangedEventHandler;
                }

                this.isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public ICommand DisplayImageFullScreen { get; }

        public ObservableCollection<ImageCollection> SearchResults => imageSearchModel.SearchResults;

        public string FullScreenImageUri { get; set; }

        public bool IsVisibleNoResultsFoundText
        {
            get
            {
                return this.isVisibleNoResultsFoundTextField; 
            }

            set
            {
                this.SetProperty(ref this.isVisibleNoResultsFoundTextField, value);
            }
        }

        public bool IsLoadingResults
        {
            get
            {
                return this.isLoadingResultsField;
            }

            set
            {
                this.SetProperty(ref isLoadingResultsField, value);
            }
        }


        public string SearchText => this.imageSearchModel.SearchText;

        // Triggered from the ImageView codebehind when reaching the bottom of the scrollbar.  Loads more images for infinite scrolling.
        internal void SeeMoreImages()
        {
            this.imageSearchModel.SeeMoreImages();
        }

        private void DisplayImageFullScreenCommandHandler(object image)
        {
            this.FullScreenImageUri = image as string;

            this.DisplayImageFullScreenStateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void TotalEstimatedMatchesChangedEventHandler(object sender, bool e)
        {
            var showError = !string.IsNullOrEmpty(this.SearchText) && !e;

            this.OnPropertyChanged(nameof(this.SearchText));

            this.IsVisibleNoResultsFoundText = showError;
        }

        private void SearchInProgressStatusChangedEventHandler(object sender, bool e)
        {
            this.IsLoadingResults = e;
        }
    }
}
