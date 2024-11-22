using UnityEngine;

[RequireComponent(typeof(MoveComponent))]

public class BulletBomb : MonoBehaviour
{
    /// <summary>
    /// The Dopple Buster's projectile.
    ///
    /// Moves vertically until its Y position (nearly) matches the player character's. After this, it spawns three rings
    /// of bullets, then despawns.
    /// </summary>
    
    private MoveComponent mover;
    
    public ShootComponent outer;
    public ShootComponent mid;
    public ShootComponent inner;

    private int bulletCount = 20;

    private Transform playerTransform;

    void Awake()
    {
        mover = GetComponent<MoveComponent>();
        
        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject != null)
        {
            playerTransform = GameObject.Find("Player").transform;
        }
    }

    void Update()
    {
        mover.Move(Vector3.zero);
        
        if (playerTransform != null)
        {
            if (Mathf.Abs(transform.position.y - playerTransform.position.y) <= 0.13f)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    outer.shotAngle = 360f/bulletCount*(float)i;
                    outer.ShootAng(outer.bulletSpeedAng);
                    outer.shotTimer = 0f;
                }
                for (int i = 0; i < bulletCount; i++)
                {
                    mid.shotAngle = (360f/bulletCount*((float)i + .5f));
                    mid.ShootAng(mid.bulletSpeedAng);
                    mid.shotTimer = 0f;
                }
                for (int i = 0; i < bulletCount; i++)
                {
                    inner.shotAngle = 360f/bulletCount*(float)i;
                    inner.ShootAng(inner.bulletSpeedAng);
                    inner.shotTimer = 0f;
                }
                Destroy(gameObject);
            }
        }
    }
}
