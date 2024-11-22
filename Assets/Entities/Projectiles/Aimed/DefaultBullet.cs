using UnityEngine;

[RequireComponent(typeof(MoveComponent))]

public class DefaultBullet : MonoBehaviour
{
    /// <summary>
    /// The default bullet for enemies and bosses.
    ///
    /// Movement is dictated by the aforementioned entities' ShootCompoents.
    /// </summary>
    
    private MoveComponent mover;

    void Awake()
    {
        mover = GetComponent<MoveComponent>();
    }
    
    void Update()
    {
        mover.MoveAng(Vector3.up);
        mover.ResetZ();
    }
}
