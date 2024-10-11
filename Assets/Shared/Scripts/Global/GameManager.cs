using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public int score = 0;
    public int[] weaponLevels = new int[] {0, 0, 0, 0};

    public int level = 1;
    int prevLevel = 1;
    public bool destroyAllBullets;

    public LevelBackground bkgd;

    public GameObject[] enemies;
    public GameObject[] bosses;

    public float gameTime;

    public GameObject player;
    public int playerHPTrack;

    void Awake() //Allows for Singleton.
    {
        if (gm != null && gm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        playerHPTrack = 4;
        player = GameObject.Find("Player");
        prevLevel = level;
        destroyAllBullets = false;
        if (level == 1 || level == 2)
            bkgd = GameObject.Find("Background").GetComponent<LevelBackground>();
        SpawnEntities();
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        if (player == null /*|| gameTime < 1f*/)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            playerHPTrack = player.GetComponent<HPComponent>().currenthealth;
            if (player.GetComponent<HPComponent>().currenthealth <= 0)
            {
                StartCoroutine(Reload());
            }
        }
        
        if (destroyAllBullets == true)
        {
            destroyAllBullets = false;
        }
        if (level != prevLevel)
        {
            prevLevel = level;
            gameTime = 0f;
            SpawnEntities();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void SpawnEntities() //Spawns the player and enemies
    {
        gameTime = 0f;
        //TO-DO: Make player a prefab and spawn the player
        if (level == 1) //Decide which enemy spawning coroutine to run, depending on the level
        {
            StartCoroutine(Level1());
        }
        else if (level == 2)
        {
            StartCoroutine(Level2());
        }
        else if (level == 3)
        {
            StartCoroutine(Level3());
        }

        //GetComponent<AudioSource>().Play();
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(3);
        score = 0;
        if (level == 1)
        {
            StopCoroutine("Level1()");
        }
        else if (level == 2)
        {
            StopCoroutine("Level2()");
        }
        else if (level == 3)
        {
            StopCoroutine("Level3()");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (level == 1 || level == 2)
            bkgd = GameObject.Find("Background").GetComponent<LevelBackground>();
        SpawnEntities();
    }

    IEnumerator Level1()
    {
        Instantiate(enemies[0], new Vector3(-6f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-4f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-2f, 10.5f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 2f);
        Instantiate(enemies[0], new Vector3(6f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(4f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(2f, 10.5f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 6f);
        Instantiate(bosses[0], new Vector3(-200f, 0f, 500f), Quaternion.identity);
    }

    IEnumerator Level2()
    {
        Instantiate(enemies[0], new Vector3(-1f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-3f, 7.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-4f, 8f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-5f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-6f, 9f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-7f, 9.5f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 3f);
        Instantiate(enemies[0], new Vector3(1f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(3f, 7.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(4f, 8f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(5f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(6f, 9f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(7f, 9.5f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 6f);
        Instantiate(enemies[0], new Vector3(-1f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-3f, 7.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-4f, 8f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-5f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-6f, 9f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-7f, 9.5f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 9f);
        Instantiate(enemies[0], new Vector3(1f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(3f, 7.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(4f, 8f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(5f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(6f, 9f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(7f, 9.5f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 15f);
        Instantiate(enemies[1], new Vector3(7f, -7f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => bkgd.levelDist >= 1f);
        Instantiate(bosses[1], new Vector3(0f, 0f, -11f), Quaternion.identity);
        yield return new WaitUntil(() => GameObject.Find("Boss 2(Clone)").GetComponent<HPComponent>().currenthealth <= 0f);
        yield return new WaitForSeconds(3);
        bkgd.levelDist = 0f;
        bkgd.transitionSpd *= 3f;
        bkgd.transitionToThree = true;
        yield return new WaitUntil(() => bkgd.levelDist >= 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        level++;
        Debug.Log("Going to level 3!");
        //StartCoroutine(CarryPlayerHP());
        int hPSet = playerHPTrack;
        while(true)
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            else
            {
                player.GetComponent<HPComponent>().currenthealth = hPSet;
                Debug.Log("Health should be set");
                break;
            }
        }
    }

    IEnumerator Level3()
    {
        Instantiate(enemies[0], new Vector3(-7f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-6f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-5f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-4f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-3f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-1f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(0f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(1f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(3f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(4f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(5f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(6f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(7f, 7f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 1f);
        Instantiate(enemies[0], new Vector3(-7f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-6f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-5f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-4f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-3f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-1f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(0f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(1f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(3f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(4f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(5f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(6f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(7f, 7f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 7f);
        Instantiate(enemies[1], new Vector3(-7f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-5f, -8f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-3f, -9f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-1f, -10f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(1f, -11f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(3f, -12f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(5f, -13f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(7f, -14f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 13f);
        Instantiate(enemies[1], new Vector3(-6f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-4f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-2f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(0f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(2f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(4f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(6f, -7f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => gameTime > 16f);
        Instantiate(bosses[2], new Vector3(0f, -10f, 0f), Quaternion.identity);
    }
}
