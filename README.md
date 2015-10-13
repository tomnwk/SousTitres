# SousTitres
The word `Sous-titres` is french for subtitles. 

With this program you will be able to download subtitles for your video files without
too much interaction: the idea is to retrieve subtitles by computing the video file hash and querying a provider remote api with it. For the time being, OpenSubtitles Xml-rpc api is being used.

## Execute

From command prompt:

    SousTitres.exe filePath

When subtitles are found, the matching file will be downloaded in the same directory where the video file is and will get the same name (minus the extension).

## Configuration

Edit `SousTitres.exe.config` to change the desired language for your subtitles ([3-letter (ISO 639-2) language code format](https://en.wikipedia.org/wiki/List_of_ISO_639-2_codes))

## Planned features

- Add an installer. During installation, a "download subtitles" action will be added to the Explorer right-click contextual menu!
- Add a fallback option when no subtitles have been found, ie a classic UI enabling a manual way to look for subtitles

## Libs and tools

C# (.NET 4.5), Unity, Nunit, Moq, Visual Studio Community 2015
