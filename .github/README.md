# HCS.Media.DescriptionManager

<img src="https://github.com/NikRimington/HCS.Media.DescriptionManager/blob/develop/docs/logo.svg" alt="Description Manager Logo" width="250" align="right" />

[![Downloads](https://img.shields.io/nuget/dt/HCS.Media.DescriptionManager?color=cc9900)](https://www.nuget.org/packages/HCS.Media.DescriptionManager/)
[![NuGet](https://img.shields.io/nuget/vpre/HCS.Media.DescriptionManager?color=0273B3)](https://www.nuget.org/packages/HCS.Media.DescriptionManager)
[![GitHub license](https://img.shields.io/github/license/NikRimington/HCS.Media.DescriptionManager?color=8AB803)](LICENSE)

Description Manager is an Umbraco v10+ plugin for helping editors quickly update media items that are missing descriptions.

**What is a description?** That depends, an example could be an *Alt Description* on an Image, it could be a *Title* on a video. It all depends on the site.

**Why use this package?** If your media types in Umbraco has a field for it's "default" description, this package will give editors a quick overview for items where the property is not yet set.

It comes with an easy to use dashboard:

<img alt="Example dashboard" src="https://github.com/NikRimington/HCS.Media.DescriptionManager/blob/develop/docs/screenshots/dashboard.png">

As well as an update to the main media Tree:

<img alt="Example dashboard" src="https://github.com/NikRimington/HCS.Media.DescriptionManager/blob/develop/docs/screenshots/tree.png">

<!--
Including screenshots is a really good idea! 

If you put images into /docs/screenshots, then you would reference them in this readme as, for example:

<img alt="..." src="https://github.com/NikRimington/HCS.Media.DescriptionManager/blob/develop/docs/screenshots/screenshot.png">
-->

## Installation

Add the package to an existing Umbraco website (v10.4+) from nuget:

`dotnet add package HCS.Media.DescriptionManager`

By default the package will work for the following types with the following properties:

- Image : Alt Description (altDescription)
- Vector Graphics : Description (description)
- Video : Title (title)

### Override settings

 It is possible to override the defaults, however it is not possible to override a single item, it is a case of all or nothing. To override the settings add the following to `appsettings.json`.

 ```json
 "HCS": {
    "Media":{
      "DescriptionManager" : {
        "Image": "altText",
        "umbracoMediaVectorGraphics": "title",
        "umbracoMediaVideo": "title"
      }
    }
  }
 ```

 Each property in the Media object represents a Key Value pair of `"Media Type Alias" : "propertyAlias"`.

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## Credits

The logo uses [missing image](hhttps://thenounproject.com/browse/icons/term/missing-image/) from the [Noun Project](https://thenounproject.com) by [MOHAMMED SALIM](https://thenounproject.com/salim.miah24/), licensed under [CC BY 3.0 US](https://creativecommons.org/licenses/by/3.0/us/).
