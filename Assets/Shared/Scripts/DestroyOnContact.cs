using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public LayerMask layers;
    public bool willDie;

    void Start()
    {
        willDie = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if((layers.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer)
        {
            StartCoroutine(DestroyNextFrame());
        }
    }

    IEnumerator DestroyNextFrame()
    {
        willDie = true;
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
}
