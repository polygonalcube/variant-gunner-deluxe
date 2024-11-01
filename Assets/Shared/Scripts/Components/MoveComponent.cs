using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public float currentSpeedX;
    public float currentSpeedY;
    public float currentSpeedZ;
    
    public Vector3 maximumSpeed = new Vector3(10f, 10f, 10f);
    
    public float acceleration;
    public float deceleration;

    public void Accelerate(ref float speedVar, float direction)
    {
        speedVar += acceleration * direction;
    }

    public void Decelerate(ref float speedVar)
    {
        speedVar += deceleration * Mathf.Sign(-speedVar);
        if (Mathf.Abs(speedVar) <= deceleration)
        {
            speedVar = 0f;
        }
    }

    public void Cap(ref float speedVar, float speedCap, float direction)
    {
        bool maxSpeedExceeded = Mathf.Abs(speedVar) > speedCap;
        bool maxSpeedExceededWithLessThanFullDirectionalInput = 
            Mathf.Abs(speedVar) > speedCap * Mathf.Abs(direction) && Mathf.Abs(direction) != 0f;

        if (maxSpeedExceeded)
        {
            speedVar = speedCap * Mathf.Sign(direction);
        }
        else if (maxSpeedExceededWithLessThanFullDirectionalInput)
        {
            bool directionalInputMagnitudeLoweredFromPrevious =
                Mathf.Abs(speedVar) > speedCap * Mathf.Abs(direction) + acceleration;
            
            if (directionalInputMagnitudeLoweredFromPrevious)
            {
                Decelerate(ref speedVar);
            }
            else
            {
                speedVar = speedCap * direction;
            }
        }
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

    public void Move(Vector3 moveDirection)
    {
        if (Mathf.Abs(moveDirection.x) != 0f)
        {
            Accelerate(ref currentSpeedX, moveDirection.x);
        }
        else
        {
            Decelerate(ref currentSpeedX);
        }
        
        if (Mathf.Abs(moveDirection.y) != 0f)
        {
            Accelerate(ref currentSpeedY, moveDirection.y);
        }
        else
        {
            Decelerate(ref currentSpeedY);
        }
        
        if (Mathf.Abs(moveDirection.z) != 0f)
        {
            Accelerate(ref currentSpeedZ, moveDirection.z);
        }
        else
        {
            Decelerate(ref currentSpeedZ);
        }
            
        Cap(ref currentSpeedX, maximumSpeed.x, moveDirection.x);
        Cap(ref currentSpeedY, maximumSpeed.y, moveDirection.y);
        Cap(ref currentSpeedZ, maximumSpeed.z, moveDirection.z);
	
        transform.position += new Vector3(currentSpeedX, currentSpeedY, currentSpeedZ) * Time.deltaTime;
    }

    public void MoveAng(Vector3 direction)
    {
        transform.position += transform.TransformDirection(direction) * maximumSpeed.x * Time.deltaTime;
    }
}
