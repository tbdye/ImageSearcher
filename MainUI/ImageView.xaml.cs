using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ImageSearcher.MainUI
{
    public sealed partial class ImageView : UserControl
    {
        private ImageViewModel ViewModel => DataContext as ImageViewModel;

        public ImageView()
        {
            this.InitializeComponent();
        }

        private void Scroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            if (scrollViewer.VerticalOffset  >= scrollViewer.ScrollableHeight)
            {
                // When the user reaches the bottom of the scrollbar, trigger a command to load more image results into view.
                this.ViewModel.SeeMoreImages();
            }
        }
    }
}
