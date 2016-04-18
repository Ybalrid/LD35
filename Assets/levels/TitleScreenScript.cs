using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

    public string levelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("return"))
        {
            //Load Level here
            Debug.Log("hello world");
            Application.LoadLevel(levelName);
         }
    }
	
	
}
