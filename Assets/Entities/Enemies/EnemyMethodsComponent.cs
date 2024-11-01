using UnityEngine;

public class EnemyMethodsComponent : MonoBehaviour
{
    public GameObject FindPlayer(string playerTagName = "Player")
    {
        return GameObject.FindGameObjectWithTag(playerTagName);
    }

    public Vector3 AimAtPlayer(Transform bulletOrigin)
    {
        Transform playerTransform = FindPlayer().transform;
        
        Vector3 currentPos = transform.position;
        Vector3 currentEuler = transform.eulerAngles;
        
        transform.position = new Vector3(bulletOrigin.position.x, bulletOrigin.position.y, 0f);
        transform.LookAt(playerTransform?.transform, Vector3.up);
        Vector3 angleToPlayer = transform.eulerAngles;
        
        transform.position = currentPos;
        transform.eulerAngles = currentEuler;

        return angleToPlayer;
    }
}
