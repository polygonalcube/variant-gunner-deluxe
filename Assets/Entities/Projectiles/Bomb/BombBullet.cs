using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    public MoveComponent mov;
    public DestroyOnContact doc;
    public GameObject explosion;
    public float exploLen;
    
    void Update()
    {
        mov.Move();
        if (doc.willDie == true)
        {
            GameObject newSplode = Instantiate(explosion, transform.position, Quaternion.identity);
            newSplode.GetComponent<Explosion>().duration = exploLen;
        }
    }
}
