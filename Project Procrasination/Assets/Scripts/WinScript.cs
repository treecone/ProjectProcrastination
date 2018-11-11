using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScript : MonoBehaviour {

    public float time;
    public int percentageOfEssay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += 0.01f;
        GameObject.Find("Canvas").transform.GetChild(0).transform.rotation = Quaternion.Euler (0,0,90-time);
        GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Essay: " + percentageOfEssay + "%";
        if (time >= 270 || percentageOfEssay > 100)
        {
            //Game over
            Debug.Log(percentageOfEssay);
            SceneManager.LoadScene(1);
        }
    }

    public IEnumerator timerForMethods (float timer, string method, GameObject oriObject)
    {
        yield return new WaitForSeconds(timer);
        StartCoroutine("method", oriObject);

    }

    public IEnumerator showerOut (GameObject oriObject)
    {
        oriObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ShowerEmpty");
        GameObject.Find("Justin").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        return null;

    }
}
