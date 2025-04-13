using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipManager : MonoBehaviour
{
    private const string key = "Skips";
    void Start()
    {
        if(!PlayerPrefs.HasKey(key)){
            PlayerPrefs.SetInt(key,2);
            PlayerPrefs.Save();
        }
    }
    public int GetSkips(){
       return PlayerPrefs.GetInt(key,0);
    }
    public void AddSkips(int amount){
        int currentSkips = GetSkips();
        currentSkips += amount;
        PlayerPrefs.SetInt(key,currentSkips);
        PlayerPrefs.Save();
    }
    public void DecSkips(int amount){
        int currentSkips = GetSkips();
        currentSkips -= amount;
        PlayerPrefs.SetInt(key,currentSkips);
        PlayerPrefs.Save();
    }

}