using UnityEngine;
using System.Collections;

public class HeroCameraControll : MonoBehaviour {

    public Camera thirdPersonCamera;

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
        //Stay behind Hero
        float angle = Mathf.LerpAngle(thirdPersonCamera.transform.eulerAngles.y, transform.eulerAngles.y, Time.deltaTime * dampingOrient);
        Quaternion rotation = Quaternion.Euler(0, angle, 0);

        Vector3 currentPosition = thirdPersonCamera.transform.position;
        Vector3 directPosition = transform.position - (rotation * cameraOffset);



        thirdPersonCamera.transform.position = Vector3.Lerp(currentPosition, directPosition, Time.deltaTime * dampingPos);
        //Look at the hero
        thirdPersonCamera.transform.LookAt(transform);
	}

    
}
