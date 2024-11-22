using UnityEngine;

public static class UnityExtensions
{
    /// <summary>
    /// New methods to be used for pre-existing classes.
    /// </summary>
    
    public static bool Contains(this LayerMask layerMask, int layer)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
    
    public static bool Contains(this LayerMask layerMask, GameObject gameObject)
    {
        return (layerMask.value & (1 << gameObject.layer)) != 0;
    }
    
    public static bool Contains(this LayerMask layerMask, Collider collider)
    {
        return (layerMask.value & (1 << collider.gameObject.layer)) != 0;
    }
}