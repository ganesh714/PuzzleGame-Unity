using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button ExitBtn;
    public GameObject ExitMenu;

    public Button SettingsBtn;
    public GameObject SettingsMenu;

    public Button coinsBtn;
    public GameObject CoinsMenu;
    public TextMeshProUGUI textInCoinsMenu;
    public TextMeshProUGUI coinsText;

    public Button hunt;
    public AudioClip btnClickSound;  

    public Button HintAdBtn;
    public TextMeshProUGUI HintsCount;

    public Button SkipAdBtn;
    public TextMeshProUGUI SkipsCount;

    public GameObject AdsMenu;
    public TextMeshProUGUI textInAdsMenu;
    public Button GiveCoinsBtn;
    public TextMeshProUGUI textInGivCoinsBtn;
    private bool isHintClickOrSkip=true; 

    private AudioSource audioSource;

    public void Start(){

        // FindObjectOfType<CoinManager>()?.AddCoins(1000);
        // FindObjectOfType<SkipManager>()?.AddSkips(-90);

        audioSource = gameObject.AddComponent<AudioSource>();
// #IF !UNITY_EDITOR
     UpdateStatusBar();
        Upd_HSC_Counts();
        UpdateAnimations();
        
        InitializeUnlockData();

        
        MobileAds.Initialize(initStatus => { });

        SettingsBtn.onClick.AddListener(ShowSettingsMenu);
        coinsBtn.onClick.AddListener(ShowCoinsMenu);
        hunt.onClick.AddListener(OnHuntBtnClicked);
        HintAdBtn.onClick.AddListener(ShowAdsHintMenu);
        SkipAdBtn.onClick.AddListener(ShowAdsSkipMenu);
        GiveCoinsBtn.onClick.AddListener(GivHintOrSkip);
        //SkipAdBtn.GetComponent<Animator>().enabled = false;;
        ExitBtn.onClick.AddListener(ShowExitMenu);  
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(ExitMenu.activeInHierarchy == true)
                exit();
            else if(SettingsMenu.activeInHierarchy ==true)
                SettingsMenu.SetActive(false);
            else if(CoinsMenu.activeInHierarchy == true)
                CoinsMenu.SetActive(false);
            else if(AdsMenu.activeInHierarchy == true)
                AdsMenu.SetActive(false);
            else
                ExitMenu.SetActive(true);
        }
        //Upd_HSC_Counts();
    }
    
    public void OnAnyButtonClicked(){
        if(btnClickSound != null  && PlayerPrefs.GetInt("ClickSounds",1)==1)
            audioSource.PlayOneShot(btnClickSound);
    }




    void ShowExitMenu(){
        ExitMenu.SetActive(true);
    }
    public void CloseExitMenu(){
        ExitMenu.SetActive(false);
    }
    public void exit(){
        Application.Quit();
    }

    public void ShowSettingsMenu(){
        SettingsMenu.SetActive(true);
    }
    public void CloseSetMenu(){
        SettingsMenu.SetActive(false);
    }
    public void ShowCoinsMenu(){
        textInCoinsMenu.text = "Coins : " + FindObjectOfType<CoinManager>()?.GetCoins();
        CoinsMenu.SetActive(true);
    }
    public void closeCoinsMenu(){
        CoinsMenu.SetActive(false);
    }
    public void OnHuntBtnClicked()
    {
        if (btnClickSound != null)
        {
            if(btnClickSound != null && PlayerPrefs.GetInt("ClickSounds",1)==1)
                audioSource.PlayOneShot(btnClickSound);
            hunt.interactable = false;
            Invoke("LoadNextScene", (float)0.162000);
            
        }
        else{
            LoadNextScene();
        }
    }
    void LoadNextScene(){
        SceneManager.LoadSceneAsync(1);
    }



    void ShowAdsHintMenu(){
        if(FindObjectOfType<CoinManager>()?.GetCoins() < 200){
            GiveCoinsBtn.interactable = false;
        }
        else{
            GiveCoinsBtn.interactable = true;
        }
        textInAdsMenu.text = "Hints : " + FindObjectOfType<HintManager>()?.GetHints();
        textInGivCoinsBtn.text = "" + 200;
        isHintClickOrSkip = true;
        AdsMenu.SetActive(true);
    }
    void ShowAdsSkipMenu(){
        if(FindObjectOfType<CoinManager>()?.GetCoins() < 400){
            GiveCoinsBtn.interactable = false;
        }
        else{
            GiveCoinsBtn.interactable = true;
        }
        textInAdsMenu.text = "Skips : " + FindObjectOfType<SkipManager>()?.GetSkips();
        textInGivCoinsBtn.text = "" + 400;
        isHintClickOrSkip = false;
        AdsMenu.SetActive(true);
    }
    public void closeAdsMenu(){
        AdsMenu.SetActive(false);
    }
    void GivHintOrSkip(){
        if(isHintClickOrSkip){
            FindObjectOfType<HintManager>()?.AddHints(1);
            FindObjectOfType<CoinManager>()?.DecCoins(200);
            HintsCount.text = "" + FindObjectOfType<HintManager>()?.GetHints();
        }
        else{
            FindObjectOfType<SkipManager>()?.AddSkips(1);
            FindObjectOfType<CoinManager>()?.DecCoins(400);
            SkipsCount.text = "" + FindObjectOfType<SkipManager>()?.GetSkips();
        }
        closeAdsMenu();
    }
    void InitializeUnlockData(){
        if(!PlayerPrefs.HasKey("Highest Level Unlocked")){
            PlayerPrefs.SetInt("Highest Level Unlocked",1);
            PlayerPrefs.SetString("Skip Unlocked Data",new string('0',30));
            PlayerPrefs.SetString("Hint Unlocked Data",new string('0',30));
        }

    }


    void updGivCoinBtnInr(){
        int reqCoins = isHintClickOrSkip ? 200 : 400 ;
        int avblCoins = PlayerPrefs.GetInt("Coins",0);
        if(reqCoins > avblCoins){
            GiveCoinsBtn.interactable=false;
        }
    }
    public void UpdateAnimations(){
        Debug.Log("UpdAnim Func Called");
        if(PlayerPrefs.GetInt("Animations",1)==1){
            hunt.GetComponent<Animator>().enabled = true;
            SkipAdBtn.GetComponent<Animator>().enabled = true;
            HintAdBtn.GetComponent<Animator>().enabled = true;
            Debug.Log("NotUpd(on) Anim");
            return;
        } 
        hunt.GetComponent<Animator>().enabled = false;
        SkipAdBtn.GetComponent<Animator>().enabled = false;
        HintAdBtn.GetComponent<Animator>().enabled = false;
        Debug.Log("Updated(offed) Animations");
    }
    void UpdateStatusBar(){

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject window = currentActivity.Call<AndroidJavaObject>("getWindow");
            window.Call("clearFlags", 1024); // WindowManager.LayoutParams.FLAG_FULLSCREEN
            
        })); 
    }
    void Upd_HSC_Counts(){
        HintsCount.text = "" + FindObjectOfType<HintManager>()?.GetHints();
        SkipsCount.text = "" + FindObjectOfType<SkipManager>()?.GetSkips();
        coinsText.text = "" + FindObjectOfType<CoinManager>()?.GetCoins();
    } 
}
