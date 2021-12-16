using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;
    Enemy enemy;

    
    
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
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
           
            gameObject.SetActive(false);
            enemy.GainGold();
           
            }
    }
   void DestroyGameObject()
    {
      Destroy(gameObject);
    }
}
