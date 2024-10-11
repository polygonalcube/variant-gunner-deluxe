using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    //make enemy named spooker
    public GameObject bullet;
    public Transform shotOrigin;
    public float shotTimer;
    public float shotTimerSet;
    public float shotAngle;
    public Vector2 bulletSpeed;
    public float bulletSpeedAng;
    public float destroyTimer;
    public string layer;
    public LayerMask destroyOnContactLayers;
    
    void Update()
    {
        shotTimer -= Time.deltaTime;
    }
    
    public void Shoot(Vector2 speed)
    {
        if (shotTimer <= 0f)
        {
            GameObject newBullet = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(0f, 0f, shotAngle));
            newBullet.GetComponent<MoveComponent>().xSpeed = speed.x;
            newBullet.GetComponent<MoveComponent>().ySpeed = speed.y;
            newBullet.layer = LayerMask.NameToLayer(layer);
            newBullet.GetComponent<DestroyOnContact>().layers = destroyOnContactLayers;
            shotTimer = shotTimerSet;
            Destroy(newBullet, destroyTimer);
        }
    }

    public void ShootAng(float speed)
    {
        if (shotTimer <= 0f)
        {
            GameObject newBullet = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(0f, 0f, shotAngle));
            newBullet.GetComponent<MoveComponent>().maxSpeed = speed;
            newBullet.layer = LayerMask.NameToLayer(layer);
            shotTimer = shotTimerSet;
            Destroy(newBullet, destroyTimer);
        }
    }
}
