using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]
	private List<GameObject> mainOrder = new List<GameObject>();
    private Node lastNodeTouched;
    private bool distracted;
    public float speed;
	

    // Update is called once per frame
    void Update() {
        //move Justin when he is not distracted and he is inbetween 2 nodes.
        if (!distracted && (!PassedNode(mainOrder[1] , GetDirectionBetweenNodes(mainOrder[0], mainOrder[1]))))
        {
            this.transform.Translate(GetDirectionBetweenNodes(mainOrder[0], mainOrder[1]) * Time.deltaTime * speed);
        }
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
}
