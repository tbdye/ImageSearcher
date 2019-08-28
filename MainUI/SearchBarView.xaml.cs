using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ImageSearcher.MainUI
{
    public sealed partial class SearchBarView : UserControl
    {
        private SearchBarViewModel ViewModel => DataContext as SearchBarViewModel;

        public SearchBarView()
        {
            this.InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                this.ViewModel.DoSearch.Execute(sender);
            }
        }
    }
}
