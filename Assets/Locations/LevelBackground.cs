using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    public Vector3[] levelPos; // 1 = -200,-10,500 // 2 = 0,0,85 // 3 = 0,0,25
    public Vector3[] levelEul; // 1 = 0,0,0 // 2 = -90,0,0
    public float levelDist;
    public float transitionSpd;
    public bool transitionToThree = false;

    void Update()
    {
        if (transitionToThree)
        {
            transform.position = Vector3.Lerp(levelPos[1], levelPos[2], levelDist);
            transform.eulerAngles = Vector3.Slerp(levelEul[1], levelEul[2], levelDist);
        }
        else
        {
            transform.position = Vector3.Lerp(levelPos[0], levelPos[1], levelDist);
            transform.eulerAngles = Vector3.Slerp(levelEul[0], levelEul[1], levelDist);
        }

        if (GameManager.gm.level >= 2)
        {
            levelDist += transitionSpd * Time.deltaTime;
        }
    }
}
