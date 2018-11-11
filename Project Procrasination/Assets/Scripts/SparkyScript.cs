using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkyScript : MonoBehaviour {

    float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += 0.1f;
        gameObject.transform.GetChild(0).position = new Vector2(gameObject.transform.position.x + Mathf.Sin(time * -0.33f + 152), gameObject.transform.position.y + Mathf.Sin(time * 0.2f));
	}
}
