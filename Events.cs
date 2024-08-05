using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
       SceneManager.LoadScene("EndlessRunnerGame");
    }
    
    public void QuitGame() 
    {
        Application.Quit();
    }

}
