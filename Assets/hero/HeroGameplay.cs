using UnityEngine;
using System.Collections;
using System;

public class HeroGameplay : MonoBehaviour
{
    //Define the "mode" enumeration
    public enum mode { BOOST, SHOOT };

    //Hold the current player mode
    public mode playerMode;

    private Quaternion BoostBaseOrient;
    private Quaternion ShootBaseOrient;




    public bool cursorHide;


    private Vector2 mouse, mouseAbs;
    public float mouseFactor;

    // Use this for initialization
    void Start()
    {
        playerMode = mode.BOOST;
        BoostBaseOrient = new Quaternion();
        ShootBaseOrient = new Quaternion();
        BoostBaseOrient.SetEulerAngles(Mathf.PI / 2, 0, 0);

        //set the cusor parameters
        Cursor.visible = cursorHide;
        Cursor.lockState = CursorLockMode.Locked;

        mouseAbs = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        getUserInputs();
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
        mouseAbs += mouse;
        Debug.Log("mouse : " + mouseAbs);
    }

    /// <summary>
    /// Mouve the hero int the X/Y plane according to the absolute mouse position
    /// </summary>
    private void setPositionFromMouse()
    {
        Vector3 heroPosition = transform.position;
        heroPosition.x = mouseAbs.x * mouseFactor;
        heroPosition.y = mouseAbs.y * mouseFactor;
        transform.position = heroPosition;
    }

    private void SwitchMode()
    {
        if (playerMode == mode.BOOST)
            playerMode = mode.SHOOT;
        else
            playerMode = mode.BOOST;
    }

    private void shoot()
    {
        if (playerMode != mode.SHOOT) return;
    }

    private void setBaseOrientation()
    {
 
        switch (playerMode)
        {
            case mode.BOOST:
                //Debug.Log("HERO BOOST MODE");
                transform.rotation = BoostBaseOrient;
                break;
            case mode.SHOOT:
                //Debug.Log("HERO SHOOT MODE");
                transform.rotation = ShootBaseOrient;
                break;
        }
    }
}
