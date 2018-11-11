using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScript : MonoBehaviour {

    public float time;
    public float percentageOfEssay;

	// Update is called once per frame
	void Update ()
    {
        if (time >= 270 || percentageOfEssay > 100)
        {
            //Game over
            StartCoroutine(EndGame());
        }
        else
        {
            GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Essay: " + (int)percentageOfEssay + "%";
            time += 0.008f;
            GameObject.Find("Canvas").transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 90 - time);
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

    public IEnumerator EndGame()
    {
        if (percentageOfEssay < 100)
        {
            GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "YOU WIN!";
        }
        else
        {
            GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "YOU LOSE!";
        }
        yield return new WaitForSeconds(8);
        Debug.Log(percentageOfEssay);
        SceneManager.LoadScene(0);
    }
}
