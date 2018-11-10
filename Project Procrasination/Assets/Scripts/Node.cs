using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    private string name;
    private float x, y;
    public List<string> touching;
    private int numberTouching;
    private bool ignore;
    private int generation; //first generation = 0

    public float X
    {
        get { return x; }
    }
    public float Y
    {
        get { return y; }
    }
    public string Name
    {
        get { return name; }
    }
   
    public int NumberTouching
    {
        get { return touching.Count; }
    }
    public bool Ignore
    {
        get { return ignore; }
        set { ignore = value; }
    }
    public int Generation
    {
        get { return generation; }
    }
    public Node(List<string> touching, string name, float x, float y, int generation)
    {
        this.touching = touching;
        this.name = name;
        this.x = x;
        this.y = y;
        this.generation = generation;
    }
}
