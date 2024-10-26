using UnityEngine;

public class EnemyMethodsComponent : MonoBehaviour
{
    public GameObject FindPlayer(string playerTagName = "Player")
    {
        return GameObject.FindGameObjectWithTag(playerTagName);
    }
}
