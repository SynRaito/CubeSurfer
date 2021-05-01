using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region - Variables -
    //GameObject
    public Player player;

    //UI Object
    public GameObject playUI;
    public GameObject retryUI;
    public GameObject quitUI;
    public GameObject scoreUI;
    public TMP_Text scoreText;

    //Score for end game
    public int score;
    #endregion

    #region - Functions For Buttons -
    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        player.SetPlayingState(true);
        playUI.SetActive(false);
    }

    #endregion

    #region - End Screen -
    public void ShowEndScreen()
    {
        retryUI.SetActive(true);
        quitUI.SetActive(true);
        if (score > 0)
            scoreText.text = "Score: " + score.ToString();
        else
            scoreText.text = "You Lost";
        scoreUI.SetActive(true);
    }
    #endregion
}
