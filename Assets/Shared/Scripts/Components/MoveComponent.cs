using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero;
    public float xSpeed;
    public float ySpeed;
    public float maxSpeed;
    public float acceleration;
    public float deceleration;

    public float Accelerate(float speedVar, bool isNegative)
    {
        if (isNegative) return speedVar - acceleration;
        else return speedVar + acceleration;
    }

    public float Decelerate(float speedVar, bool isNegative)
    {
        if (isNegative) return speedVar - acceleration;
        else return speedVar + acceleration;
    }

    public float CheckNearZero(float speedVar)
    {
        if (Mathf.Abs(speedVar) < 0.01f) return 0f;
        else return speedVar;
    }

    public float Cap(float speedVar)
    {
        if (speedVar > maxSpeed) return maxSpeed;
        else if (speedVar < -maxSpeed) return -maxSpeed;
        else return speedVar;
    }

    public void BoundXY(float xBound, float yBound)
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
        else if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
    }

    public void ResetZ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public void Move()
    {
        moveDirection = new Vector3(xSpeed, ySpeed, 0f);
        transform.position += moveDirection * Time.deltaTime;
    }

    public void MoveAng(Vector3 direction)
    {
        transform.position += transform.TransformDirection(direction) * maxSpeed * Time.deltaTime;
    }
}
