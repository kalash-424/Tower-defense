using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Tooltip("World Grid Size - should match Unity Editor snap settings")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get{ return unityGridSize;} }

    [SerializeField] Vector2Int gridSize;
    [SerializeField] Vector2Int startingNode;  //starting node SerializeField

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get{ return grid;} }

    void Awake(){
        CreateGrid();
    }

    public Node getNode(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes(){
        foreach (KeyValuePair<Vector2Int, Node> entry in grid){
            entry.Value.parentNode = null;
            entry.Value.visited = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position){
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
        
        return coordinates;
    }

      public Vector3 GetPositionFromCoordinates(Vector2Int coordinates){
        Vector3 position = new Vector3();
        position.x = (coordinates.x * unityGridSize);
        position.z = (coordinates.y * unityGridSize);
        
        return position;
    }


    void CreateGrid(){
        int x = startingNode.x , y = startingNode.y;    //initiate the starting node for the loop

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2Int coordinates = new Vector2Int(x + i, y + j);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
