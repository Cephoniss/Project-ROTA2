using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;
    
    GridMan gridMan;
    
    Queue<Node> frontier = new Queue<Node>();

    Node currentSearchNode;
    Node startNode;
    Node destinationNode;
  
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    
    

    void Awake()
    {
        gridMan = FindObjectOfType<GridMan>();
        if(gridMan != null)
        {
            grid = gridMan.Grid;
        }

        
    }


    void Start()
    {
        startNode = gridMan.Grid[startCoordinates];
        destinationNode = gridMan.Grid[destinationCoordinates];
        
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridMan.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        
        foreach(Vector2Int directions in directions)
        {
            Vector2Int neighborsCoords = currentSearchNode.coordinates + directions;
            
            if(grid.ContainsKey(neighborsCoords))
            {
                neighbors.Add(grid[neighborsCoords]);
                //grid[neighborsCoords].isExplored = true;
                //grid[currentSearchNode.coordinates].isPath = true;
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
   }
    
    void BreadthFirstSearch()
    {
        frontier.Clear();
        reached.Clear();
        
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        
        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }
}
