using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMessage : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(Timer(2));
    }

    IEnumerator Timer (float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.FindWithTag("Player").GetComponent<TextBubbleScript>().CallTextMessage();
        Destroy(gameObject);
    }

}
