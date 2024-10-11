using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOffScreen : MonoBehaviour
{
    public float[] bounds;
    
    void Update()
    {
        if (transform.position.x < bounds[0])
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x > bounds[1])
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < bounds[2])
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y > bounds[3])
        {
            Destroy(this.gameObject);
        }
    }
}
