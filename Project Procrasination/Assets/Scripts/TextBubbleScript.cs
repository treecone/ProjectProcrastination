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
        theMessages.Add("Toliet", new string[] {"I knew I shouldn't have gone to taco bell last night!"});
        theMessages.Add("Bed", new string[] {"Oh no I lost my my will to live!", "I know it's here somewhere!"});
        theMessages.Add("Pet", new string[] { "Who's a good boy?!!?" });
        theMessages.Add("Couch", new string[] {"Maybe I'll close my eyes for a few minutes"});
        theMessages.Add("TV", new string[] {"I got to stop watching this garbage"});
        theMessages.Add("KitchenSink", new string[] {"Who left their dishes in my sink?", "Oh yeah, that was me..."});
        theMessages.Add("Stove", new string[] {"Ahh, shoot", "I burned my eggs again"});
        theMessages.Add("Dishwasher", new string[] {"Better put away the dishes..."});
        theMessages.Add("Fridge", new string[] {"Imhm...", "Just one more..."});
        theMessages.Add("Bike", new string[] { "Oh man I forgot to pump up my tire!","Almost there!", "There we go."});
        theMessages.Add("Shower", new string[] {"Squeaky Clean!"});
        theMessages.Add("SportToys", new string[] {"My mitt's got to be in there somewhere!"});
        theMessages.Add("Dogfood", new string[] { "Sit!", "Good boy!", "Now eat up!" });
        theMessages.Add("Shoes", new string[] {"Went hiking yesterday.", "Better clean up this mud!"});
        theMessages.Add("Large Table", new string[] { "Oh man", " These are barely edible"});
        theMessages.Add("Car (1)", new string[] { "Where did my laptop go?!", "Ah! There it is!"});


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
