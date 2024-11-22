using UnityEngine;

[RequireComponent(typeof(MoveComponent))]

public class VulcanBullet : MonoBehaviour
{
    /// <summary>
    /// The player's vulcan weapon.
    ///
    /// The projectile travels straight upwards at a fast speed.
    /// </summary>
    
    private MoveComponent mover;

    void Awake()
    {
        mover = GetComponent<MoveComponent>();
    }
    
    void Update()
    {
        mover.Move(Vector3.up);
        mover.ResetZ();
    }
}
