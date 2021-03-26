---
Title: Load data at Startup
Order: 12
---

## Loading Albums on Startup

Our backend code provides a nice way to load the users collection from disk.

Add a method to `MainWindowViewModel.cs` like:

```csharp
private async Task LoadAlbums()
{
    var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

    foreach (var album in albums)
    {
        Albums.Add(album);
    }
    
    foreach (var album in Albums.ToList())
    {
        await album.LoadCover();
    }
}
```

As you can see it firstly uses the buisness logic apis to load the list of `Albums`. It then transforms each one into an `AlbumViewModel`. After this we add each `AlbumViewModel` instance to the `ObservableCollection` of `Albums`, this will instantly updat the UI.

Note we then re-iterate over the `Albums` and asynchronously load each cover. Note that we do this after adding all the albums to the list, as its more important to quickly show the user all the albums available and then load the images. 

![image-20210310184202271](/docs/advanced-tutorial/images/image-20210310184202271.png)



