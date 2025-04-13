using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private const string Coinskey = "Coins";
    void Start()
    {
        if(!PlayerPrefs.HasKey(Coinskey)){
            PlayerPrefs.SetInt(Coinskey,1000);
            PlayerPrefs.Save();
        }
    }
    public int GetCoins(){
       return PlayerPrefs.GetInt("Coins",0);
    }
    public void AddCoins(int amount){
        int currentCoins = GetCoins();
        currentCoins += amount;
        PlayerPrefs.SetInt(Coinskey,currentCoins);
        PlayerPrefs.Save();
    }
    public void DecCoins(int amount){
        int currentCoins = PlayerPrefs.GetInt("Coins",0);
        currentCoins -= amount;
        PlayerPrefs.SetInt("Coins",currentCoins);
        PlayerPrefs.Save();
    }

}
