using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    /// <summary>
    /// The logic for the transitions between Stages 1 & 2 and Stages 2 & 3.
    ///
    /// For the first transition, the background rotates towards the arena of the second boss fight. For the second, the
    /// background moves to create the illusion of progressing towards the hideout.
    /// </summary>
    
    [SerializeField] private Vector3[] levelPositions = { new(-200f, -10f, 500f), new(0f, 0f, 85f), new(0f, 0f, 25f) };
    [SerializeField] private Vector3[] levelEulerAngles = { new(0f, 0f, 0f), new(-90f, 0f, 0f), new(-90f, 0f, 0f) };
    [HideInInspector] public float levelDistance;
    public float transitionSpeed = 0.05f;
    [HideInInspector] public bool transitioningToLevelThree;

    private void Update()
    {
        if (transitioningToLevelThree)
        {
            TransitionToArea(levelPositions[1], levelPositions[2], levelEulerAngles[1], levelEulerAngles[2]);
        }
        else
        {
            TransitionToArea(levelPositions[0], levelPositions[1], levelEulerAngles[0], levelEulerAngles[1]);
        }

        if (GameManager.gm.level >= 2)
        {
            levelDistance += transitionSpeed * Time.deltaTime;
        }
    }

    private void TransitionToArea(Vector3 previousPosition, Vector3 nextPosition, Vector3 previousEulerAngles,
        Vector3 nextEulerAngles)
    {
        transform.position = Vector3.Lerp(previousPosition, nextPosition, levelDistance);
        transform.eulerAngles = Vector3.Slerp(previousEulerAngles, nextEulerAngles, levelDistance);
    }
}
