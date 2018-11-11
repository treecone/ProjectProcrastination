using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMessage : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(Timer(3));
    }

    IEnumerator Timer (float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
