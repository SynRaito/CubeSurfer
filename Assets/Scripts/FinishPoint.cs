using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    #region - Variables-
    //GameObject
    Player player;
    UIManager uiManager;

    //Control
    bool isCollided;
    #endregion

    #region - Start-
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        uiManager = GameObject.FindGameObjectWithTag("UIHandler").GetComponent<UIManager>();
    }
    #endregion

    #region - Collision -
    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollided)
        {
            isCollided = true;
            player.SetPlayingState(false);
            uiManager.ShowEndScreen();
        }
    }
    #endregion
}