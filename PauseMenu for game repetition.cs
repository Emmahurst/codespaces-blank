using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool gameisPaused = false;
    public GameObject pauseMenuUI;



    // Start is called before the first frame update
    public void Start()

    {

    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (gameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameisPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameisPaused = true;
    }

}


