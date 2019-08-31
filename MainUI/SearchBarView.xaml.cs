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
                // The content of the searchbox is saved to the SearchBarViewModel with every character press.
                // When the user presses enter, execute the DoSearch command, which is also performed when clicking the UI button.
                this.ViewModel.DoSearch.Execute(sender);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            // Reset the combobox back to the placeholder text when the default value is set.  This is assuming the default value is the first value in the list.
            if (comboBox.SelectedIndex == 0)
            {
                // Because UWP will remember the last selection that was set from the UI, it will never read the property again from the ViewModel.
                // To force the update, it is necessary to update it again through an event.  Doing this as an event occurs after the user has finished making
                // any selections and the UI has refreshed.  This acts as an entirely seperate task from the user's UI interaction.
                // Because this initiates another input on the ComboBox, it is necessary to protect the SelectedItem from being nulled.
                // This is handled in the ViewModel.  Setting the SelectedIndex to -1 is an invalid selection, and forces the ComboBox
                // to default back to the PlaceholderText, which is the intended result.
                comboBox.SelectedIndex = -1;
            }
        }
    }
}
