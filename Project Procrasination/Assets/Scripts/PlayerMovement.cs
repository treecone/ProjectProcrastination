using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private bool restrictLeft, restrictRight, restrictTop, restrictBottom;
    public float sizeOfRay = .1f;


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
        CheckCollisionsWithWall();
        Movement();
    }
    /// <summary>
    /// moves player up, down, left, right, according to what key(s) are pressed
    /// </summary>
    void Movement()
    {
        MoveIfKeyPressed(KeyCode.W, Vector3.up, restrictTop);
        MoveIfKeyPressed(KeyCode.S, Vector3.down, restrictBottom);
        MoveIfKeyPressed(KeyCode.A, Vector3.left, restrictLeft);
        MoveIfKeyPressed(KeyCode.D, Vector3.right, restrictRight);
    }
    void MoveIfKeyPressed(KeyCode k, Vector3 v, bool restriction)
    {
        if(Input.GetKey(k) == true && !restriction)
        {
            this.transform.Translate(v * Time.deltaTime * 2);
        }
    }

    void CheckCollisionsWithWall()
    {
        if (CollidesWithObject(this.transform.Find("Top").gameObject, Vector2.up, sizeOfRay))
        {
            restrictTop = true;
        }
        else
        {
            restrictTop = false;
        }

        if (CollidesWithObject(this.transform.Find("Bottom").gameObject, Vector2.down, sizeOfRay))
        {
            restrictBottom = true;
        }
        else
        {
            restrictBottom = false;
        }

        if (CollidesWithObject(this.transform.Find("Left").gameObject, Vector2.left, sizeOfRay))
        {
            restrictLeft = true;
        }
        else
        {
            restrictLeft = false;
        }

        if (CollidesWithObject(this.transform.Find("Right").gameObject, Vector2.right, sizeOfRay))
        {
            restrictRight = true;
        }
        else
        {
            restrictRight = false;
        }
    }
    
    bool CollidesWithObject(GameObject g, Vector2 direction, float length)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, length);
        Debug.DrawRay(g.transform.position, direction * length);
        return hit.collider != null;
    }
   
}
