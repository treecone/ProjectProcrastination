using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    //Simple method to start the game!
    public void StartGame ()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene(1);
    }
}
