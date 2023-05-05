# SMLHelper
SMLHelper is a complete library that helps you out on your Subnautica modding adventures by making adding new items, changing items, adding models, sprites a LOT easier.  
Check out [the wiki page](https://github.com/SMLHelper/SMLHelper/wiki) for details on how to use it.

## ⚠️Deprecation Notice⚠️
SMLHelper has been deprecated and will no longer receive updates.  
If you're using the latest version of Subnautica, use [Nautilus](https://github.com/SubnauticaModding/Nautilus) instead.  
Nautilus contains all the features SML used to offer and more.  

Both libraries are compatible and can be present simultaneously. However, we encourage modders to update to Nautilus as SML is deprecated. To get started, see the [Updating to Nautilus](https://subnauticamodding.github.io/Nautilus/guides/sml2-to-nautilus.html) guide.

## Contributing
We would love to have people contribute to SMLHelper.  
To get started, first fork the repo and then clone it to your local environment.  

- As of version 2.8, SMLHelper has been updated for QModManager v4.0 and uses [HarmonyX](https://github.com/BepInEx/HarmonyX).

Make sure to use the _publicized_ versions of `Assembly-CSharp.dll` and `Assembly-CSharp-firstpass.dll` (the originals being located in your `Subnautica_Data/Managed` folder).  

To create your own publicized DLLs, install the [BepinEx Plugin](https://github.com/elliotttate/Bepinex-Tools/releases) and run the game once to create the following files:

* `Subnautica_Data\Managed\publicized_assemblies\Assembly-CSharp_publicized.dll`
* `Subnautica_Data\Managed\publicized_assemblies\Assembly-CSharp-firstpass_publicized.dll`

Copy these DLLs into the `Dependencies` folder of SMLHelper so you can build it. Publicized version of the assemblies should make you able to call non-public members without reflection.

- As of version 2.14, SMLHelper developers are required to have .NET 6 installed for the project to build.

Then, load up the solution, make your edits, then create your Pull Request!
