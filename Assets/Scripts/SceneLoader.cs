using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartScreen()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<Level>().DestroyLevel();
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
       // FindObjectOfType<Level>().destroyLevel();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

