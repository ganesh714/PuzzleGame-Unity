using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayBtn : MonoBehaviour
{
    public Button playBtn;
    public Image LockImg;
    //public TextMeshProUGUI lvlTxt;
    //private string cardName; //= transform.parent.name;
    private int levelnumber; //= Cardtolevel(cardName);
    void Start()
    {
        //cardName = transform.parent.name;
        levelnumber = Cardtolevel(transform.parent.name);
        //lvlTxt.text = "Level " + levelnumber;
        if(levelnumber > PlayerPrefs.GetInt("Highest Level Unlocked",1)){
            playBtn.gameObject.SetActive(false);
            LockImg.gameObject.SetActive(true);
        }
        else{
            playBtn.gameObject.SetActive(true);
            LockImg.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void PlayGame()//, AudioClip onbtnClickSound)
    {
        //string cardName=transform.parent.name;
        
        //Debug.Log("Card :"+ cardName);
        
        //int levelnumber=Cardtolevel(cardName);
        PlayerPrefs.SetInt("selectedLevel",levelnumber);
        
        Debug.Log("lvl :"+ levelnumber);
        
        if( FindObjectOfType<S1LevelsController>().btnClickSound != null){ 
            if(FindObjectOfType<S1LevelsController>().btnClickSound  != null && PlayerPrefs.GetInt("ClickSounds",1)==1)
                FindObjectOfType<S1LevelsController>()?.OnAnyButtonClicked();
            Invoke("LoadNextScene",(float)0.1622);
        }
        else{
            LoadNextScene();
        }
    }

    void LoadNextScene(){
        SceneManager.LoadSceneAsync(2);
    }
    private int Cardtolevel(string card)
    {
        int lvl=0;
        for (int i = 6; card[i]!= ')'; i++)
        {
            lvl= (lvl*10) + (card[i]-'0');
        }
        return lvl;
    }
}
