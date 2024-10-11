using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeelineEnemy : MonoBehaviour
{
    public MoveComponent mov;
    public ShootComponent gun;
    public Transform player;

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
        ShootAtPlayer();
        mov.Move();
    }

    void ShootAtPlayer()
    {
        if (player != null)
        {
            Vector3 currentPos = transform.position;
            Vector3 currentEuler = transform.eulerAngles;
            transform.position = new Vector3(gun.shotOrigin.position.x, gun.shotOrigin.position.y, 0f);
            transform.LookAt(player.transform, Vector3.up);
            Vector3 pointer = transform.eulerAngles;
            transform.position = currentPos;
            transform.eulerAngles = currentEuler;

            gun.shotAngle = pointer.x - 270f;
            if (player.position.x > gun.shotOrigin.position.x)
            {
                gun.shotAngle = -gun.shotAngle;
            }
            gun.ShootAng(gun.bulletSpeedAng);
        }
    }
}
