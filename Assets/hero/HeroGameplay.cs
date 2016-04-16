using UnityEngine;
using System.Collections;
using System;

public class HeroGameplay : MonoBehaviour
{
    public bool debugGUI;
    public bool GO;

    ///Define the "mode" enumeration
    public enum mode { BOOST, SHOOT };

    //Hold the current player mode
    public mode playerMode;
    public float eulerY;


    //Ratio between Mouse mouvement and Hero mouvement
    public float mouseFactor;

    //Rotation speed 
    public float rotateSpeed;

    /// <summary>
    /// The orientation in BOOST mode
    /// </summary>
    private Quaternion BoostBaseOrient;

    /// <summary>
    /// The orientation in Shoot mode
    /// </summary>
    private Quaternion ShootBaseOrient;


    /// <summary>
    /// Holds mouse movement data  
    /// </summary>
    private Vector2 mouse;

    // Use this for initialization
    void Start()
    {
        playerMode = mode.BOOST;
        BoostBaseOrient = transform.rotation;
        ShootBaseOrient = Quaternion.Euler(-90, 0, 0) * BoostBaseOrient;

        //set the cusor parameters
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        eulerY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        getUserInputs();
        setBaseOrientation();
        setPositionFromMouse();
    }

    /// <summary>
    /// Do the basic input procesing on keyboard and mouse
    /// </summary>
    private void getUserInputs()
    {
        //If player press space, change the mode 
        if (Input.GetKeyDown("space")) SwitchMode();

        //Get the last mouse movement and update mouse absolute position from it from it
        mouse.x = Input.GetAxis("Mouse X");
        mouse.y = Input.GetAxis("Mouse Y");
        
        //Debug.Log("mouse : " + mouseAbs);
        rotateYaw(Input.GetAxis("Horizontal"));

    }

    /// <summary>
    /// Mouve the hero int the X/Y plane according to the absolute mouse position
    /// </summary>
    private void setPositionFromMouse()
    {
        
        if (playerMode == mode.SHOOT) mouse.y = 0;
        transform.Translate( new Vector3(mouse.x * mouseFactor, mouse.y * mouseFactor, 0));
    }

    /// <summary>
    /// Switch the current hero mode. Should play an animation. No idea how to do that, tho... :D
    /// </summary>
    private void SwitchMode()
    {
        if (playerMode == mode.BOOST)
            playerMode = mode.SHOOT;
        else
            playerMode = mode.BOOST;
    }

    /// <summary>
    /// If in shoot mode, shoot.
    /// </summary>
    private void shoot()
    {
        if (playerMode != mode.SHOOT) return;
    }

    /// <summary>
    /// Switch the orientation of the player between the two modes. 
    /// </summary>
    private void setBaseOrientation()
    {
        Quaternion gameplayOrient = Quaternion.Euler(0, eulerY, 0);
        Quaternion gameplayShootOrient = Quaternion.Euler(0, 0, eulerY);

        switch (playerMode)
        {
            case mode.BOOST:
                //Debug.Log("HERO BOOST MODE");
                transform.rotation = BoostBaseOrient * gameplayOrient;
                break;
            case mode.SHOOT:
                //Debug.Log("HERO SHOOT MODE");
                transform.rotation = BoostBaseOrient * gameplayOrient;
                transform.Rotate(-90, 0, 0);
                break;
        }
    }

    private void rotateYaw(float axis)
    {
        eulerY += rotateSpeed * Time.deltaTime * axis;
    }


    void OnGUI()
    {
        string debugInfo = "EulerY : " + eulerY + "\n";
        debugInfo += "Mode : " + (int) playerMode;
        if (!debugGUI) return;
        GUI.Box(new Rect(0, 0, 100, 100), "Debug info");
        GUI.TextArea(new Rect(10, 10, 90, 90), debugInfo);
    }
}
