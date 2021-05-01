using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    #region - Variables -
    //GameObjects
    GameObject player;

    //Control
    bool isDestroying;

    //Frequency
    public float destroyingFrequency;
    #endregion

    #region - Start -
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region - Trigger -
    private void OnTriggerStay(Collider other)
    {
        if (!isDestroying)
        {
            player.GetComponent<Player>().LeftCube(1 , true,true);
            isDestroying = true;
            StartCoroutine(Destroyer());
        }

    }

    #endregion

    #region - Destroy Routine -
    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(destroyingFrequency);
        isDestroying = false;
    }

    #endregion
}
