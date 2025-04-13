using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine;

public class BannerAds : MonoBehaviour
{
#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif

    private BannerView _bannerView;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            CreateBannerView();
            LoadAd();
            ListenToAdEvents();
        });
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
    }

    public void LoadAd()
    {
    // create an instance of a banner view first.
        if(_bannerView == null)
        {
            CreateBannerView();
        }

    // create our request used to load the ad.
        var adRequest = new AdRequest();

    // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    private void ListenToAdEvents()
    {
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner ad loaded successfully.");
        };

        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError($"Banner ad failed to load with error: {error}");
        };

        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner ad clicked.");
        };

        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner ad impression recorded.");
        };

        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log($"Banner ad earned {adValue.Value} {adValue.CurrencyCode}.");
        };

        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner ad opened full screen content.");
        };

        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner ad closed full screen content.");
        };
    }

     public void DestroyAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }


}
