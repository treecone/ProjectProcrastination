using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionNodeScript : MonoBehaviour {

    [SerializeField]
    private int Number;

    public int number
    {
        get
        {
            GameObject.FindWithTag("Player").GetComponent<TextBubbleScript>().theLastKey = gameObject.transform.name;
            GameObject.FindWithTag("Player").GetComponent<TextBubbleScript>().CallTextMessage();

            if(gameObject.name == "Shower")
            {
                GameObject.Find("Justin").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("ShowerInUse");
                StartCoroutine(Camera.main.transform.GetComponent<WinScript>().timerForMethods(3, "showerOut", gameObject));
            }
            return Number;
        }
    }
}
