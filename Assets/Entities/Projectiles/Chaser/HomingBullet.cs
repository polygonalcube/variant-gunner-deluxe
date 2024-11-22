using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveComponent))]

public class HomingBullet : MonoBehaviour
{
    /// <summary>
    /// The player's homing weapon.
    ///
    /// The projectile moves towards the closest enemy or boss. If no enemies exist, the bullets move upwards.
    /// </summary>
    
    private MoveComponent mover;
    private List<Transform> nearbyEnemies = new();
    private Transform nearestTarget;
    public LayerMask layers;

    void Awake()
    {
        mover = GetComponent<MoveComponent>();
    }
    
    void Update()
    {
        nearbyEnemies.Clear();
        FindGameObjectsWithLayer();
        foreach (Transform enemy in nearbyEnemies)
        {
            if (nearestTarget == null)
            {
                nearestTarget = enemy;
            }
            else if (Vector3.Distance(transform.position, enemy.position) < Vector3.Distance(transform.position, nearestTarget.position))
            {
                nearestTarget = enemy;
            }
        }

        if (nearestTarget != null && nearestTarget.gameObject.activeSelf)
        {
            Vector3 pointVec = nearestTarget.position - transform.position;
            pointVec *= mover.maximumSpeed.x/pointVec.magnitude;

            mover.currentSpeedX = pointVec.x;
            mover.currentSpeedY = pointVec.y;
            mover.Move(Vector3.zero);

            transform.LookAt(nearestTarget);
        }
        else
        {
            transform.eulerAngles = new Vector3(-90f, 90f, 0f);
            
            mover.currentSpeedY = mover.maximumSpeed.y;
            mover.Move(Vector3.up);
        }
        mover.ResetZ();
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
