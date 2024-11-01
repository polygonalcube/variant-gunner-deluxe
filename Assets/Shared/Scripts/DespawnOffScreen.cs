using UnityEngine;

public class DespawnOffScreen : MonoBehaviour
{
    public float[] bounds;

    void Update()
    {
        if (transform.position.x < bounds[0] || transform.position.x > bounds[1] || transform.position.y < bounds[2] ||
            transform.position.y > bounds[3])
        {
            Destroy(gameObject);
        }
    }
}
