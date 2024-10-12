using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    public MoveComponent mov;
    
    void Update()
    {
        mov.ResetZ();
        mov.MoveAng(Vector3.up);
    }
}
