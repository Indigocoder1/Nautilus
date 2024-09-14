using System.Reflection;
using UnityEngine;

namespace Nautilus.Utility.ThunderkitUtilities;

internal class ApplySNLayer : MonoBehaviour
{
    [Tooltip("The name of the layer you want to apply")]
    public LayerName layerName;

    [Tooltip("How to apply the layer")]
    public GeneralSetMode layerSetMode;

    private void Start()
    {
        int layer = layerName switch
        {
            LayerName.Default => LayerID.Default,
            LayerName.Useable => LayerID.Useable,
            LayerName.NotUseable => LayerID.NotUseable,
            LayerName.Player => LayerID.Player,
            LayerName.TerrainCollider => LayerID.TerrainCollider,
            LayerName.UI => LayerID.UI,
            LayerName.Trigger => LayerID.Trigger,
            LayerName.BaseClipProxy => LayerID.BaseClipProxy,
            LayerName.OnlyVehicle => LayerID.OnlyVehicle,
            LayerName.Vehicle => LayerID.Vehicle,
#if SUBNAUTICA
            LayerName.DefaultCollisionMask => LayerID.DefaultCollisionMask,
            LayerName.SubRigidbodyExclude => LayerID.SubRigidbodyExclude,
#elif BELOWZERO          
            LayerName.Interior => LayerID.Interior,
            LayerName.AllowPlayerAndVehicle => LayerID.AllowPlayerAndVehicle,
#endif
            _ => 0
        };

        switch(layerSetMode)
        {
            case GeneralSetMode.SingleObject:
                gameObject.layer = layer;
                break;
            case GeneralSetMode.AllChildObjects:
                GetComponentsInChildren<GameObject>().ForEach(g => g.layer = layer);
                break;
            case GeneralSetMode.AllChildObjectsIncludeInactive:
                GetComponentsInChildren<GameObject>(true).ForEach(g => g.layer = layer);
                break;
        }
    }

    /// <summary>
    /// These are taken from <see cref="LayerID"/>, and are retrieved using reflection
    /// </summary>
    public enum LayerName
    {
        Default,
        Useable,
        NotUseable,
        Player,
        TerrainCollider,
        UI,
        Trigger,
        BaseClipProxy,
        OnlyVehicle,
        Vehicle,
#if SUBNAUTICA
        DefaultCollisionMask,
        SubRigidbodyExclude,
#elif BELOWZERO
        Interior,
        AllowPlayerAndVehicle
#endif
    }
}
