using ImageSearcher.Components;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ImageSearcher.MainUI
{
    public class ImageViewModel : UiToolsBase
    {
        private readonly ImageSearchModel imageSearchModel;

        public event EventHandler DisplayImageFullScreenStateChanged;

        public ImageViewModel(ImageSearchModel imageSearchModel)
        {
            this.imageSearchModel = imageSearchModel;

            this.DisplayImageFullScreen = new DelegateCommand(this.DisplayImageFullScreenCommandHandler);
        }

        public ICommand DisplayImageFullScreen { get; }

        public ObservableCollection<ImageCollection> SearchResults => imageSearchModel.SearchResults;

        public string FullScreenImageUri { get; set; }

        internal void SeeMoreImages()
        {
            this.imageSearchModel.SeeMoreImages();
        }

        private void DisplayImageFullScreenCommandHandler(object image)
        {
            this.FullScreenImageUri = image as string;

            this.DisplayImageFullScreenStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
