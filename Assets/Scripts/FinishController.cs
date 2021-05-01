using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    #region - Variables -
    //GameObject
    Player player;
    UIManager uiManager;

    //Control
    bool isCollided;
    #endregion

    #region - Start -
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
            player.LeftCube(1, false, false);
            isCollided = true;
            uiManager.score += 1;
        }
    }
    #endregion
}
