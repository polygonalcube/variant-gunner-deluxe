using UnityEngine;

public class Explosion : MonoBehaviour
{
    /// <summary>
    /// The player bomb's explosion.
    ///
    /// Spawns when a player bomb comes in contact with an enemy, boss, or wall. Lasts for a certain amount of time
    /// before despawning.
    /// </summary>
    
    [HideInInspector] public float duration;
    
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
