using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interAd;
    private RewardBasedVideoAd videoAd;
    [SerializeField] private string appID = "";
    [SerializeField] private string bannerID = "";
    [SerializeField] private string interstitialAdID = "";
    [SerializeField] private string videoAdID = "";

    private void Awake()
    {
        MobileAds.Initialize(appID);
    }
    private void Start()
    {
        this.RequestBanner();
        this.RequestInterstitial();
        this.RequestVideo();
    }

    //public void OnClickshowBanner()
    //{
    //    this.RequestBanner();

    //}
    //public void OnClickshowAd()
    //{
    //    this.RequestInterstitial();
    //}
    void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Bottom);
        //for testing
        AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        bannerView.LoadAd(request);
    }
    void RequestInterstitial()
    {
        interAd = new InterstitialAd(interstitialAdID);
        //for testing
        AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        interAd.LoadAd(request);
        
    }

    void RequestVideo()
    {
        videoAd = RewardBasedVideoAd.Instance;

        //for testing
        AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        videoAd.LoadAd(request,videoAdID);

    }

    public void ShowBanner()
    {
        bannerView.Show();
    }
    public void ShowInterstitial()
    {
        if (interAd.IsLoaded())
        {
            interAd.Show();
        }
           
    }
    public void ShowVideo()
    {
        if (videoAd.IsLoaded())
        {
           videoAd.Show();
        }

    }


    //HANDLE EVENTS
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        ShowBanner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    void HandleBannerADEvents(bool subscribe)
    {
        if (subscribe) {
            // Called when an ad request has successfully loaded.
            bannerView.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerView.OnAdOpening += HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerView.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            bannerView.OnAdLoaded -= HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerView.OnAdOpening -= HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerView.OnAdClosed -= HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerView.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        }
        
    }

    private void OnEnable()
    {
        HandleBannerADEvents(true);
    }

    private void OnDisable()
    {
        HandleBannerADEvents(false);
    }

}
