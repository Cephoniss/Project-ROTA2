using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] [Range(0f, 10f)]float speed = 1f;
    
    Enemy enemy;
    GridMan gridMan;
    Pathfinder pathfinder;

    List<Node> path = new List<Node>();
    
   void OnEnable()
    {
        ReturnToStart();
        FindPath(true);
        
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridMan = FindObjectOfType<GridMan>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridMan.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            
            transform.LookAt(endPosition);
            
            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            //transform.position = waypoint.transform.position;
            //yield return new WaitForSeconds(waitTime);
        }
        
        FinishPath();
    }   
    
    
    void FinishPath()
    {
        enemy.LoseGold();
        gameObject.SetActive(false);
    }
    
    void FindPath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        
        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
           coordinates = gridMan.GetCoordinatesFromPosition(transform.position);
        }
        
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
        
        //GameObject parent = GameObject.FindGameObjectWithTag("Path");

        //foreach(Transform child in parent.transform)
        // {
        //    Waypoint waypoint = child.GetComponent<Waypoint>();
        //    if(waypoint != null)
        //    {
        //        path.Add(waypoint);
        //    }
        //}

        
    }
    
    void ReturnToStart()
    {
        transform.position = gridMan.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
