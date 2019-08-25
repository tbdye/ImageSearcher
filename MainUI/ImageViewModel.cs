using ImageSearcher.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher.MainUI
{
    public class ImageViewModel : UiToolsBase
    {
        private ImageContentModel imageContentModel;

        public ImageViewModel(ImageContentModel imageContentModel)
        {
            this.imageContentModel = imageContentModel;
        }
    }
}
