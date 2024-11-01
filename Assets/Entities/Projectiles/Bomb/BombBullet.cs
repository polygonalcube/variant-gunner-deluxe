using UnityEngine;

[RequireComponent(typeof(DestroyOnContact))]
[RequireComponent(typeof(MoveComponent))]

public class BombBullet : MonoBehaviour
{
    private MoveComponent mover;
    private DestroyOnContact destroyOnContact;
    public GameObject explosionPrefab;
    [SerializeField] private float explosionDuration = 1f;
    
    void Awake()
    {
        destroyOnContact = GetComponent<DestroyOnContact>();
        mover = GetComponent<MoveComponent>();
    }
    
    void Update()
    {
        mover.Move(Vector3.down);
        
        if (destroyOnContact.willDie)
        {
            GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            newExplosion.GetComponent<Explosion>().duration = explosionDuration;
        }
    }
}
