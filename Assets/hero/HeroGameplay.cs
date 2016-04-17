using UnityEngine;
using System.Collections;
using System;

public class HeroGameplay : MonoBehaviour
{
    public Rigidbody Bullet;
    public bool resetPitch;
    public bool debugGUI;
    public bool GO;
    public float speed;
    public float bulletSpeed;

    ///Define the "mode" enumeration
    public enum mode { BOOST, SHOOT };

    //Hold the current player mode
    public mode playerMode;
    public float eulerY;
    public float eulerP;

    public Vector3 BulletSpawnOffset;

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
    private float lastShootTime;
    public float shootRate;

    Animator anim;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

       // playerMode = mode.BOOST;
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
        if(playerMode == mode.SHOOT)
            applyHoverOscilations();
        else
            goFowrard();
    }

    private void goFowrard()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    private void applyHoverOscilations()
    {
        transform.position += new Vector3(0, 0.0125f* Mathf.Cos(3*Time.realtimeSinceStartup), 0);
    }

    /// <summary>
    /// Do the basic input procesing on keyboard and mouse
    /// </summary>
    private void getUserInputs()
    {
        //If player press space, change the mode 
        if (Input.GetKeyDown("space") || Input.GetKeyUp("space")) SwitchMode();

        //Get the last mouse movement and update mouse absolute position from it from it
        mouse.x = Input.GetAxis("Mouse X");
        mouse.y = Input.GetAxis("Mouse Y");
        
        //Debug.Log("mouse : " + mouseAbs);
        rotateYaw(Input.GetAxis("Horizontal"));
        rotatePitch(Input.GetAxis("Vertical"));

        if (Input.GetMouseButton(0)) shoot();

    }

    /// <summary>
    /// Mouve the hero int the X/Y plane according to the absolute mouse position
    /// </summary>
    private void setPositionFromMouse()
    {

        if (playerMode == mode.SHOOT)
        {
            rotateYaw(mouse.x);
            rotatePitch(mouse.y);
        }
        else
        {
            transform.Translate(new Vector3(mouse.x * mouseFactor, mouse.y * mouseFactor, 0));
        }

    }

    /// <summary>
    /// Switch the current hero mode. Should play an animation. No idea how to do that, tho... :D
    /// </summary>
    private void SwitchMode()
    {

        if (playerMode == mode.BOOST)
        {
            playerMode = mode.SHOOT;
            anim.SetBool("doTheShift", true);
            anim.SetBool("doTheDeshift", false);
        }
        else
        {
            playerMode = mode.BOOST;
            anim.SetBool("doTheShift", false);
            anim.SetBool("doTheDeshift", true);
        }
        if (resetPitch)
            resetEulerPitch();
    }

    private void resetEulerPitch()
    {
        eulerP = 0;
    }

    /// <summary>
    /// If in shoot mode, shoot.
    /// </summary>
    private void shoot()
    {
        if (playerMode != mode.SHOOT) return;
        if (Time.realtimeSinceStartup - lastShootTime >= shootRate)
        {
            BulletSpawnOffset.x = -BulletSpawnOffset.x;
            Debug.Log("Shoot !!");
            lastShootTime = Time.realtimeSinceStartup;
            Rigidbody bulletInstance =  Instantiate(Bullet, transform.position + transform.rotation * BulletSpawnOffset, new Quaternion()) as Rigidbody;
            bulletInstance.velocity = (transform.rotation * new Vector3(0, 0, 1) * bulletSpeed);
     
        }
    }

    /// <summary>
    /// Switch the orientation of the player between the two modes. 
    /// </summary>
    private void setBaseOrientation()
    {
        Quaternion gameplayOrient = Quaternion.Euler(0, eulerY, 0);

        switch (playerMode)
        {
            case mode.BOOST:
                //Debug.Log("HERO BOOST MODE");
                transform.rotation = BoostBaseOrient * gameplayOrient;
                transform.Rotate(eulerP, 0, 0);
                break;
            case mode.SHOOT:
                //Debug.Log("HERO SHOOT MODE");

                transform.rotation = BoostBaseOrient * gameplayOrient;
                transform.Rotate(eulerP, 0, 0);
                break;
        }
    }

    private void rotateYaw(float axis)
    {
        eulerY += rotateSpeed * Time.deltaTime * axis;

    }
    private void rotatePitch(float axis)
    {
        eulerP += rotateSpeed * Time.deltaTime * -axis;

        //Clamp value to [-90:90]
        if (eulerP <= -90) eulerP = -90;
        if (eulerP >= 90) eulerP = 90;
    }


    void OnGUI()
    {
        string debugInfo = "EulerY : " + eulerY + "\n";
        debugInfo += "EulerP : " + eulerP + "\n";
        debugInfo += "Mode : " + (int) playerMode;
        if (!debugGUI) return;
        GUI.Box(new Rect(0, 0, 100, 100), "Debug info");
        GUI.TextArea(new Rect(10, 10, 90, 90), debugInfo);
    }
}
