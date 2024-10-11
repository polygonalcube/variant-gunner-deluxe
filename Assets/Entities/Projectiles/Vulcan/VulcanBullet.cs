using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanBullet : MonoBehaviour
{
    public MoveComponent mov;
    
    void Update()
    {
        mov.Move();
        mov.ResetZ();
    }
}
