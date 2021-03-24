## Displaying Album Cover Images

So we have the Albums showing with the Artist name and Title, however if we can download the Album art and display it, this will really bring the app alive.

Fortunately our buisness logic code provides a convenient `LoadCoverBitmapAsync` this returns a stream that can be used to load a bitmap from.

Firstly open `AlbumViewModel.cs` and add a property of type `Bitmap?` named `Cover` using the usually pattern so that it can notify the UI when it changes. 

```csharp
private Bitmap? _cover;

public Bitmap? Cover
{
    get => _cover;
    private set => this.RaiseAndSetIfChanged(ref _cover, value);
}
```

Next we need to actually load the image data. To do this add the following method.

```csharp
public async Task LoadCover()
{
    await using (var imageStream = await _album.LoadCoverBitmapAsync())
    {
        Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
    }
}
```

Notice that it uses a Task to run the Bitmap loading on a background thread, this is so that the UI thread does not get blocked and make the UI unresponsive, potentially we will be loading many images quickly.

We also use the `Bitmap.DecodeToWidth` method to create a Bitmap object. This is an important insight, if you want to display images in your UI and the jpg, png or whatever format you using provides a very large hight resolution image, but you only want to display it in a small portion of your UI. You need to decode the image so that the `Bitmap` this is displayed is not the full resolution. This `DecodeToWidth` method maintains the aspect ratio and loads to the specified width, efficiently. This means we will not waste huge amounts of memory to show our album covers even if the image files themselves turn out to be quite large.



Now we need to start loading the Album covers whenever a search query has been returned from the backend. Note that because loading the images takes time and is asynchronous, we may need to cancel loading them if the user makes another search request.

Your `AlbumViewModel.cs` should now look like:

```csharp
using Avalonia.Media.Imaging;
using Avalonia.MusicStore.Models;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels
{
    public class AlbumViewModel : ViewModelBase
    {
        private readonly Album _album;
        private Bitmap? _cover;

        public AlbumViewModel(Album album)
        {
            _album = album;
        }

        public string Artist => _album.Artist;

        public string Title => _album.Title;

        public Bitmap? Cover
        {
            get => _cover;
            private set => this.RaiseAndSetIfChanged(ref _cover, value);
        }
    }
}
```

Open `MusicStoreViewModel.cs` and add the following method.

```csharp
private async void LoadCovers(CancellationToken cancellationToken)
{
    foreach (var album in SearchResults.ToList())
    {
        await album.LoadCover();

        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
    }
}
```

Calling this asynchronous method will itereate through each item in the `SearchResults` and call our `AlbumViewModel`s `LoadCover` method.

Notice a `CancellationToken` is used to check if we want to stop loading album covers.

Now add the following code to the beggining of `DoSearch` method of `MusicStoreViewModel` after the `SearchResults.Clear();` line.

```csharp
_cancellationTokenSource?.Cancel();
_cancellationTokenSource = new CancellationTokenSource();
```

This will mean that if there is an existing request still loading Album art, we can cancel them.

Now add the following code to the end of `DoSearch ` method of `MusicStoreViewModel` before the `IsBusy = false;` line.

```c#
if (!_cancellationTokenSource.IsCancellationRequested)
{
    LoadCovers(_cancellationTokenSource.Token);
}
```

`DoSearch` should now look like this.

```csharp
private async void DoSearch(string s)
{
    IsBusy = true;
    SearchResults.Clear();
  
  	_cancellationTokenSource?.Cancel();
    _cancellationTokenSource = new CancellationTokenSource();

    var albums = await Album.SearchAsync(s);

    foreach (var album in albums)
    {
        var vm = new AlbumViewModel(album);

        SearchResults.Add(vm);
    }
    
    if (!_cancellationTokenSource.IsCancellationRequested)
    {
        LoadCovers(_cancellationTokenSource.Token);
    }

    IsBusy = false;
}
```

Ok run the application and search for your favourite artist.

Notice how the Albums covers load one by one, but that the UI remains responsive.

![image-20210310173858088](/Users/danwalmsley/repos/avaloniaui.net/src/AvaloniaUI.Net/wwwroot/docs/advanced-tutorial/images/image-20210310173858088.png)

