using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] TextMeshProUGUI diamondCount;

    private int totalDiamonds;

    #region Subscribe and unsubscribe to events

    private void OnEnable()
    {
        EventBroker.OnPickUpDiamond += TakeDiamond;
        EventBroker.OnGameOver += GameOverActions;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpDiamond -= TakeDiamond;
        EventBroker.OnGameOver -= GameOverActions;
    }

    #endregion

    private void TakeDiamond()
    {
        totalDiamonds++;
        diamondCount.text = totalDiamonds.ToString();
    }

    private void IncreaseDiamond(int increaseAmount)
    {
        totalDiamonds += increaseAmount;
        diamondCount.text = totalDiamonds.ToString();
    }

    private void DecreaseDiamond(int decreaseAmount)
    {
        totalDiamonds -= decreaseAmount;
        diamondCount.text = totalDiamonds.ToString();
    }

    private void GameOverActions()
    {
        StartCoroutine(PauseGame());
    }

    private IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
