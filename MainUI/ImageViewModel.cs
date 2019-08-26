using ImageSearcher.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher.MainUI
{
    public class ImageViewModel : UiToolsBase
    {
        private ImageSearchModel imageSearchModel;

        public ImageViewModel(ImageSearchModel imageSearchModel)
        {
            this.imageSearchModel = imageSearchModel;
        }

        public ObservableCollection<ImageCollection> SearchResults => imageSearchModel.SearchResults;
    }
}
