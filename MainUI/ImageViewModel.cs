﻿using ImageSearcher.Components;
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
        private readonly ImageSearchModel imageSearchModel;

        public ImageViewModel(ImageSearchModel imageSearchModel)
        {
            this.imageSearchModel = imageSearchModel;
        }

        public ObservableCollection<ImageCollection> SearchResults => imageSearchModel.SearchResults;

        internal void SeeMoreImages()
        {
            this.imageSearchModel.SeeMoreImages();
        }
    }
}
