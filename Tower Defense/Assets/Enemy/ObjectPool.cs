using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 30f)] float spawn_interval = 1f;
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0, 50)] int poolsize = 4;

    GameObject[] pool;

    void Awake(){
         populatePool();
    }

    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    void populatePool(){
        pool = new GameObject[poolsize];

        for(int i=0; i< poolsize; i++){
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableEnemyInPool(){
        for(int i=0; i< poolsize; i++){
            if(pool[i].activeInHierarchy == false){
                pool[i].SetActive(true);
                return;
            } 
            
        }
    }

    IEnumerator spawnEnemy(){

        while(true){
           EnableEnemyInPool();
            yield return new WaitForSeconds(spawn_interval);
        }
    }
    

 }

