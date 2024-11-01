using UnityEngine;

[RequireComponent(typeof(MoveComponent))]

public class VulcanBullet : MonoBehaviour
{
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
