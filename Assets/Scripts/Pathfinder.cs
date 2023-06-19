using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    GridManager gridManager;
    public Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    // //turn this on for prev state
    public Vector2Int startcoordinates;
    public Vector2Int getstartcoordinates
    {
        get { return startcoordinates; }
    }
    //turn this on for prev state
    public Vector2Int destinationcoordinates;
    Node Startnode;
    Node Destinationnode;
    Node Currentnode;
    public List<Node> reached= new List<Node>();
    //queue
    public List<Vector2Int> directions = new List<Vector2Int>() { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    public Queue<Node> frontier= new Queue<Node>();
   public bool isbinarysearch = true;
    public List<Vector2Int> vectorlist;
    public List<Node> newpath;
    public List<Vector2Int> pathList;
    public event Action regeneratepath;
    EnemyMover enemyMover;
    public List<Vector2Int> frontierList = new List<Vector2Int>();
    //10,3 8,5 8,2 6,3
    public List<Transform> destinationpoints= new List<Transform>();
    // Start is called before the first frame update
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        enemyMover = FindObjectOfType<EnemyMover>();
        grid = gridManager.getGrid;
        
        //below line to be deleted
        vectorlist = gridManager.getgridList;

        Startnode = grid[startcoordinates];

        Destinationnode = grid[destinationcoordinates];
        //set the destination
        destinationpoints = Levelinstantiatedestination.instance.getdestinationpoints;
    }
    void Start()
    {

        
        //GetNewPath();

        //Debug.Log(newpath.Count);
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(startcoordinates);

    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        //very importatn when recalculating path we have to put starting coordinateds as transform .positon of the enemymover
        ResetNodes();
        BreadFirstSearch(coordinates);
        newpath = Buildpath();
      /*  Debug.Log("the path count "+newpath.Count);*/
        return newpath;
    }

    private void ResetNodes()
    {
       foreach(KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
            isbinarysearch = true;
        }
    }

    private void Exploreneighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourcords = Currentnode.coordinates + direction;
            if (gridManager.getGrid.ContainsKey(neighbourcords))
            {
                Node neighbournode = gridManager.GetNode(neighbourcords);
                neighbours.Add(neighbournode);
               
            }
        }
        foreach (Node neighbour in neighbours)
        {
           /* Debug.Log("the neighbours count "+neighbours.Count);*/
            if(!reached.Contains(neighbour) && neighbour.isWalkable)
            {
               
                reached.Add(neighbour);
               
                frontier.Enqueue(neighbour);
                frontierList.Add(neighbour.coordinates);
           /*     Debug.Log("the frontier count " + frontier.Count);*/
                neighbour.connectedTo = Currentnode;
            }

        }
     
      

    }

    private void BreadFirstSearch(Vector2Int coordinates)
    {
        reached.Clear();
        frontier.Clear();
        frontierList.Clear();
        startcoordinates = coordinates;
        
        //Debug.Log("the start coordinates"+startcoordinates);
        Startnode = grid[coordinates];

        //add here
        destinationcoordinates = gridManager.GetCoordinatesFromPosition(destinationpoints[UnityEngine.Random.Range(0, destinationpoints.Count)].transform.position);

        Destinationnode = grid[destinationcoordinates];
        //cant keep neigbours here else it itself would get added
        //start node and destination node initaialse here
    
        reached.Add(Startnode);
        frontier.Enqueue(Startnode);
        frontierList.Add(Startnode.coordinates);

        while (frontier.Count>0 && isbinarysearch)
        {
            //Debug.Log("here inside loop"+frontier.);
            Currentnode = frontier.Dequeue();
         /*   Debug.Log("current node "+Currentnode.coordinates);*/
            Exploreneighbours();
           
            if (Startnode==Destinationnode)
            {
                isbinarysearch = false;
            }
        }
    }
    private List<Node> Buildpath()
    {

        //Debug.Log("here in build path");
        List<Node> path = new List<Node>();
        Node Currentnode = Destinationnode;
        path.Add(Currentnode);
      
        pathList.Clear();
        Currentnode.isPath = true;
        pathList.Add(Currentnode.coordinates);
        //current node connected is null
        while (Currentnode.connectedTo!=null)
        {
          
            Currentnode = Currentnode.connectedTo;
           
            Currentnode.isPath = true;
          /*  Debug.Log("the current node " + Currentnode.coordinates);*/
            path.Add(Currentnode);
          
            pathList.Add(Currentnode.coordinates);

            
        }
     /*   if(pathList.Count<=1)
        {
            Debug.Log("the pathlist count less than 1 "); 
        }*/
        path.Reverse();
        pathList.Reverse();
        return path;
    }
    public bool WillBlockPath(Vector2Int coordinates)
    {
       
        //will block path
        if (grid.ContainsKey(coordinates))
        {
            bool previoustate = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            newpath = GetNewPath(); //i think the problem lies here in getnew path
        
            if(newpath.Count<=1)
            {
               /* Debug.Log("the new path");*/
                //rese the original path it will be same as before because the shortest path remains the same
                grid[coordinates].isWalkable = true;
               /* Debug.Log("here in iswalkable " + grid[coordinates].isWalkable);*/
                GetNewPath();
                //problem lies here i think
                return true;
            }

            else
            {
               
              /*  Debug.Log("the last second path "+newpath[newpath.Count-2].coordinates);*/
                regeneratepath?.Invoke();
                //i think the problem lies here in invokinf
            }


        }
        //trigger the event here
        return false;
    }
    private void Update()
    {/*if (pathList.Count == 1)
        {
            Debug.Log("the current node " + Currentnode.coordinates);
        }    */  
    }


}
