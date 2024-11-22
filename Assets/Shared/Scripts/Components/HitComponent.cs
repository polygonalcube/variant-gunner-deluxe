using UnityEngine;

public class HitComponent : MonoBehaviour
{
    /// <summary>
    /// The hitbox for entities that can deal damage.
    ///
    /// Stores damage values. If it comes into contact with a HurtComponent, the HurtComponent decrements the health of
    /// its HPComponent.
    /// </summary>
    
    public int hitStrength = 1;
}