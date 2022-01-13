using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    
    GridMan gridman;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridman = FindObjectOfType<GridMan>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }
    
    void Start()
    {
        if(gridman != null)
        {
            coordinates = gridman.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridman.BlockNode(coordinates);
            }
        }
    }

    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }
   
    void OnMouseDown() 
    {
        if(gridman.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            //Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = !isPlaced;
            //Debug.Log(transform.name);
            gridman.BlockNode(coordinates);
        }
        
    }
}
