using System.Collections;
using UnityEngine;
using UWE;

#if SUBNAUTICA
namespace Nautilus.Utility;

public static partial class MaterialUtils
{
    private static IEnumerator PatchInternal()
    {
        yield return LoadIonCubeMaterial();
        yield return LoadPrecursorGlassAndFogMaterial();
        yield return LoadStasisFieldMaterial();
        yield return LoadAirWaterBarrierMaterial();
        yield return LoadForcefieldMaterial();
        yield return LoadGhostMaterial();
    }

    /// <summary>
    /// Gets the default Ion Cube Material.
    /// </summary>
    public static Material IonCubeMaterial { get; private set; }

    /// <summary>
    /// Gets the Precursor Glass Material.
    /// </summary>
    public static Material PrecursorGlassMaterial { get; private set; }

    /// <summary>
    /// Gets the Stasis Rifle's ball Material.
    /// </summary>
    public static Material StasisFieldMaterial { get; private set; }

    /// <summary>
    /// Gets the Precursor Force Field Material.
    /// </summary>
    public static Material ForceFieldMaterial { get; private set; }

    /// <summary>
    /// Gets the Material used by Alien Bases for the transition between water and air.
    /// </summary>
    public static Material AirWaterBarrierMaterial { get; private set; }
    
    /// <summary>
    /// Gets the material used by unfinished constructions.
    /// </summary>
    public static Material GhostMaterial { get; private set; }

    /// <summary>
    /// Gets the material used by the UI in the Cyclops
    /// </summary>
    public static Material HolographicUIMaterial { get; private set; }

    private static IEnumerator LoadIonCubeMaterial()
    {
        if (IonCubeMaterial)
            yield break;

        var task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
        yield return task;

        var ionCube = task.GetResult();
        IonCubeMaterial = ionCube.GetComponentInChildren<MeshRenderer>().material;
    }

    private static IEnumerator LoadAirWaterBarrierMaterial()
    {
        if (AirWaterBarrierMaterial)
            yield break;

        var task = PrefabDatabase.GetPrefabAsync("8b5e6a02-533c-44cb-9f34-d2773aa82dc4");
        yield return task;

        if (task.TryGetPrefab(out var prefab))
        {
            AirWaterBarrierMaterial = prefab.GetComponentInChildren<MeshRenderer>().material;
        }
    }

    private static IEnumerator LoadStasisFieldMaterial()
    {
        if (StasisFieldMaterial)
            yield break;

        var task = CraftData.GetPrefabForTechTypeAsync(TechType.StasisRifle);
        yield return task;

        var stasisRifle = task.GetResult();
        StasisFieldMaterial = stasisRifle.GetComponent<StasisRifle>().effectSpherePrefab.GetComponentInChildren<Renderer>().materials[1];
    }

    private static IEnumerator LoadForcefieldMaterial()
    {
        if (ForceFieldMaterial)
            yield break;

        var task = PrefabDatabase.GetPrefabAsync("b7ec7d50-186b-4656-9cc6-7dd503d14d98");
        yield return task;

        if (task.TryGetPrefab(out var prefab))
        {
            ForceFieldMaterial = prefab.GetComponentInChildren<Renderer>().material;
        }
    }

    private static IEnumerator LoadPrecursorGlassAndFogMaterial() // precursor glass AND fog material which derives from that
    {
        if (PrecursorGlassMaterial)
            yield break;

        var request = PrefabDatabase.GetPrefabAsync("2b43dcb7-93b6-4b21-bd76-c362800bedd1");
        yield return request;

        if (request.TryGetPrefab(out var glassPanel))
        {
            PrecursorGlassMaterial = new Material(glassPanel.GetComponentInChildren<MeshRenderer>().material);
            PrecursorGlassMaterial.SetColor("_Color", new Color(1f, 1f, 1f, 0.7f));
            PrecursorGlassMaterial.SetFloat("_SpecInt", 1f);
        }
    }

    private static IEnumerator LoadGhostMaterial()
    {
        if (GhostMaterial)
            yield break;

        var task = PrefabDatabase.GetPrefabAsync("cf1df719-905c-4385-98da-b638fdfd53f7");
        yield return task;

        if (task.TryGetPrefab(out var wallShelf))
        {
            GhostMaterial = wallShelf.GetComponentInChildren<Constructable>().ghostMaterial;
        }
    }

    private static bool _cyclopsLoaded;

    private static IEnumerator LoadUIMaterial()
    {
        yield return new WaitUntil(() => LightmappedPrefabs.main);

        LightmappedPrefabs.main.RequestScenePrefab("Cyclops", new LightmappedPrefabs.OnPrefabLoaded(OnCyclopsLoaded));

        yield return new WaitUntil(() => _cyclopsLoaded);
    }

    private static void OnCyclopsLoaded(GameObject cyclops)
    {
        var holoMat = cyclops.transform.Find("HelmHUD/HelmHUDVisuals/Canvas_LeftHUD/EngineOnUI/EngineOff_Button")
            .GetComponent<UnityEngine.UI.Image>().material;

        HolographicUIMaterial = new Material(holoMat);
        _cyclopsLoaded = true;
    }
}
#endif