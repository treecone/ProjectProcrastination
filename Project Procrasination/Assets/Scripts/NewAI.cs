using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAI : MonoBehaviour
{ 

    //Kitchen Sink Does not work. invalid index
    public enum Rooms
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
    private int updateCount = 0;
    public bool enteringKeyDistraction, exitingKeyDistraction, enactKeyDistraction;
    private bool enteringGhostDistraction, exitingGhostDistraction, enactGhostDistraction;
    private bool waitForDistraction;
    private Dictionary<Rooms, int> numTimesEnteringRooms = new Dictionary<Rooms, int>();
    public float speed;

    /// <summary>
    /// initialize dictionary with keyvalue pairs of room and node
    /// </summary>
    void Awake()
    {
        nodeInRoom.Add(Rooms.BathRoom, GameObject.Find("node1"));
        nodeInRoom.Add(Rooms.BedRoom, GameObject.Find("node2"));
        nodeInRoom.Add(Rooms.LivingRoom, GameObject.Find("node3"));
        nodeInRoom.Add(Rooms.FrontDoor, GameObject.Find("node4"));
        nodeInRoom.Add(Rooms.DiningRoom, GameObject.Find("node5"));
        nodeInRoom.Add(Rooms.Kitchen, GameObject.Find("node6"));
        nodeInRoom.Add(Rooms.Garage, GameObject.Find("node7"));
        numTimesEnteringRooms[Rooms.BathRoom] = 0;
        numTimesEnteringRooms[Rooms.BedRoom] = 0;
        numTimesEnteringRooms[Rooms.LivingRoom] = 0;
        numTimesEnteringRooms[Rooms.FrontDoor] = 0;
        numTimesEnteringRooms[Rooms.DiningRoom] = 0;
        numTimesEnteringRooms[Rooms.Kitchen] = 0;
        numTimesEnteringRooms[Rooms.Garage] = 0;
        gameObject.GetComponent<Animator>().SetInteger("Animation State", 1);
    }
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        ReaccessDirection();
        UpdateWhichRoomPlayerIsIn();
        UpdateNumTimesEnteringRoom();
        if (TriggeredKeyDistraction() || enteringKeyDistraction || enactKeyDistraction || exitingKeyDistraction)
        {
            FollowKeyDistraction();
        }
        else if (TriggeredGhostDistraction() || enteringGhostDistraction || enactGhostDistraction || exitingGhostDistraction)
        {
            FollowGhostDistraction();
        }
        else
        {
            MoveBetweenNodes();
        }
    }
    /// <summary>
    /// changes the direction that Justin is facing
    /// </summary>
    void ReaccessDirection()
    {
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
    }

    /// <summary>
    /// flips Justins direction as needed
    /// </summary>
    void SetDirection()
    {
        if (this.transform.position.x > lastFramePosition.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /// <summary>
    /// updates the room variable to store which room the player is in
    /// </summary>
    void UpdateWhichRoomPlayerIsIn()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if (x > -5.5 && x < 2.5)
        {
            room = Rooms.BedRoom;
        }
        else if (x > 3.5)
        {
            room = Rooms.BathRoom;
        }
        else if (x > -14.5 && x < -6.5)
        {
            room = Rooms.LivingRoom;
        }
        else if (x > -24.5 && x < -14.5)
        {
            room = Rooms.FrontDoor;
        }
        else if (x < -24.5 && y < 6.5)
        {
            room = Rooms.DiningRoom;
        }
        else if (y > 9.5 && y < 14.5)
        {
            room = Rooms.Kitchen;
        }
        else if (y > 18.5)
        {
            room = Rooms.Garage;
        }
    }
    /// <summary>
    /// increases the number of times the player entered said room when they reach that rooms node.
    /// </summary>
    void UpdateNumTimesEnteringRoom()
    {
        if (PassedNode(mainOrder[1], GetDirectionBetweenNodes(mainOrder[0], mainOrder[1])))
        {
            numTimesEnteringRooms[room]++;
        }
    }

    /// <summary>
    /// checks to see if Justin passed a Node
    /// </summary>
    /// <param name="node"> the node</param>
    /// <param name="direction"> the direction Justin is moving</param>
    /// <returns> true if he passed it, false if he hasn't</returns>
    bool PassedNode(GameObject node, Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            return (this.transform.position.x > node.transform.position.x);
        }
        if (direction == Vector2.left)
        {
            return (this.transform.position.x < node.transform.position.x);
        }
        if (direction == Vector2.up)
        {
            return (this.transform.position.y > node.transform.position.y);
        }
        if (direction == Vector2.down)
        {
            return (this.transform.position.y < node.transform.position.y);
        }
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
        if (firstNode.transform.position.x < secondNode.transform.position.x)
        {
            return Vector2.right;
        }
        if (firstNode.transform.position.x > secondNode.transform.position.x)
        {
            return Vector2.left;
        }
        if (firstNode.transform.position.y < secondNode.transform.position.y)
        {
            return Vector2.up;
        }
        return Vector2.down;
    }

    bool TriggeredKeyDistraction()
    {
        if (room == Rooms.BathRoom && PassedNode(mainOrder[1], GetDirectionBetweenNodes(mainOrder[0], mainOrder[1])) && numTimesEnteringRooms[room] == 1)
        {
            enteringKeyDistraction = true;
            distraction = GameObject.Find("Shower");
            this.transform.position = Vector2.Lerp(this.transform.position, distraction.transform.position, .01f);
            return true;
        }
        else if (room == Rooms.Kitchen && numTimesEnteringRooms[room] == 1)
        {
            enteringKeyDistraction = true;
            distraction = GameObject.Find("Stove");
            this.transform.position = Vector2.Lerp(this.transform.position, distraction.transform.position, .01f);
            return true;
        }
        return false;
    }

    void FollowKeyDistraction()
    {
        if (!waitForDistraction)
        {
            if (Vector2.Distance(GameObject.Find("Justin").transform.position, distraction.transform.position) < .2f && enteringKeyDistraction == true)
            {
                if (distraction.name == "Shower")
                {
                    enteringKeyDistraction = false;
                    enactKeyDistraction = true;
                    StartCoroutine(ShowerTimer(5));
                }
                if(distraction.name == "Stove")
                {
                    enteringKeyDistraction = false;
                    enactKeyDistraction = true;
                    StartCoroutine(KitchenTimer(5));
                }
            }
            else if (enactKeyDistraction)
            {
                enactKeyDistraction = false;
                exitingKeyDistraction = true;
                this.transform.position = Vector2.Lerp(this.transform.position, nodeInRoom[room].transform.position, .01f);
            }
            else if(enteringKeyDistraction)
            {
                this.transform.position = Vector2.Lerp(this.transform.position, distraction.transform.position, .01f);
            }
            else if (exitingKeyDistraction && Vector2.Distance(GameObject.Find("Justin").transform.position, nodeInRoom[room].transform.position) < .05f)
            {
                exitingKeyDistraction = false;
            }
            else if (exitingKeyDistraction)
            {
                this.transform.position = Vector2.Lerp(this.transform.position, nodeInRoom[room].transform.position, .01f);
            }      
        }
    }

    bool TriggeredGhostDistraction()
    {
        GameObject[] possibleDistractions = GameObject.FindGameObjectsWithTag("GhostDistraction");
        foreach (GameObject g in possibleDistractions)
        {
            if (Input.GetKey(KeyCode.Space) == true && Vector2.Distance(GameObject.Find("Ghost").transform.position, g.transform.position) < 1f)
            {
                if (room == Rooms.BathRoom)
                {
                    if (GameObject.Find("MedicineCabinet") == g || GameObject.Find("Toliet") == g)
                    {
                        distraction = g;
                        enteringGhostDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.BedRoom)
                {
                    if (GameObject.Find("BrowseMemes") == g || GameObject.Find("Bed") == g || GameObject.Find("Table") == g || GameObject.Find("Lamp") == g)
                    {
                        distraction = g;
                        enteringGhostDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.LivingRoom)
                {
                    if (GameObject.Find("Couch") == g || GameObject.Find("TV") == g)
                    {
                        distraction = g;
                        enteringGhostDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.FrontDoor)
                {
                    if (GameObject.Find("Shoes") == g)
                    {
                        distraction = g;
                        enteringGhostDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.DiningRoom)
                {
                    if (GameObject.Find("Pet") == g)
                    {
                        distraction = g;
                        enteringGhostDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.Kitchen)
                {
                    if (GameObject.Find("KitchenSink") == g || GameObject.Find("Stove") == g || GameObject.Find("KitchenSink") == g || GameObject.Find("Dishwasher") == g || GameObject.Find("Fridge") == g)
                    {
                        distraction = g;
                        enteringGhostDistraction = true;
                        return true;
                    }
                }
                else if (room == Rooms.Garage)
                {

                }
            }
        }
        return false;
    }

    void FollowGhostDistraction()
    {
        if (!waitForDistraction)
        {
            if (Vector2.Distance(GameObject.Find("Justin").transform.position, distraction.transform.position) < .2f && enteringGhostDistraction == true)
            {
                enteringGhostDistraction = false;
                enactGhostDistraction = true;
                this.GetComponent<Animator>().SetInteger("Animation State", distraction.GetComponent<DistractionNodeScript>().number);
                StartCoroutine(GeneralTimer(5));
            }
            else if (enactGhostDistraction)
            {
                enactGhostDistraction = false;
                exitingGhostDistraction = true;
                this.transform.position = Vector2.Lerp(this.transform.position, nodeInRoom[room].transform.position, .01f);
            }
            else if (enteringGhostDistraction)
            {
                this.transform.position = Vector2.Lerp(this.transform.position, distraction.transform.position, .01f);
            }
            else if (exitingGhostDistraction && Vector2.Distance(GameObject.Find("Justin").transform.position, nodeInRoom[room].transform.position) < .05f)
            {
                exitingGhostDistraction = false;
            }
            else if (exitingGhostDistraction)
            {
                this.transform.position = Vector2.Lerp(this.transform.position, nodeInRoom[room].transform.position, .01f);
            }
        }
    }
    void MoveBetweenNodes()
    {
        if(PassedNode(mainOrder[1], GetDirectionBetweenNodes(mainOrder[0], mainOrder[1])))
        {
            mainOrder.RemoveAt(0);
        }
        else
        {
            this.transform.Translate(GetDirectionBetweenNodes(mainOrder[0], mainOrder[1]) * Time.deltaTime * speed);
        }
    }

    IEnumerator ShowerTimer(int time)
    {
        GameObject.Find("Shower").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ShowerInUse");
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        waitForDistraction = true;
        yield return new WaitForSeconds(time);
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        GameObject.Find("Shower").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ShowerEmpty");
        waitForDistraction = false;
    }
    IEnumerator KitchenTimer(int time)
    {
        //add change of animations for kitchen
        waitForDistraction = true;
        yield return new WaitForSeconds(time);
        //revert changes
        waitForDistraction = false;
    }
    IEnumerator GeneralTimer(int time)
    {
        waitForDistraction = true;
        yield return new WaitForSeconds(time);
        this.GetComponent<Animator>().SetInteger("Animation State", 1);
        waitForDistraction = false;
    }
}
