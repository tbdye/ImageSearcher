using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher.Components
{
    public class ImageSearchModel
    {
        public ImageSearchModel(ImageContentModel imageContentModel)
        {

        }

        public string SearchText { get; set; }

        public List<string> ImageSizeList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }

        public List<string> ColorList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }

        public List<string> TypeList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }

        public List<string> LayoutList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }

        public List<string> PeopleList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }

        public List<string> DateList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }

        public List<string> LicenseList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }

        public List<string> SafeSearchList
        {
            get
            {
                var list = new List<string>
                {
                    "All",
                    "One",
                    "Two",
                    "Three",
                };

                return list;
            }
        }
    }
}
