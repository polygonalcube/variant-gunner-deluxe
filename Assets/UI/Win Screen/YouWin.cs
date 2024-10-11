using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class YouWin : MonoBehaviour
{
    public TextMeshProUGUI score;

    void Start()
    {
        score.text = "FINAL SCORE: " + GameManager.gm.score.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
