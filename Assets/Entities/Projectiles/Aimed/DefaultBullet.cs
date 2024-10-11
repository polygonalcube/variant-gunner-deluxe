using System.Collections;
using System.Collections.Generic;
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
