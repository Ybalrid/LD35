using UnityEngine;
using System.Collections;

public class HeroCameraControll : MonoBehaviour {

    public Camera thirdPersonCamera;
    public bool doNothing;
    public float dampingPos;
    public float dampingOrient;

    /// <summary>
    /// Offset when stating the game
    /// </summary>
    private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
        if (thirdPersonCamera)
            Debug.Log("Camera is set");
        else
            return;

        cameraOffset = transform.position - thirdPersonCamera.transform.position;
        Debug.Log("Set camera offset : " + cameraOffset);
	
	}

    void LateUpdate () {
        if (doNothing) return;
        //Stay behind Hero
        float y = transform.eulerAngles.y;
     //   Debug.Log("Y : " + y);
        float angle = Mathf.LerpAngle(thirdPersonCamera.transform.eulerAngles.y, y, Time.deltaTime * dampingOrient);
    //    Debug.Log("Angle : " + angle);

        //if (transform.rotation.eulerAngles.z == 180) angle += 180;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
     //   Debug.Log("Rotation (euler) : " + rotation.eulerAngles);

   

        Vector3 currentPosition = thirdPersonCamera.transform.position;
        Vector3 directPosition = transform.position - (rotation * cameraOffset);

     //   Debug.Log(directPosition.z);

        thirdPersonCamera.transform.position = Vector3.Lerp(currentPosition, directPosition, Time.deltaTime * dampingPos);
        //Look at the hero
        thirdPersonCamera.transform.LookAt(transform);
	}

    
}
