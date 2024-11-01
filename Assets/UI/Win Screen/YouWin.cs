using TMPro;
using UnityEngine;

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
