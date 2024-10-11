using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public MoveComponent mov;
    public List<Transform> nearbyEnemies = new List<Transform>();
    public Transform nearest;
    public LayerMask layers;

    //public Vector3 deleteThis;
    
    void Update()
    {
        nearbyEnemies.Clear();
        FindGameObjectsWithLayer();
        foreach (Transform enemy in nearbyEnemies)
        {
            if (nearest == null)
            {
                nearest = enemy;
            }
            else if (Vector3.Distance(transform.position, enemy.position) < Vector3.Distance(transform.position, nearest.position))
            {
                nearest = enemy;
            }
        }

        if (nearest != null)
        {
            /*if(nearest.position.x < transform.position.x)
            {
                mov.xSpeed = mov.Accelerate(mov.xSpeed, true);
            }
            else if(nearest.position.x > transform.position.x)
            {
                mov.xSpeed = mov.Accelerate(mov.xSpeed, false);
            }

            if(nearest.position.y < transform.position.y)
            {
                mov.ySpeed = mov.Accelerate(mov.ySpeed, true);
            }
            else if(nearest.position.y > transform.position.y)
            {
                mov.ySpeed = mov.Accelerate(mov.ySpeed, false);
            }*/

            Vector3 pointVec = nearest.position - transform.position;
            //pointVec = pointVec.normalized;
            //Vector3.ClampMagnitude(pointVec, mov.maxSpeed);
            pointVec = pointVec * (mov.maxSpeed/pointVec.magnitude);

            mov.xSpeed = pointVec.x;
            mov.ySpeed = pointVec.y;

            //transform.LookAt(nearest, deleteThis);
        }
        else
        {
            mov.ySpeed = mov.Accelerate(mov.ySpeed, false);
        }

        //caps movement speed
        mov.xSpeed = mov.Cap(mov.xSpeed);
        mov.ySpeed = mov.Cap(mov.ySpeed);

        //Moves the GameObject
        mov.Move();
        mov.ResetZ();
    }

    /*
    void OnTriggerEnter(Collider col)
    {
        if((layers.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer)
        {
            nearbyEnemies.Add(col.gameObject.transform);
            //Debug.Log("Nearest exists!!");
        }
    }
    */

    void FindGameObjectsWithLayer()
    {
        var enemies = FindObjectsOfType<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if((layers.value & 1<<enemy.layer) == 1<<enemy.gameObject.layer)
            {
                nearbyEnemies.Add(enemy.transform);
                //Debug.Log("Nearest exists!!");
            }
        }
    }
    
    /*
    function FindGameObjectsWithLayer (layer : int) : GameObject[] {
        var goArray = FindObjectsOfType(GameObject);
        var goList = new System.Collections.Generic.List.<GameObject>();
        for (var i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0) {
            return null;
        }
        return goList.ToArray();
    }
    */
}
