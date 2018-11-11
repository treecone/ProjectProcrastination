using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

    public float time;
    public float percentageOfEssay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += 0.01f;
        GameObject.Find("Canvas").transform.GetChild(0).transform.rotation = Quaternion.Euler (0,0,90-time);
        if(time >= 270)
        {
            //Game over
            Debug.Log(percentageOfEssay);
            SceneManager.LoadScene(1);
        }
	}
}
