using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBubbleScript : MonoBehaviour {

    public Dictionary<string, string[]> theMessages = new Dictionary<string, string[]>();
    int theCount;
    public string theLastKey; //Set this where its first called

	void Start ()
    {
        theMessages.Add ("BrowseMemes", new string[] {"O man gotta look up some hot memes!", "Ohh Jeez!", "Golly!" });
        theCount = 0;
	}

    public void CallTextMessage()
    {
        if(GameObject.Find ("MessageBubble") != null)
        {
            Destroy(GameObject.Find("MessageBubble"));
        }
        if (theCount < theMessages[theLastKey].Length)
        {
            GameObject bm = Instantiate(Resources.Load("TextBubble"), new Vector2(GameObject.FindWithTag("Player").transform.position.x + 0.5f, GameObject.FindWithTag("Player").transform.position.y + 1.4f), Quaternion.identity, GameObject.FindWithTag("Player").transform) as GameObject;
            bm.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = theMessages[theLastKey][theCount];
        }
        if(theCount == theMessages[theLastKey].Length)
        {
            theCount = 0;
            return;
        }
        theCount += 1;
    }
	
	void Update ()
    {
		
	}
}
