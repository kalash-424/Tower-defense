using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int reward = 25;
    [SerializeField] int penalty = 25;

    Bank playerbank;
    // Start is called before the first frame update
    void Start()
    {
        playerbank = FindObjectOfType<Bank>();
    }

    public void Rewardmoney(){
        if(playerbank == null) return;
        playerbank.Deposit(reward);
    }

    public void Stealmoney(){
        if(playerbank == null) return;
        playerbank.Withdrawal(penalty);
    }
}
