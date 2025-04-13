using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    private const string key = "Hints";
    void Start()
    {
        if(!PlayerPrefs.HasKey(key)){
            PlayerPrefs.SetInt(key,4);
            PlayerPrefs.Save();
        }
    }
    public int GetHints(){
       return PlayerPrefs.GetInt(key,0);
    }
    public void AddHints(int amount){
        int currentHints = GetHints();
        currentHints += amount;
        PlayerPrefs.SetInt(key,currentHints);
        PlayerPrefs.Save();
    }
    public void DecHints(int amount){
        int currentHints = GetHints();
        currentHints -= amount;
        PlayerPrefs.SetInt(key,currentHints);
        PlayerPrefs.Save();
    }

}
