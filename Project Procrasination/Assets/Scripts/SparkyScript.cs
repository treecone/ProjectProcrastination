using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkyScript : MonoBehaviour {

    public int movement;
    public float timeElapsed;
    public float totalTimeElapsed;


	// Use this for initialization
	void Start () {
        movement = 1;
        totalTimeElapsed = 0;
	}

    // Update is called once per frame
    void Update() {
        switch (movement)
        {
            case 1:
                this.transform.Translate(Vector3.right * Time.deltaTime * 3);
                this.transform.Translate(Vector3.up * Time.deltaTime * Mathf.Sin(totalTimeElapsed * 10));
                break;
            case 2:
                this.transform.Translate(Vector3.left * Time.deltaTime * 3);
                this.transform.Translate(Vector3.up * Time.deltaTime * Mathf.Sin(totalTimeElapsed * 10));
                break;

        }
        timeElapsed += Time.deltaTime;
        totalTimeElapsed += Time.deltaTime;
        if(timeElapsed >= 1)
        {
            if (movement == 1)
                movement++;
            else
                movement = 1;
            timeElapsed = 0;
        }
	}

}
