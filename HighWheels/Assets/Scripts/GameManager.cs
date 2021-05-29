using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] TextMeshProUGUI diamondCount;
    [SerializeField] private TotalDiamonds totalDiamondsData;

    #region Subscribe and unsubscribe to events

    private void OnEnable()
    {
        EventBroker.OnPickUpDiamond += TakeDiamond;
        //EventBroker.OnGameOver += GameOverActions;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpDiamond -= TakeDiamond;
        //EventBroker.OnGameOver -= GameOverActions;
    }

    #endregion

    private void TakeDiamond()
    {
        totalDiamondsData.diamondCount++;
        diamondCount.text = totalDiamondsData.diamondCount.ToString();
    }

    private void IncreaseDiamond(int increaseAmount)
    {
        totalDiamondsData.diamondCount += increaseAmount;
        diamondCount.text = totalDiamondsData.diamondCount.ToString();
    }

    private void DecreaseDiamond(int decreaseAmount)
    {
        totalDiamondsData.diamondCount -= decreaseAmount;
        diamondCount.text = totalDiamondsData.diamondCount.ToString();
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
