using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]int goldGain = 25;
    [SerializeField]int goldLost = 25;

    Bank bank;
    
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

  public void GainGold()
  {
      if(bank == null) {return;}
      bank.Deposit(goldGain);
  }

public void LoseGold()
  {
      if(bank == null) {return;}
      bank.Withdraw(goldLost);
  }

}
