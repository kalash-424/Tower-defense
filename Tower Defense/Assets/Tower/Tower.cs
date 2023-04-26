using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    void Start(){
        StartCoroutine(Build());
    }

   public bool BuildTower(Tower towerprefab, Vector3 position){
        Bank playerbank = FindObjectOfType<Bank>();

        if(playerbank == null) return false;


        if(playerbank.CurrentBalance >= cost){ 
            playerbank.Withdrawal(cost);
            Instantiate(towerprefab, position, Quaternion.identity);
            return true;
        }

        return false;
    }

    IEnumerator Build(){
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }


    }

}
