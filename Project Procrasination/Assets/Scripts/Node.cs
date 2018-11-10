using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    [SerializeField]
    private string name;
    [SerializeField]
    private float x, y;
    private bool partOfMain;
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
    public bool PartOfMain
    {
        get { return partOfMain; }
    }  

    public Node(string name, float x, float y)
    {
        this.name = name;
        this.x = x;
        this.y = y;
    }
}
