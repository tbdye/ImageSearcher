---
topic: sample
languages:
- csharp
products:
- windows
---

# Bing ImageSearcher App
My goal was to learn UWP app development, integration of REST API calls, and build an app that used them, all in one week.  The Bing ImageSearcher App is the result.   This app allows you to search for images using the Bing Image Search API from Azure, filter results, view the results as a scrollable collection, and click the one you like to see it full screen.  Documentation about the API can be found [here](https://docs.microsoft.com/en-us/rest/api/cognitiveservices-bingsearch/bing-images-api-v7-reference).  This app also makes use of tooling provided by the [Windows Community Toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit).

## Features

### Design Features
* Search box for Bing Image Search
* Selectable image search filters
* Masonry-like layout for image results
* On-demand loading of image results for infinite scrolling
* Responsive design for multiple screen sizes and layouts
* Full screen image preview support
* Loading animations
* Error text for no results found

### Application Features
* Universal Windows Platform UWP application
* Model-View-ViewModel MVVM design architecture
* Utilizes command delegates and events
* Centralized resource dictionary for styles
* Asynchronous loading for REST API calls to Bing Image Search using JSON response objects
* Results are prefetched on-demand asynchronously in batches and cached to reduce image load wait times

## Structure
This program makes use of the MVVM design architecture separating presentation, application logic, and application data.  The App.xaml codebehind navigates to the MainPageView as the main page of the application, which initializes the MainPageViewModel as the data context, which in turn initializes all other classes in the app.  Presentation in View XAML and application logic in ViewModel C# are in the MainUI folder.  Model data and supporting C# classes reside in the Components folder.  App image assets and shared style libraries are in the Design folder.  The MainPageView houses all XAML user controls for the application, such as the SearchBarView and ImageView.  All databinding is to an associated ViewModel to promote unit testing, though this is not implemented.  XAML codebehind is used when it is necessary to work programmatically with XAML control objects and pass their events or properties to the ViewModel.

## Search Result Prefetch
To promote a smoother user experience and reduce the memory footprint, this application makes use of limited data virtualization by loading a predetermined number of results, as configured at compile time in the BingImageSearchConnector class.  After rending the first set of results in the application, the next set of results are prefetched and cached, but not displayed.  When the user scrolls to the bottom of the displayed results, the cached results are rendered and a new set of results are prefetched and cached, but not displayed.  This process repeats until all results have been rendered (through scrolling) or the user initiates a new search.

## Proposed Improvements
As a fully featured app, there are several areas that should be explored:

### Full Data Virtualization
This app, as it is, progressively loads image data in, but does not release it until a new search is performed or the application is closed.  This is because of my decision to use the UWP Windows Toolkit StaggeredPanel to render image results, which I wanted for aesthetics.  While some panel layouts support full data virtualization natively, the more complex panel layouts do not.  The StaggeredPanel will render images left to right, then vertical, in the column with the least vertical height.  The result is a vertical “masonry-like” layout which both looks nice and has very good responsive design.  On the other hand, it’s progressively rendered and entirely dependent on content that came before.  If the content changes, the entire panel will be reordered, which looks chaotic while scrolling.

A true masonry layout is horizontal with a fixed height for each row.  In the case of using a horizontal masonry layout or pure grid, entire rows of results can be purged from view at the top of the scroll buffer as the user reaches the bottom of the scroll buffer.  This way, only images the user is currently or has recently looked at are rendered in memory.  There are two ways to accomplish this:
* Move pictures at the top of the scroll buffer back into a metadata collection where if the user scrolls back up, they will be redownloaded; however, the application never requests them again from Bing Image Search.  This is faster, but uses more memory.
* Completely purge the results at the top of the scroll buffer.  If the user scrolls back up, the image search API will be called using the appropriate offset of results in the same manner that images are prefetched from Bing as the user scrolls down.  This is slower, but uses less memory.

The first option is easier to implement and may be more performant, but at the cost of some memory usage.  Be that as it may, an object collection of metadata is still significantly smaller than a fully rendered collection of images.  While infinite scrolling is possible, the Bing Image Search API is limited to 1,000 results for a search term, so while the result collection is large, it is still manageable.  The second option requires more logic for pagination to keep track of the last purged image offset and to queue prefetching of results for scrolling up and down but offers near unlimited scalability.  Both options require a fixed size layout which I did not explore.

### Additional Custom Image Layouts
The Bing Image Search webpage makes use of a horizontal masonry layout which takes advantage of a random assortment of images in different sizes and aspects.  It programmatically zooms and formats the image to fit it to a fixed height in the layout with minimal dead space surrounding it.  Recreating this in UWP procedurally can likely be accomplished by making use of and centering on the Bing Image Search API’s RecognizedEntityRegion to produce good looking results, but some experimentation is required.  This type of processing could be used to create horizontal masonry layouts or pure grids of images with little to no framing.

### Persistent Application Settings
UWP natively supports persistent application settings for remembering filters, custom application behaviors, or enabling login sessions.  This would be a good next step if making the application more feature rich.

### Enhanced Basic Features
The Bing Image Search API has many additional filter controls that I did not implement, but could be incorporated into the UI and application logic.  The filter combo box dropdown menus can be more closely styled to match the web-based counterpart, such as using a color picker tool to filter on colors.  The full screen image view could have left and right navigation buttons to page through the image results without leaving full screen mode.  There’s lots of opportunities for improvements and additions.
