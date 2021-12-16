using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 10f)]float speed = 1f;
    Enemy enemy;

    
   void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
        
        enemy.LoseGold();
        gameObject.SetActive(false);
    }   
    
    void FindPath()
    {
        path.Clear();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }

        
    }
    
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
