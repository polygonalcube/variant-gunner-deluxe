using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelerEnemy : MonoBehaviour
{
    public MoveComponent movEnter;
    public MoveComponent movAttack;
    public ShootComponent gun;
    public Transform player;
    Vector3 playerPos;
    public int state;

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
        if (state == 0)
        {
            movEnter.Move();
            if (player != null)
            {
                if (transform.position.y > -5f)
                {
                    playerPos = player.position;
                    /*
                    Vector3 currentPos = transform.position;
                    Vector3 currentEuler = transform.eulerAngles;
                    //transform.position = new Vector3(vulcan.shotOrigin.position.x, vulcan.shotOrigin.position.y, 0f);
                    transform.LookAt(player.transform, Vector3.up);
                    Vector3 pointer = transform.eulerAngles;
                    transform.position = currentPos;
                    transform.eulerAngles = currentEuler;

                    transform.eulerAngles = new Vector3(0f, 0f, pointer.x + 180f);
                    */

                    Vector3 pointVec = playerPos - transform.position;
                    //pointVec = pointVec.normalized;
                    //Vector3.ClampMagnitude(pointVec, mov.maxSpeed);
                    pointVec = pointVec * (movAttack.maxSpeed/pointVec.magnitude);

                    movAttack.xSpeed = pointVec.x;
                    movAttack.ySpeed = pointVec.y;

                    state++;
                }
            }
        }
        /*
        if (state == 1)
        {
            Vector3 pointVec = playerPos - transform.position;
            //pointVec = pointVec.normalized;
            //Vector3.ClampMagnitude(pointVec, mov.maxSpeed);
            pointVec = pointVec * (movAttack.maxSpeed/pointVec.magnitude);

            movAttack.xSpeed = pointVec.x;
            movAttack.ySpeed = pointVec.y;
            
        }
        */
        if (state == 1)
        {
            movAttack.Move();
            gun.ShootAng(0f);
            //movAttack.MoveAng(Vector3.up);
        }
    }
}
