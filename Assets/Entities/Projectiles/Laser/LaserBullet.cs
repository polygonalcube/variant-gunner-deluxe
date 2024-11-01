using UnityEngine;

[RequireComponent(typeof(MoveComponent))]

public class LaserBullet : MonoBehaviour
{
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
