using UnityEngine;

public class ShootComponent : MonoBehaviour
{
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

    GameObject BaseShoot()
    {
        GameObject newBullet = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(0f, 0f, shotAngle));
        newBullet.layer = LayerMask.NameToLayer(layer);
        if (newBullet.TryGetComponent<DestroyOnContact>(out DestroyOnContact destroyOnContact))
        {
            destroyOnContact.layers = destroyOnContactLayers;
        }
        shotTimer = shotTimerSet;
        Destroy(newBullet, destroyTimer);
        return newBullet;
    }
    
    public void Shoot(Vector2 speed)
    {
        if (shotTimer <= 0f)
        {
            GameObject newBullet = BaseShoot();
            if (newBullet.TryGetComponent<MoveComponent>(out MoveComponent moveComponemt))
            {
                moveComponemt.currentSpeedX = speed.x;
                moveComponemt.currentSpeedY = speed.y;
            }
        }
    }

    public void ShootAng(float speed)
    {
        if (shotTimer <= 0f)
        {
            GameObject newBullet = BaseShoot();
            if (newBullet.TryGetComponent<MoveComponent>(out MoveComponent moveComponemt))
            {
                moveComponemt.maximumSpeed.x = speed;
                //moveComponemt.maximumSpeed.y = speed;
            }
        }
    }
}
