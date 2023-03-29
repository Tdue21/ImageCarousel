# ImageCarousel

This is a simple web api with a single endpoint for getting a random image. 

The application is set up to look in one or several folders for images, will retrieve one randomly whenever the endpoint is called. 

Once it has been set up, call the endpoint on https://localhost:5001/image.png. 

## Setting up
The file `appsettings.json` contains the necessary settings: 
```json
{
  "AllowedHosts": "*",

  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://0.0.0.0:5000"
      },
      "Https": {
        "Url": "https://0.0.0.0:5001"
      }
    }
  },
  
  "Carousel": {
    "ImagePaths": ["..\\Images"],
    "Extensions": ["*.png"]
  }
}
```
Specifically, the section named *Carousel*. 

* **ImagePaths:** This is a string array of relative or absolute paths to where the applications looks for images. 
* **Extensions:** This is a string array of valid file extensions. Currently, only *.png is supported. 

¤ Background

The reason I made this little application is a [Conan Exiles](https://www.conanexiles.com) mod, 
called [MultiChar](https://steamcommunity.com/sharedfiles/filedetails/?id=2460383547). 

This is a mod that adds a character selection screen to a server, making it possible to have multiple characters 
on a server. It is possible to configure where the background of the character selection screen is located, so 
I thought, why not make a small web api that cycles *through* several images. 

# Future Plans

Nothing has been set in stone, but I would like to support more formats, including video, just like the mod does. 

The mod requires an actual file name, at the end of the path, so right now I am limited by that. 