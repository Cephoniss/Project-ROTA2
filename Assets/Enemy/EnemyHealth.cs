using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] int addHP = 1;
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
            maxHitPoints += addHP;
            enemy.GainGold();
           
            }
    }
   void DestroyGameObject()
    {
      Destroy(gameObject);
    }
}
