using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{
    #region - Variables-
    //Various GameObject Got to Reach
    public UIManager ui;
    public Rigidbody rig;
    public GameObject cubePrefab;
    GameObject cubeParent;

    //Speed Variables
    public float forwardSpeed;
    public float horizontalTouchSpeed;
    public float horizontalKeyboardSpeed;

    //Controls
    bool isPlaying;

    //Lists
    List<GameObject> cubes = new List<GameObject>();
    List<int> collidedBlocks = new List<int>();

    #endregion

    #region - Start -
    void Start()
    {
        cubeParent = GameObject.FindGameObjectWithTag("CubeParent");
        GetCube(1);
    }
    #endregion

    #region - Update -
    void Update()
    {
        if (isPlaying)
        {
            ForwardMovement();

            HorizontalMovementStabilizer();

            KeyboardControl();

            TouchControl();
        }
    }
    #endregion

    #region - Multiple Colliding Issue Handler -
    public void HandleCollidedBlocks(int count, GameObject container)
    {
        collidedBlocks.Add(count);

        if (collidedBlocks.Count > 1)
        {
            int temp = 0;
            for (int i = 0; i < collidedBlocks.Count; i++)
            {
                if (collidedBlocks[i] > temp)
                    temp = collidedBlocks[i];
            }
            LeftCube(temp, false, false);
            container.GetComponent<BlockContainer>().DeactivateAll();
            collidedBlocks.Clear();
        }
        
    }
    #endregion

    #region - Movement Limiter -
    void HorizontalMovementStabilizer()
    {
        if (transform.position.x > 4f)
        {
            transform.position = new Vector3(4f, transform.position.y, transform.position.z);
            cubeParent.transform.position = new Vector3(4f, cubeParent.transform.position.y, cubeParent.transform.position.z);
        }
        if (transform.position.x < -4f)
        {
            transform.position = new Vector3(-4f, transform.position.y, transform.position.z);
            cubeParent.transform.position = new Vector3(-4f, cubeParent.transform.position.y, cubeParent.transform.position.z);
        }
    }

    #endregion

    #region - Keyboard Controls -
    void KeyboardControl()
    {
        if (transform.position.x >= -4f && transform.position.x <= 4f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(horizontalKeyboardSpeed * Time.deltaTime, 0, 0);
                cubeParent.transform.position += new Vector3(horizontalKeyboardSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(-horizontalKeyboardSpeed * Time.deltaTime, 0, 0);
                cubeParent.transform.position += new Vector3(-horizontalKeyboardSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    #endregion

    #region - Touch Controls -

    void TouchControl()
    {
        

        if (Input.touchCount > 0)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (transform.position.x >= -4f && transform.position.x <= 4f)
            {
                transform.position += new Vector3(-touchDeltaPosition.x * horizontalTouchSpeed * Time.deltaTime, 0, 0);
                cubeParent.transform.position += new Vector3(-touchDeltaPosition.x * horizontalTouchSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    #endregion

    #region - Mover to Forward -
    void ForwardMovement()
    {
        transform.position += new Vector3(0, 0, -forwardSpeed * Time.deltaTime);
        cubeParent.transform.position += new Vector3(0, 0, -forwardSpeed * Time.deltaTime);
    }
    #endregion

    #region - Getting and Lefting Cubes -
    public void GetCube(int count)
    {
        for (int i = 0; i < count; i++)
        {
            transform.position += new Vector3(0, cubePrefab.GetComponent<Renderer>().bounds.size.y, 0);
            GameObject go = Instantiate(cubePrefab, new Vector3(cubeParent.transform.position.x, cubeParent.transform.position.y + cubePrefab.GetComponent<Renderer>().bounds.size.y * (cubes.Count + 1), cubeParent.transform.position.z), Quaternion.identity);
            cubes.Add(go);
            go.transform.parent = cubeParent.transform;
        }
        Debug.Log(cubes.Count);
    }

    public void LeftCube(int count, bool isGonnaDestroy, bool instantDrop)
    {
        if (cubes.Count <= count)
            LoseIt();
        for (int i = 0; i < count; i++)
        {
            cubes[0].transform.parent = null;
            cubes[0].GetComponent<Collider>().enabled = false;
            cubes[0].GetComponent<Rigidbody>().isKinematic = true;
            if (instantDrop)
            {
                for (int k = 1; k < cubeParent.transform.childCount; k++)
                {
                    cubeParent.transform.GetChild(k).transform.position -= new Vector3(0, cubePrefab.GetComponent<Renderer>().bounds.size.y, 0);
                }
            }
            if (isGonnaDestroy)
                Destroy(cubes[0]);
            cubes.RemoveAt(0);
        }
    }

    #endregion

    #region - Losing -
    void LoseIt()
    {
        ui.ShowEndScreen();
        isPlaying = false;
    }
    #endregion

    #region - Play Status -

    public void SetPlayingState(bool state)
    {
        isPlaying = state;
    }

    #endregion
}