using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    /// <summary>
    /// The logic for the UI that displays during gameplay.
    ///
    /// Displays health and score, and shows game over text on death.
    /// </summary>
    
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI scoreDisplay;
    public Image gameOverImage;

    void Start()
    {
        gameOverImage.gameObject.SetActive(false);
    }
   
    void Update()
    {
        healthDisplay.text = GameManager.gm.playerHPTrack.ToString();
        scoreDisplay.text = GameManager.gm.score.ToString();
        gameOverImage.gameObject.SetActive(GameManager.gm.playerHPTrack <= 0);
    }
}
