using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

	private Dictionary<string, Node> nodes;
    private Dictionary<string, int> numberOfDeadEnds; //for each node, its a running list of how many dead ends it leads to.
    private List<Node> path;
    

    public PathFinder(Dictionary<string, Node> nodes)
    {
        this.nodes = nodes;
        path = new List<Node>();
        foreach(KeyValuePair<string, Node> k in nodes)
        {
            numberOfDeadEnds.Add(k.Key, 0);
        }
    }
    /// <summary>
    /// possibly works in cases of there being a direct path from start to stop. Need to account for stepping back a generation of start to get to end
    /// </summary>
    /// <param name="start"></param>
    /// <param name="stop"></param>
    public void FindPath(Node start, Node stop)
    {
        //when you arrive at endpoint
        if(start.Name == stop.Name)
        {
            path.Add(start);
            return;
        }
        //dead end    or   you exaughsted all possible paths from one node with no success in reaching stop
        else if((start.NumberTouching == 1 && path.Count>1) || (numberOfDeadEnds[path[path.Count - 1].name] == path[path.Count - 1].NumberTouching - 1 && path[path.Count - 1].Generation != 0))
        {
            path[path.Count - 1].Ignore = true;
            path.RemoveAt(path.Count - 1);
            numberOfDeadEnds[path[path.Count - 1].name]++;
        }
        //there are still paths you can take branching from this node
        else if((numberOfDeadEnds[path[path.Count-1].name] > 0 && numberOfDeadEnds[path[path.Count-1].name] < path[path.Count - 1].NumberTouching-1) || (numberOfDeadEnds[start.name] == 0))
        {
            Node newStart = null;
            foreach(string name in path[path.Count - 1].touching)
            {
                if (!nodes[name].Ignore && nodes[name].Generation > path[path.Count-1].Generation)
                {
                    newStart = nodes[name];
                    path.Add(newStart);
                    break;
                }
            }
            FindPath(newStart, stop);         
        }
        
    }
}
