using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    public MoveComponent mov;
    
    void Update()
    {
        mov.MoveAng(Vector3.up);
    }
}
