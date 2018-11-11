using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBubbleScript : MonoBehaviour {

    public Dictionary<string, string[]> theMessages = new Dictionary<string, string[]>();
    int theCount;

	void Start ()
    {
        theCount = 0;
	}

    void CallTextMessage(string key)
    {
        if(GameObject.Find ("MessageBubble") != null)
        {
            Destroy(GameObject.Find("MessageBubble"));
        }
        if (theCount <= theMessages[key].Length)
        {
            GameObject bm = Instantiate(Resources.Load("MessageBubble"), new Vector2(GameObject.FindWithTag("Player").transform.position.x + 0.06f, GameObject.FindWithTag("Player").transform.position.y + 0.66f), Quaternion.identity, GameObject.FindWithTag("Player").transform) as GameObject;
            bm.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = theMessages[key][theCount];
        }
        theCount += 1;
    }
	
	void Update ()
    {
		
	}
}
