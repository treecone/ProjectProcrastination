using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {


    enum Rooms
    {
        BedRoom,
        BathRoom,
        FrontDoor,
        DiningRoom,
        Kitchen,
        Garage
    }

    [SerializeField]
	private List<GameObject> mainOrder = new List<GameObject>();
    GameObject distraction;
    public float speed;
	

    // Update is called once per frame
    void Update() {
        CheckIfDistracted();
        //move Justin when he is not distracted and he is inbetween 2 nodes.
        if (distraction == null && (!PassedNode(mainOrder[1] , GetDirectionBetweenNodes(mainOrder[0], mainOrder[1]))))
        {
            this.transform.Translate(GetDirectionBetweenNodes(mainOrder[0], mainOrder[1]) * Time.deltaTime * speed);
        }
        //once Justin arrives at the node, delete it from mainOrder
        else if(PassedNode(mainOrder[1], GetDirectionBetweenNodes(mainOrder[0], mainOrder[1])))
        {
            mainOrder.RemoveAt(0);
        }
       
    }

    /// <summary>
    /// gets the direction the character needs to move between two nodes
    /// </summary>
    /// <param name="first"> the first node</param>
    /// <param name="second"> the second node</param>
    /// <returns> the Vector2 direction</returns>
    Vector2 GetDirectionBetweenNodes(GameObject firstNode, GameObject secondNode)
    {
        if(firstNode.transform.position.x < secondNode.transform.position.x)
        {
            return Vector2.right;
        }
        if(firstNode.transform.position.x > secondNode.transform.position.x)
        {
            return Vector2.left;
        }
        if(firstNode.transform.position.y < secondNode.transform.position.y)
        {
            return Vector2.up;
        }
        return Vector2.down;
    }
    /// <summary>
    /// checks to see if Justin passed a Node
    /// </summary>
    /// <param name="node"> the node</param>
    /// <param name="direction"> the direction Justin is moving</param>
    /// <returns> true if he passed it, false if he hasn't</returns>
    bool PassedNode(GameObject node, Vector2 direction)
    {
        if(direction == Vector2.right)
        {
            return (this.transform.position.x > node.transform.position.x);
        }
        if(direction == Vector2.left)
        {
            return (this.transform.position.x < node.transform.position.x);
        }
        if(direction == Vector2.up)
        {
            return (this.transform.position.y > node.transform.position.y);
        }
        if(direction == Vector2.down)
        {
            return (this.transform.position.y < node.transform.position.y);
        }
        return false;
    }

    /// <summary>
    /// checks to see if Justin got distracte and store that distraction in the field 'distraction'
    /// </summary>
    /// <returns>true if Justin got distracted and false if he hasn't</returns>
    bool CheckIfDistracted()
    {
        GameObject[] possibleDistractions = GameObject.FindGameObjectsWithTag("distraction");
        foreach(GameObject g in possibleDistractions)
        {
            //when E is pressed and you are in a certain Range and the distraction is not already in mainOrder
            if(Input.GetKey(KeyCode.E) == true && Vector2.Distance(GameObject.Find("Ghost").transform.position, g.transform.position) < .2f && g!=distraction){
                distraction = g;
                return true;
            }
            //When the distraction is already in the main order
            else if (g == distraction)
            {
                return true;
            }
        }   
        distraction = null;
        return false;
    }


}
