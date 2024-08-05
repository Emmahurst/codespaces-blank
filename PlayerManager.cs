using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static int numberOfCoins;
  
    void Start()
    {
        gameOver = false;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
         if (gameOver)
         {

           Time.timeScale = 0;
            gameOverPanel.SetActive(true);
              ShowGameOverPanel();
         }

        

    }



         private void ShowGameOverPanel()
         {
            gameOverPanel.SetActive(true);
         }


         
          public void RestartGame()
         {
            gameOver = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         }
    


}

// Error messages for line 31 and 38 the modifier private & public cannot be used 





