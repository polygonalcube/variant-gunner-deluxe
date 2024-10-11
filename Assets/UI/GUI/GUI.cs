using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GUI : MonoBehaviour
{
    public TextMeshProUGUI hp;
    public TextMeshProUGUI score;
    public Image gameOver;

    void Start()
    {
        gameOver.gameObject.SetActive(false);
    }
   
    void Update()
    {
        hp.text = GameManager.gm.playerHPTrack.ToString();
        score.text = GameManager.gm.score.ToString();
        if (GameManager.gm.playerHPTrack <= 0)
        {
            gameOver.gameObject.SetActive(true);
        }
        else
        {
            gameOver.gameObject.SetActive(false);
        }
    }
}
