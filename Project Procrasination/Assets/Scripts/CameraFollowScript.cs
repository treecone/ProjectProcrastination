using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public GameObject target;
    public float speed;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, new Vector3 (target.transform.position.x, target.transform.position.y, -10), speed * Time.deltaTime);
	}
}
