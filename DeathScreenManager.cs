using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GameObject _deathScreen;
    [SerializeField] GameObject _firstSelect;


    private void OnEnable()
    {
        PlayerController.playerDies += OnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerController.playerDies -= OnPlayerDeath;
    }

    public void OnPlayerDeath()
    {
        _deathScreen.SetActive(true);
        _eventSystem.SetSelectedGameObject(_firstSelect);
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
