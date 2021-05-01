using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    #region - Variables -
    //Components
    GameObject player;
    Collider col;

    //Controllers
    public bool isDestroyer;
    public int cubeCount;
    public bool isCollided;
    #endregion

    #region - Start -
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<Collider>();
    }
    #endregion

    #region - Various Interactions With Collider -

    private void OnTriggerEnter(Collider other)
    {
        if (!isDestroyer)
        {
            col.enabled = false;
            player.GetComponent<Player>().GetCube(cubeCount);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (isDestroyer && !isCollided)
        {
            isCollided = true;
            player.GetComponent<Player>().HandleCollidedBlocks(cubeCount,transform.parent.gameObject);
            
        }
    }

    #endregion
}
