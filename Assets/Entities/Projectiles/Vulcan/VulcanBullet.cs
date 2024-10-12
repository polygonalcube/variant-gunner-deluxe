using UnityEngine;

public class VulcanBullet : MonoBehaviour
{
    public MoveComponent mov;
    
    void Update()
    {
        mov.Move(Vector3.up);
        mov.ResetZ();
    }
}
