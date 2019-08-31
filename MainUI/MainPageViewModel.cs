using ImageSearcher.Components;
using System;
using System.Windows.Input;

namespace ImageSearcher.MainUI
{
    public class MainPageViewModel : UiToolsBase, IDisposable
    {
        private bool isDisposed = false;
        private bool isFullScreenPictureField;

        public MainPageViewModel()
        {
            this.Initialize();

            this.DisableImageFullScreen = new DelegateCommand(this.DisableImageFullScreenCommandHandler);

            this.ImageViewModel.DisplayImageFullScreenStateChanged += this.DisplayImageFullScreenStateChangedEventHandler;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.ImageViewModel.DisplayImageFullScreenStateChanged -= this.DisplayImageFullScreenStateChangedEventHandler;
                }

                this.isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public SearchBarViewModel SearchBarViewModel { get; private set; }

        public ImageViewModel ImageViewModel { get; private set; }

        public ICommand DisableImageFullScreen { get; }

        public bool IsFullScreenPicture
        {
            get
            {
                return this.isFullScreenPictureField;
            }

            set
            {
                this.SetProperty(ref this.isFullScreenPictureField, value);
            }
        }

        public string FullScreenImageUri => this.ImageViewModel.FullScreenImageUri;

        // Initialize other viewmodels and models
        private void Initialize()
        {
            var imageSearchModel = new ImageSearchModel();

            this.SearchBarViewModel = new SearchBarViewModel(imageSearchModel);
            this.ImageViewModel = new ImageViewModel(imageSearchModel);
        }

        private void DisableImageFullScreenCommandHandler(object o)
        {
            this.IsFullScreenPicture = false;
        }

        private void DisplayImageFullScreenStateChangedEventHandler(object o, EventArgs e)
        {
            this.IsFullScreenPicture = true;
            this.OnPropertyChanged(nameof(this.FullScreenImageUri));
        }
    }
}
