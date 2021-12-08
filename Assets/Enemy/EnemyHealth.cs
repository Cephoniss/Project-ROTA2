using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

 
    private void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
        Debug.Log("${currentHitPoints}"); 
    }
    
    void ProcessHit()
    {
    
    currentHitPoints--;
            
            if(currentHitPoints < 1)
            {
           
            DestroyGameObject();
           
            }
    }
   void DestroyGameObject()
    {
      Destroy(gameObject);
    }
}
