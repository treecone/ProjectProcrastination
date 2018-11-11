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
            return Number;
        }
    }
}
