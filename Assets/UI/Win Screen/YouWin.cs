using TMPro;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    /// <summary>
    /// Displays the player's final score after the game is beaten. Allows the player to quit the game.
    /// </summary>
    
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
