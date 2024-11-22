using UnityEngine;

public class AreaTwoBackground : MonoBehaviour
{
    /// <summary>
    /// The logic for the scrolling background of Stage 3.
    ///
    /// Background segments move upwards and initiate the spawning of new ones before despawning themselves.
    /// </summary>
    
    [SerializeField] private float scrollSpeed = 15f;
    private bool newSectionSpawned;

    void Update()
    {
        transform.position += new Vector3(0f, scrollSpeed * Time.deltaTime, 0f);
        if (!newSectionSpawned && transform.position.y > 0f)
        {
            Instantiate(gameObject, new Vector3(0f, -40f + transform.position.y, 0f), Quaternion.identity);
            newSectionSpawned = true;
        }

        if (transform.position.y > 33f)
        {
            Destroy(gameObject);
        }
    }
}
