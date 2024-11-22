using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Stores variables and handles actions that need to be preserved between scenes.
    /// </summary>
    
    public static GameManager gm;

    public int score;

    public int level = 1;
    int prevLevel = 1;
    //public bool destroyAllBullets;

    public LevelBackground levelBackground;

    public GameObject[] enemies;
    public GameObject[] bosses;

    private float timeElapsed;

    public GameObject player;
    public int playerHPTrack;

    void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        player = GameObject.Find("Player");
        playerHPTrack = 4;

        prevLevel = level;
        if (level == 1 || level == 2)
        {
            levelBackground = GameObject.Find("Background").GetComponent<LevelBackground>();
        }

        SpawnEntities();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            playerHPTrack = player.GetComponent<HPComponent>().currentHealth;
            if (player.GetComponent<HPComponent>().currentHealth <= 0)
            {
                StartCoroutine(Reload());
            }
        }

        if (level != prevLevel)
        {
            prevLevel = level;
            timeElapsed = 0f;
            SpawnEntities();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void SpawnEntities()
    {
        timeElapsed = 0f;

        if (level == 1)
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
        {
            levelBackground = GameObject.Find("Background").GetComponent<LevelBackground>();
        }

        SpawnEntities();
    }

    private void SpawnThreeBeelinersAtLeft()
    {
        Instantiate(enemies[0], new Vector3(-6f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-4f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-2f, 10.5f, 0f), Quaternion.identity);
    }
    
    private void SpawnThreeBeelinersAtRight()
    {
        Instantiate(enemies[0], new Vector3(6f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(4f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(2f, 10.5f, 0f), Quaternion.identity);
    }

    IEnumerator Level1()
    {
        yield return new WaitUntil(() => timeElapsed > 0.04f);
        levelBackground = GameObject.Find("Background").GetComponent<LevelBackground>();

        SpawnThreeBeelinersAtLeft();
        yield return new WaitUntil(() => timeElapsed > 2f);
        SpawnThreeBeelinersAtRight();
        yield return new WaitUntil(() => timeElapsed > 6f);
        Instantiate(bosses[0], new Vector3(-200f, 0f, 500f), Quaternion.identity);
    }
    
    private void SpawnSevenBeelinersAtLeft()
    {
        Instantiate(enemies[0], new Vector3(-1f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-3f, 7.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-4f, 8f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-5f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-6f, 9f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(-7f, 9.5f, 0f), Quaternion.identity);
    }
    
    private void SpawnSevenBeelinersAtRight()
    {
        Instantiate(enemies[0], new Vector3(1f, 6.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(2f, 7f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(3f, 7.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(4f, 8f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(5f, 8.5f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(6f, 9f, 0f), Quaternion.identity);
        Instantiate(enemies[0], new Vector3(7f, 9.5f, 0f), Quaternion.identity);
    }

    IEnumerator Level2()
    {
        yield return new WaitUntil(() => timeElapsed > 0.04f);
        levelBackground = GameObject.Find("Background").GetComponent<LevelBackground>();

        SpawnSevenBeelinersAtLeft();
        yield return new WaitUntil(() => timeElapsed > 3f);
        SpawnSevenBeelinersAtRight();
        yield return new WaitUntil(() => timeElapsed > 6f);
        SpawnSevenBeelinersAtLeft();
        yield return new WaitUntil(() => timeElapsed > 9f);
        SpawnSevenBeelinersAtRight();
        yield return new WaitUntil(() => timeElapsed > 15f);
        Instantiate(enemies[1], new Vector3(7f, -7f, 0f), Quaternion.identity);
        yield return new WaitUntil(() => levelBackground.levelDistance >= 1f);
        Instantiate(bosses[1], new Vector3(0f, 0f, -11f), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        
        yield return new WaitUntil(() =>
            GameObject.Find("Boss 2(Clone)").GetComponent<HPComponent>().currentHealth <= 0f);
        
        yield return new WaitForSeconds(3);
        levelBackground.levelDistance = 0f;
        levelBackground.transitionSpeed *= 3f;
        levelBackground.transitioningToLevelThree = true;
        
        yield return new WaitUntil(() => levelBackground.levelDistance >= 1f);
        
        int hPSet = playerHPTrack;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        level = 3;
        Debug.Log("Going to level 3!");

        while (true)
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            else
            {
                player.GetComponent<HPComponent>().currentHealth = hPSet;
                Debug.Log("Health should be set");
                break;
            }
        }
    }

    private void SpawnRowOfFifteenBeeliners()
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
    }

    private void SpawnEightBarrellersStaggered()
    {
        Instantiate(enemies[1], new Vector3(-7f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-5f, -8f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-3f, -9f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-1f, -10f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(1f, -11f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(3f, -12f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(5f, -13f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(7f, -14f, 0f), Quaternion.identity);
    }

    private void SpawnSevenBarrellersAligned()
    {
        Instantiate(enemies[1], new Vector3(-6f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-4f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(-2f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(0f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(2f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(4f, -7f, 0f), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(6f, -7f, 0f), Quaternion.identity);
    }

    IEnumerator Level3()
    {
        yield return new WaitUntil(() => timeElapsed > 0.04f);
        SpawnRowOfFifteenBeeliners();
        yield return new WaitUntil(() => timeElapsed > 1f);
        SpawnRowOfFifteenBeeliners();
        yield return new WaitUntil(() => timeElapsed > 7f);
        SpawnEightBarrellersStaggered();
        yield return new WaitUntil(() => timeElapsed > 13f);
        SpawnSevenBarrellersAligned();
        yield return new WaitUntil(() => timeElapsed > 16f);
        Instantiate(bosses[2], new Vector3(0f, -10f, 0f), Quaternion.identity);
    }
}
