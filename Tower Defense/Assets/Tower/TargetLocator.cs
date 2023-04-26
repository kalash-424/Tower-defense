using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] ParticleSystem projectile_bolts;
    [SerializeField] Transform weapon;
    [SerializeField] float range = 20f; 
     Transform target;
   
    void Update()
    {
        FindClosestTarget();
       Aim(); 
    }

    void FindClosestTarget(){
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closest_target = null;
        float maxDistance = Mathf.Infinity;
// Find min value(distance) in array
        foreach(Enemy enemy in enemies){
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance){
                closest_target = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closest_target;
    }

    void Aim(){
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);
        if(targetDistance <= range) Shoot(true);
        else Shoot(false);
    }
    void Shoot(bool isActive){
        var emissionModule = projectile_bolts.emission;
        emissionModule.enabled = isActive;
    }
}
