using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockContainer : MonoBehaviour
{
    // Deactivate all other cubes at the same axis to prevent bug
    public void DeactivateAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<CubeController>().isCollided = true;
        }
    }
}
