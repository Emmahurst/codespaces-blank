using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathSystem : MonoBehaviour
{
    public GameObject Death;
    public GameObject DeathScreen;

    void Start()
    {
        Death.SetActive(false);
    }

    




    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            if (Death.activeInHierarchy)
            {
                StartCoroutine("DeathScreenLoader");
                //Cursor.lockState = CursorLockMode.None;

                Restart();
            }
        }
    }

    public IEnumerator DeathScreenLoader()
    {
        yield return new WaitForSeconds(2);
        Death.SetActive(true);
    }

  


}    

