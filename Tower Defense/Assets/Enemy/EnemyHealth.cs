using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 4;

    [Tooltip("Increses enemy health on next spawning whenever enemy dies")]
    [SerializeField] int difficultylevel = 1;
    int currenthitpoints = 0;
    
    Enemy enemy;

    void OnEnable()
    {
        currenthitpoints = maxHitPoints;
    }

    void Start(){
        enemy = GetComponent<Enemy>();

    }

    void OnParticleCollision(GameObject other)
    {
       Hit();
    }

    void Hit(){
        currenthitpoints--;

        if(currenthitpoints <= 0){
            gameObject.SetActive(false);
            maxHitPoints += difficultylevel;
            enemy.Rewardmoney();
        }
    }
}


