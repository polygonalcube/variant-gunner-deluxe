using System.Collections;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    /// <summary>
    /// A component that allows an entity to despawn on contact with objects in certain layers.
    /// </summary>
    
    public LayerMask layers;
    public bool willDie;

    void Start()
    {
        willDie = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (layers.Contains(col))
        {
            StartCoroutine(DestroyNextFrame());
        }
    }

    IEnumerator DestroyNextFrame()
    {
        willDie = true;
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
