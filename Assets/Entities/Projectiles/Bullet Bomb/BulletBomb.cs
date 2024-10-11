using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBomb : MonoBehaviour
{
    public MoveComponent mov;
    public ShootComponent outer;
    public ShootComponent mid;
    public ShootComponent inner;

    public int bulletCount;

    Transform player;

    void Start()
    {
        GameObject goPlayer = GameObject.Find("Player");
        if (goPlayer != null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    void Update()
    {
        mov.Move();
        if (player != null)
        {
            if (transform.position.y < player.position.y)
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
                Destroy(this.gameObject);
            }
        }
    }
}
