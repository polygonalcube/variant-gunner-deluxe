using UnityEngine;

public class EnemyMethodsComponent : MonoBehaviour
{
    public GameObject FindPlayer(string playerName = "Player")
    {
        GameObject player = null;
        player = GameObject.Find(playerName);
        return player;
    }
}
