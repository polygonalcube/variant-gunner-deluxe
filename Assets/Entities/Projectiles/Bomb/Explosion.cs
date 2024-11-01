using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public float duration;
    
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
