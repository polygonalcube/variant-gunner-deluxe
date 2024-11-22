using UnityEngine;

[RequireComponent(typeof(MoveComponent))]

public class LaserBullet : MonoBehaviour
{
    /// <summary>
    /// The player's dual weapon.
    ///
    /// Two projectiles are spawned at once, traveling diagonally and piercing through walls, enemies, and bosses.
    /// </summary>
    
    private MoveComponent mover;

    void Awake()
    {
        mover = GetComponent<MoveComponent>();
    }
    
    void Update()
    {
        mover.MoveAng(Vector3.up);
    }
}
