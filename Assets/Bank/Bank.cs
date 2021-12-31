using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBalance {get{return currentBalance; }}
    [SerializeField] TextMeshProUGUI displayGold;


    void Awake()
    {
        currentBalance = startBalance;
        UpdateGold();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateGold();
    }

    public void Withdraw(int amout)
    {
        currentBalance -= Mathf.Abs(amout);
        UpdateGold();

        if(currentBalance < 0)
        {
            ReloadScene();
        }
    }

    void UpdateGold()
    {
        displayGold.text = "Gold:" + currentBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
