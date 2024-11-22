using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    /// <summary>
    /// The logic for the title screen.
    ///
    /// Allows the player to progress to the game if a UI button or key is pressed.
    /// </summary>
    
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void StartGame()
    {
        LoadNextScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
