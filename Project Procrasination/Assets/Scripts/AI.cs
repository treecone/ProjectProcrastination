using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    enum Rooms
    {
        BedRoom,
        BathRoom,
        LivingRoom,
        FrontDoor,
        DiningRoom,
        Kitchen,
        Garage
    }

    [SerializeField]
	private List<GameObject> mainOrder = new List<GameObject>();
    private GameObject distraction;
    private bool needsToFinishDistraction;
    private Rooms room;
    private Dictionary<Rooms, GameObject> nodeInRoom = new Dictionary<Rooms, GameObject>();
    private Vector2 lastFramePosition;
    private bool waitForDistraction;
    private int updateCount = 0;
    private int numTimesVisitKitchen;
    public float speed;

    void Awake()
    {
        nodeInRoom.Add(Rooms.BathRoom, GameObject.Find("node1"));
        nodeInRoom.Add(Rooms.BedRoom, GameObject.Find("node2"));
        nodeInRoom.Add(Rooms.LivingRoom, GameObject.Find("node3"));
        nodeInRoom.Add(Rooms.FrontDoor, GameObject.Find("node4"));
        nodeInRoom.Add(Rooms.DiningRoom, GameObject.Find("node5"));
        nodeInRoom.Add(Rooms.Kitchen, GameObject.Find("node6"));
        nodeInRoom.Add(Rooms.Garage, GameObject.Find("node7"));
        gameObject.GetComponent<Animator>().SetInteger("Animation State", 1);
    }
	

    // Update is called once per frame
    void Update() {
        UpdateRoom();
        CheckIfDistracted();
        if (!waitForDistraction)
        {
            //every five frames the direction the player faces is re-accessed
            if (updateCount == 5)
            {
                SetDirection();
                lastFramePosition = this.transform.position;
                updateCount = 0;
            }
            else
            {
                updateCount++;
            }

            //move Justin when he is not distracted and he is inbetween 2 nodes.
            if (distraction == null && (!PassedNode(mainOrder[1], GetDirectionBetweenNodes(mainOrder[0], mainOrder[1]))))
            {
                this.transform.Translate(GetDirectionBetweenNodes(mainOrder[0], mainOrder[1]) * Time.deltaTime * speed);
            }
            //once Justin arrives at the node, delete it from mainOrder
            else if (PassedNode(mainOrder[1], GetDirectionBetweenNodes(mainOrder[0], mainOrder[1])))
            { 
                mainOrder.RemoveAt(0);
                CheckKeyEvent();
                
            }
            //at the distraction
            else if (Vector2.Distance(GameObject.Find("Justin").transform.position, distraction.transform.position) < .2f && needsToFinishDistraction)
            {
                needsToFinishDistraction = false;
                gameObject.GetComponent<Animator>().SetInteger("Animation State", distraction.GetComponent<DistractionNodeScript>().number);
                StartCoroutine(Timer(5));
            }
            //Justin is on his way to the distraction
            else if (distraction != null && needsToFinishDistraction)
            {
                this.transform.position = Vector2.Lerp(this.transform.position, distraction.transform.position, .01f);
            }
            //Justin moves from distraction back to node
            else if (!needsToFinishDistraction)
            {

                gameObject.GetComponent<Animator>().SetInteger("Animation State", 1);
                this.transform.position = Vector2.Lerp(this.transform.position, nodeInRoom[room].transform.position, .01f);
                if (Vector2.Distance(GameObject.Find("Justin").transform.position, nodeInRoom[room].transform.position) < .2f)
                {
                    distraction = null;
                }
            }
        }
    }

    /// <summary>
    /// checks to see if Justin got distracte and store that distraction in the field 'distraction'
    /// </summary>
    /// <returns>true if Justin got distracted and false if he hasn't</returns>
    bool CheckIfDistracted()
    {
        GameObject[] possibleDistractions = GameObject.FindGameObjectsWithTag("distraction");
        foreach (GameObject g in possibleDistractions)
        {
            //when E is pressed and you are in a certain Range and the player isn't aready distracted by g. Or you are at the Kitchen for the first time
            if ((Input.GetKey(KeyCode.E) == true && Vector2.Distance(GameObject.Find("Ghost").transform.position, g.transform.position) < 1f && g != distraction) || (numTimesVisitKitchen == 1 && g.name == "Stove"))
            {
                if (room == Rooms.BathRoom)
                {
                    if (GameObject.Find("MedicineCabinet") == g || GameObject.Find("Toliet") == g)
                    {
                        distraction = g;
                        needsToFinishDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.BedRoom)
                {
                    if (GameObject.Find("BrowseMemes") == g || GameObject.Find("Bed") == g || GameObject.Find("Table") == g || GameObject.Find("Lamp") == g)
                    {
                        distraction = g;
                        needsToFinishDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.LivingRoom)
                {
                    if (GameObject.Find("Couch") == g || GameObject.Find("TV") == g)
                    {
                        distraction = g;
                        needsToFinishDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.FrontDoor)
                {
                    if (GameObject.Find("Shoes") == g)
                    {
                        distraction = g;
                        needsToFinishDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.DiningRoom)
                {
                    if (GameObject.Find("Pet") == g)
                    {
                        distraction = g;
                        needsToFinishDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.Kitchen)
                {
                    if (GameObject.Find("KitchenSink") == g || GameObject.Find("Stove") == g || GameObject.Find("KitchenSink") == g || GameObject.Find("Dishwasher") == g || GameObject.Find("Fridge") == g)
                    {
                        distraction = g;
                        needsToFinishDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.Garage)
                {

                }
            }
            //When the g is already the distraction
            else if (g == distraction)
            {
                return true;
            }
        }
        distraction = null;
        return false;
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
            return (this.transform.position.x > node.transform.position.x );
        }
        if(direction == Vector2.left )
        {
            return (this.transform.position.x < node.transform.position.x);
        }
        if(direction == Vector2.up)
        {
            return (this.transform.position.y > node.transform.position.y );
        }
        if(direction == Vector2.down)
        {
            return (this.transform.position.y < node.transform.position.y );
        }
        return false;
    }

   

    void UpdateRoom()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if(x > -5.5 && x < 2.5)
        {
            room = Rooms.BedRoom;
        }
        else if( x > 3.5)
        {
            room = Rooms.BathRoom;
        }
        else if(x > -14.5 && x < -6.5)
        {
            room = Rooms.LivingRoom;
        }
        else if( x > -24.5 && x< -14.5)
        {
            room = Rooms.FrontDoor;
        }
        else if( x < -24.5 && y< 6.5)
        {
            room = Rooms.DiningRoom;
        }
        else if( y > 9.5 && y < 14.5)
        {
            room = Rooms.Kitchen;
        }
        else if(y > 18.5)
        {
            room = Rooms.Garage;
        }
    }

    void SetDirection()
    {
        if(this.transform.position.x > lastFramePosition.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void CheckKeyEvent()
    {
        //shower
        if (room == Rooms.BathRoom)
        {
            StartCoroutine(Timer(5));
        }
        //make food
        if (room == Rooms.Kitchen)
        {
            numTimesVisitKitchen++;
        }
    }

    IEnumerator Timer(int time)
    {
        waitForDistraction = true;
        yield return new WaitForSeconds(time);
        waitForDistraction = false;
    }
}
