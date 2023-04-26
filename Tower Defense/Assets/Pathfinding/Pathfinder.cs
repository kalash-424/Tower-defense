using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoord;
    public Vector2Int Startcoord { get{return startCoord;} }

    [SerializeField] Vector2Int destCoord;
    public Vector2Int Destcoord { get{return destCoord;} }


    Node startNode;
    Node destNode;
    Node currentNode;

    Queue<Node> q = new Queue<Node>();

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int,Node>();


    void Awake(){
        gridManager = FindObjectOfType<GridManager>();

        if(gridManager != null){ 
            grid = gridManager.Grid;
            startNode = grid[startCoord];
            destNode = grid[destCoord];
            
        }

    }

    void Start()
    {
        GetNewPath();
    }


    public List<Node> GetNewPath(){
        return GetNewPath(startCoord);
    }

    //overloaded method to pass initial coordinates for BFS traversal
    public List<Node> GetNewPath(Vector2Int coordinates){
        gridManager.ResetNodes();
        BFS(coordinates);
        return buildPath();
    }


    void exploreNeighnors(){
        List<Node> neighbor = new List<Node>();

        //making adjaceny list
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoord = currentNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoord)){
                neighbor.Add(grid[neighborCoord]);
            }
        }

        //traversing neighbors
        foreach (Node nb in neighbor)
        {
            if(!reached.ContainsKey(nb.coordinates) && nb.isWalkable){
                nb.parentNode = currentNode;
                reached.Add(nb.coordinates, nb);
                q.Enqueue(nb);
            }
        }
    }

    //BFS algo
    void BFS(Vector2Int coordinates){
        startNode.isWalkable = true;
        destNode.isWalkable = true;

        q.Clear();
        reached.Clear();

        if(startNode == null) return;

        q.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(q.Count > 0){
            currentNode = q.Dequeue();
            currentNode.visited = true;
            if(currentNode.coordinates == destCoord) break;
            exploreNeighnors(); 
        }

    }

    List<Node> buildPath(){
        List<Node> path = new List<Node>();
        Node curr = destNode;

        path.Add(curr);
        curr.isPath = true;

        while(curr.parentNode != null){
            path.Add(curr.parentNode);
            curr = curr.parentNode;
            curr.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool willBlockPath(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            bool prevState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = prevState;

            if(newPath.Count <= 1){
                GetNewPath();
                return true;
            }

        }

        return false;
    }

    public void NotifyRecievers(){
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

}
