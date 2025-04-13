using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdsInS1 : MonoBehaviour
{
    [SerializeField] private string _androidAdUnitId = "ca-app-pub-3940256099942544/5224354917"; // Replace with your Ad Unit ID
    [SerializeField] private string _iOSAdUnitId = "ca-app-pub-3940256099942544/1712485313"; // Replace with your Ad Unit ID
    public Button showAdButton;
    public GameObject skipMenu;
    public GameObject hintMenu;

    private RewardedAd _rewardedAd;
    private string _adUnitId;
    private bool isShowclickOrHint = true;

    void Start()
    {

        // Set the Ad Unit ID based on the platform
#if UNITY_ANDROID
        _adUnitId = _androidAdUnitId; // Replace with your real Ad Unit ID
#elif UNITY_IOS
        _adUnitId = _iOSAdUnitId; // Replace with your real Ad Unit ID
#else
        _adUnitId = "unused";
#endif

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
           LoadRewardedAd();
        });
        

        // Initially disable the button until the ad is loaded
        showAdButton.interactable = false;
        showAdButton.onClick.AddListener(ShowRewardedAd);
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // If the operation failed, an error is returned.
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load an ad with error: " + error);
                return;
            }

            Debug.Log("Rewarded ad loaded with response: " + ad.GetResponseInfo());

            _rewardedAd = ad;

            // Enable the button for users to click
            showAdButton.interactable = true;
            Debug.Log("Show Ad button is now interactable.");

            RegisterEventHandlers(_rewardedAd);
        });
    }
     public void ShowRewardedAd()
    {
        loadRequiredMenu();


        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // Reward the user here
                Debug.Log($"Rewarded ad rewarded the user. Type: {reward.Type}, amount: {reward.Amount}");
                //Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));

                FindObjectOfType<s1GameController>()?.closeTries();
                rewardTheUser();
            });

            // Disable the button until the next ad is loaded
            showAdButton.interactable = false;
        }
    }
    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log($"Rewarded ad paid {adValue.Value} {adValue.CurrencyCode}.");
        };
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");

            // Load a new ad once the previous one is closed
            LoadRewardedAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError($"Rewarded ad failed to open full screen content with error: {error}");

            // Load a new ad if the previous one fails to open
            LoadRewardedAd();
        };
    }
    void rewardTheUser(){

        if(isShowclickOrHint){
            skipMenu.SetActive(true);
            FindObjectOfType<s1GameController>()?.UnlockSkip();
        }
        else{
            hintMenu.SetActive(true);
            FindObjectOfType<HintManager>()?.AddHints(1);
            FindObjectOfType<s1GameController>().HintsCount.text = "" + FindObjectOfType<HintManager>()?.GetHints();
            FindObjectOfType<s1GameController>()?.Unlockhint();
        }
                
    }
    void loadRequiredMenu(){

        if(isShowclickOrHint){
            skipMenu.SetActive(true);
            skipMenu.SetActive(false); 
        }
        else{
            hintMenu.SetActive(true);
            hintMenu.SetActive(false);
        }
         
    }
    public void onHintClicked(){ isShowclickOrHint = false; }
    public void onSkipClicked(){ isShowclickOrHint = true; }
}
