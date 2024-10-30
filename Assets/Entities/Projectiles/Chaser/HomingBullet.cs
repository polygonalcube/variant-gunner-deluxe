using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public MoveComponent mov;
    public List<Transform> nearbyEnemies = new();
    public Transform nearest;
    public LayerMask layers;
    
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

        if (nearest != null && nearest.gameObject.activeSelf)
        {
            Vector3 pointVec = nearest.position - transform.position;
            pointVec *= mov.maximumSpeed.x/pointVec.magnitude;

            mov.currentSpeedX = pointVec.x;
            mov.currentSpeedY = pointVec.y;
            mov.Move(Vector3.zero);//

            transform.LookAt(nearest);
        }
        else
        {
            transform.eulerAngles = new Vector3(-90f, 90f, 0f);
            
            mov.currentSpeedY = mov.maximumSpeed.y;
            mov.Move(Vector3.up);
        }
        mov.ResetZ();
    }

    void FindGameObjectsWithLayer()
    {
        var enemies = FindObjectsOfType<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (layers.Contains(enemy))
            {
                nearbyEnemies.Add(enemy.transform);
            }
        }
    }
}
