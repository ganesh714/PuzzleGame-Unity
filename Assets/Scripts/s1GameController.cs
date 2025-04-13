using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class s1GameController : MonoBehaviour
{
    public AudioClip btnClickSound;
    public AudioClip correctAnswerSound;
    public AudioClip WrongAnswerSound;
    private AudioSource audioSource;

    public GameObject pauseMenu;
    public Button PauseBtn;
    public Button clsBtnInPause;
    public Button hmBtnInPas;
    public Button resmBtnInPas;
    public Button extBtnInPas;

    public TextMeshProUGUI lvlTxt;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI qsnTxt;
    public Image qsnImg;
    public InputField userAnswer;
    public Image ansbordr;
    public TextMeshProUGUI ansRev;
    public Button verBtn;
    public S1StringsData stringsData;

    public GameObject hintMenu;
    public Button clsBtnInHint;
    public Image hintImg;
    public Button hintBtn;
    public TextMeshProUGUI HintsCount;
    //private string hintUnLData;
    private char[] hintUnLDataArr;

    public GameObject triesMenu;
    public Button clsBtnInTris;
    public TextMeshProUGUI textInTriesMenu;
    public TextMeshProUGUI textInGivCoinsBtn;
    public Button givCoinsBtn;

    private bool isHintClickOrSkip=true;

    public GameObject SkipMenu;
    public Button clsBtnInSkip;
    public Image AnsExpImg;
    public Button extBtnInSkip;
    public Button homBtnInSkip;
    public Button nxtBtnInSkip;
    public Button skipBtn;
    public TextMeshProUGUI SkipsCount;
    private char[] skipUnLDataArr;

    public Button nxtLvlBtn;
    public GameObject mscInLBtn;
    public GameObject CSMenu;
    
    int selectedLevel;
    
    [SerializeField] bool _testMode = true;


    void Start()
    {
        //InitializeAds();
        /*MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            FindObjectOfType<RewardedAdsButton>()?.LoadRewardedAd();
        });*/
        audioSource = gameObject.AddComponent<AudioSource>();

        selectedLevel=PlayerPrefs.GetInt("selectedLevel",0);

        updLvlTxt(selectedLevel);
        Upd_HSC_Counts();
        updQsnTxt(selectedLevel-1);
        updImg(selectedLevel-1);
        updNxtLvlBtn();
        updHintSkipUnLData();
        if(selectedLevel==1){
            updHint(selectedLevel-1);
            updAns(selectedLevel-1);
        }

        PauseBtn.onClick.AddListener(showPasMenu);
        clsBtnInPause.onClick.AddListener(closePauseMenu);
        hmBtnInPas.onClick.AddListener(gotoHome);
        resmBtnInPas.onClick.AddListener(closePauseMenu);
        extBtnInPas.onClick.AddListener(closes1gs);
        
        verBtn.onClick.AddListener(verAns);

        givCoinsBtn.onClick.AddListener(GivHintOrSkip);

        hintBtn.onClick.AddListener(onHintClicked);
        clsBtnInHint.onClick.AddListener(closeHint);

        skipBtn.onClick.AddListener(onSkipClicked);
        clsBtnInTris.onClick.AddListener(closeTries);
        clsBtnInSkip.onClick.AddListener(closeSkip);
        extBtnInSkip.onClick.AddListener(closes1gs);
        homBtnInSkip.onClick.AddListener(gotoHome);
        nxtBtnInSkip.onClick.AddListener(goToNxtLvl);


        nxtLvlBtn.onClick.AddListener(goToNxtLvl);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseMenu.activeInHierarchy == true)
                closePauseMenu();
            else if(triesMenu.activeInHierarchy == true)
                closeTries();
            else if(hintMenu.activeInHierarchy == true)
                closeHint();
            else if(SkipMenu.activeInHierarchy == true)
                closeSkip();
            else if(CSMenu.activeInHierarchy == true)
                closeCSMenu();
            else
                closes1gs();
        }
    }
    public void OnAnyButtonClicked(){
        if(btnClickSound != null && PlayerPrefs.GetInt("ClickSounds",1)==1)
            audioSource.PlayOneShot(btnClickSound);
    }
    
    void updLvlTxt(int lvl){
        lvlTxt.text ="Level " + lvl;
    }
    void updQsnTxt(int lvl){
        qsnTxt.text= stringsData.questions[lvl];
    }
    void updImg(int lvl){
        qsnImg.sprite=stringsData.images[lvl];
    }
    void updHint(int lvl){
        hintImg.sprite =stringsData.hints[lvl];
    }
    void updAns(int lvl){
        AnsExpImg.sprite = stringsData.AnsExp[lvl];
    }
    void verAns(){
        string playerAnswer = userAnswer.text.ToLower().Trim();
        string[] correctAnswers = stringsData.answers[selectedLevel-1].answers;
        bool isCorrect = false;

        foreach (string correctAnswer in correctAnswers)
        {
            if(playerAnswer.Equals(correctAnswer)){
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            ansbordr.color=Color.green;
            ansRev.text="Correct Answer..!";
            ansRev.color=Color.green;
            
            if(correctAnswerSound != null && PlayerPrefs.GetInt("AnswerResponse",1)==1)
                audioSource.PlayOneShot(correctAnswerSound);

            PlayerPrefs.SetInt("Highest Level Unlocked",selectedLevel + 1);
            mscInLBtn.SetActive(false);
            nxtLvlBtn.interactable=true;
        }
        else
        {
            ansbordr.color=Color.red;
            ansRev.text="Wrong Answer..!";
            ansRev.color=Color.red;

            if(WrongAnswerSound != null && PlayerPrefs.GetInt("AnswerResponse",1)==1)
                audioSource.PlayOneShot(WrongAnswerSound);
        }   
    }
    void updNxtLvlBtn(){
        if(selectedLevel < PlayerPrefs.GetInt("Highest Level Unlocked",1)){
            mscInLBtn.SetActive(false);
            nxtLvlBtn.interactable=true;
        }
    }
    void showPasMenu(){
        pauseMenu.SetActive(true);
    }
    public void closePauseMenu(){
        pauseMenu.SetActive(false);
    }
    public void closes1gs(){
        SceneManager.LoadSceneAsync(1);
    }
    public void closeCSMenu(){
        CSMenu.SetActive(false);
    }
    public void gotoHome(){
        SceneManager.LoadSceneAsync(0);
    }
    void onHintClicked(){   
        if(hintUnLDataArr[selectedLevel - 1] == '1'){
            hintMenu.SetActive(true);
            return;
        }

        if(PlayerPrefs.GetInt("Hints",0) > 0){
            FindObjectOfType<HintManager>()?.DecHints(1);
            HintsCount.text = "" + FindObjectOfType<HintManager>()?.GetHints();
            hintUnLDataArr[selectedLevel - 1] = '1';
            PlayerPrefs.SetString("Hint Unlocked Data",new string(hintUnLDataArr));
            HintsCount.transform.parent.gameObject.SetActive(false);
            hintMenu.SetActive(true);
            return;
        }
        if(FindObjectOfType<CoinManager>()?.GetCoins() < 200){
            givCoinsBtn.interactable = false;
        }
        else{
            givCoinsBtn.interactable = true;
        }
        FindObjectOfType<RewardedAdsInS1>()?.onHintClicked();
        textInTriesMenu.text = "Hints Over..."; 
        textInGivCoinsBtn.text = "200";
        isHintClickOrSkip =true;
        showTriesMenu();  
    }

    public void closeHint(){
        hintMenu.SetActive(false);
    }
    void showTriesMenu(){
        void updGivCoinBtnInr(){
            int reqCoins = isHintClickOrSkip ? 200 : 400 ;
            int avblCoins = PlayerPrefs.GetInt("Coins",0);
            if(reqCoins > avblCoins){
                givCoinsBtn.interactable=false;
            }
        }
        triesMenu.SetActive(true);
    }
    void GivHintOrSkip(){
        if(isHintClickOrSkip){
            FindObjectOfType<CoinManager>()?.DecCoins(200);
            triesMenu.SetActive(false);
            Unlockhint();
            hintMenu.SetActive(true);
        }
        else{
            FindObjectOfType<CoinManager>()?.DecCoins(400);
            triesMenu.SetActive(false);
            UnlockSkip();
            SkipMenu.SetActive(true);
        }
    }
    public void closeTries(){
        triesMenu.SetActive(false);
    }
    public void closeSkip(){
        SkipMenu.SetActive(false);
    }
    void goToNxtLvl(){
        if(selectedLevel==30){
            CSMenu.SetActive(true);
            return;
        }
        PlayerPrefs.SetInt("selectedLevel",selectedLevel+1);
        SceneManager.LoadSceneAsync(2);
    }
    public void onSkipClicked(){
        if(skipUnLDataArr[selectedLevel - 1] == '1'){
            SkipMenu.SetActive(true);
            return;
        }
        if(PlayerPrefs.GetInt("Skips",0) > 0){
            FindObjectOfType<SkipManager>()?.DecSkips(1);
            SkipsCount.text = "" + FindObjectOfType<SkipManager>()?.GetSkips();
            // skipUnLDataArr[selectedLevel - 1] = '1';
            // PlayerPrefs.SetString("Skip Unlocked Data",new string(skipUnLDataArr));
            // SkipsCount.transform.parent.gameObject.SetActive(false);
            // PlayerPrefs.SetInt("Highest Level Unlocked",selectedLevel + 1);
            // mscInLBtn.SetActive(false);
            // nxtLvlBtn.interactable = true ;
            UnlockSkip();
            SkipMenu.SetActive(true);
            return;
        }
        if(FindObjectOfType<CoinManager>()?.GetCoins() < 400){
            givCoinsBtn.interactable = false;
        }
        else{
            givCoinsBtn.interactable = true;
        }
        FindObjectOfType<RewardedAdsInS1>()?.onSkipClicked();
        textInTriesMenu.text = "Skips Over...";  
        textInGivCoinsBtn.text = "400";
        isHintClickOrSkip = false;
        showTriesMenu();
    }

    void updHintSkipUnLData(){
        // hintUnLData = PlayerPrefs.GetString("Hint Unlocked Data",new string('0',30));
        //char ch = PlayerPrefs.GetString("Hint Unlocked Data",new string('0',30)).ToCharArray()[selectedLevel-1];
        hintUnLDataArr = PlayerPrefs.GetString("Hint Unlocked Data",new string('0',30)).ToCharArray();
        HintsCount.transform.parent.gameObject.SetActive(hintUnLDataArr[selectedLevel-1] == '0');
        skipUnLDataArr = PlayerPrefs.GetString("Skip Unlocked Data",new string('0',30)).ToCharArray();
        SkipsCount.transform.parent.gameObject.SetActive(skipUnLDataArr[selectedLevel-1] == '0');

    }
    void Upd_HSC_Counts(){
        HintsCount.text = "" + FindObjectOfType<HintManager>()?.GetHints();
        SkipsCount.text = "" + FindObjectOfType<SkipManager>()?.GetSkips();
        coinsText.text = "" + FindObjectOfType<CoinManager>()?.GetCoins();
    } 
    public void Unlockhint(){
        hintUnLDataArr[selectedLevel - 1] = '1';
        PlayerPrefs.SetString("Hint Unlocked Data",new string(hintUnLDataArr));
        HintsCount.transform.parent.gameObject.SetActive(false);
    }
    public void UnlockSkip(){
        skipUnLDataArr[selectedLevel - 1] = '1';
        PlayerPrefs.SetString("Skip Unlocked Data",new string(skipUnLDataArr));
        SkipsCount.transform.parent.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Highest Level Unlocked",selectedLevel + 1);
        mscInLBtn.SetActive(false);
        nxtLvlBtn.interactable = true ;
    }
     void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // The application is paused. Suspend any operations that should not continue while paused.
            Debug.Log("Application paused.");
            Time.timeScale = 0;
        }
        else
        {
            // The application is resumed. Resume operations as necessary.
            Debug.Log("Application resumed.");
            Time.timeScale = 1;
        }
    }
    
}


