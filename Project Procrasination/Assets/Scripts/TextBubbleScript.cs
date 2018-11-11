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
        theMessages.Add("MedicineCabinet", new string[] {"I should keep myself clean.", "Its flu season."});
        theMessages.Add("Toliet", new string[] {"I forgot to clean this yesterday!"});
        theMessages.Add("Bed", new string[] {"Oh no I lost my phone!", "I know it's here somewhere!"});
        theMessages.Add("Pet", new string[] { "Who's a good boy?!!?" });
        theMessages.Add("Couch", new string[] {"Maybe just a second..."});
        theMessages.Add("TV", new string[] {"The last episode left on a cliffhanger!"});
        theMessages.Add("KitchenSink", new string[] {"Better do the dishes!", "I'm feeling productive now!"});
        theMessages.Add("Stove", new string[] {"I got to cook this food before it goes bad!"});
        theMessages.Add("Dishwasher", new string[] {"Better but away the dishes..."});
        theMessages.Add("Fridge", new string[] {"Imhm...", "Just one more..."});
        theMessages.Add("Bike", new string[] { "Oh man I forgot to pump up my tire!","Almost there!", "There we go."});
        theMessages.Add("Shower", new string[] {"Squeaky Clean!"});
        theMessages.Add("SportToys", new string[] {"My mitt's got to be in there somewhere!"});
        theMessages.Add("Dogfood", new string[] { "Sit!", "Good boy!", "Now eat up!" });


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
