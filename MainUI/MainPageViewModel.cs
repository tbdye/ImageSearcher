using ImageSearcher.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher.MainUI
{
    public class MainPageViewModel : UiToolsBase
    {
        public MainPageViewModel()
        {
            this.Initialize();
        }

        public SearchBarViewModel SearchBarViewModel { get; private set; }

        public ImageViewModel ImageViewModel { get; private set; }

        private void Initialize()
        {
            var imageSearchModel = new ImageSearchModel();

            this.SearchBarViewModel = new SearchBarViewModel(imageSearchModel);
            this.ImageViewModel = new ImageViewModel(imageSearchModel);
        }
    }
}
