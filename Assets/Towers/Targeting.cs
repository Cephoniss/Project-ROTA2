using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 25;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;
    
    

    // Update is called once per frame
    void Update()
    {
        LocateTarget();
        AimWeapon();
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if(targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void LocateTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform targetClose = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
            targetClose = enemy.transform;
            maxDistance = targetDistance;
            }
        }

        target = targetClose;
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

}
