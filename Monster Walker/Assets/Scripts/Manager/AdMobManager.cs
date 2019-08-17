using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdMobManager : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interAd;
    private RewardBasedVideoAd videoAd;
    [SerializeField] private string appID = "ca-app-pub-4306238078188379~5897581980";

   

    //real ads
    //private string bannerID = "ca-app-pub-4306238078188379/2373269949";
    //private string interstitialAdID = "ca-app-pub-4306238078188379/1463512955";
    //private string videoAdID = "ca-app-pub-4306238078188379/4806139397";

    //test ads
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";
    private string interstitialAdID = "ca-app-pub-3940256099942544/1033173712";
    private string videoAdID = "ca-app-pub-3940256099942544/5224354917";

    private void Awake()
    {
        MobileAds.Initialize(appID);
    }
    private void Start()
    {


        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;





        if(sceneName == "OpeningMenu" || sceneName =="StepCounter" || sceneName=="Battle") { this.RequestBanner(); }
        



        this.RequestInterstitial();
        this.RequestVideo();

        
    }
    

    
    public void RequestBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Top);

        //For Register AD events
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

        //AdRequest request = new AdRequest.Builder().Build();
        //for testing
        AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        bannerView.LoadAd(request);
    }
    public void RequestInterstitial()
    {
        if(interAd != null)
        {
            interAd.Destroy();
        }
        interAd = new InterstitialAd(interstitialAdID);

        // Called when an ad request has successfully loaded.
        interAd.OnAdLoaded += HandleInterstitialLoaded;
        // Called when an ad request failed to load.
        interAd.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        // Called when an ad is clicked.
        interAd.OnAdOpening += HandleInterstitialOpened;
        // Called when the user returned from the app after an ad click.
        interAd.OnAdClosed += HandleInterstitialClosed;
        // Called when the ad click caused the user to leave the application.
        interAd.OnAdLeavingApplication += HandleInterstitialLeftApplication;

        //AdRequest request = new AdRequest.Builder().Build();
        //for testing
        AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        interAd.LoadAd(request);
        
    }

    public void DestroyBanner()
    {
        bannerView.Destroy();

    }

    public void RequestVideo()
    {
        videoAd = RewardBasedVideoAd.Instance;

        //AdRequest request = new AdRequest.Builder().Build();
        //for testing
        AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        videoAd.LoadAd(request,videoAdID);

    }

    //public void ShowBanner()
    //{
    //    bannerView.Show();
    //}
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
    //BANNER
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

        //ShowBanner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //RequestBanner();
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

    

    //INTERSTITIAL
    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }


    

    ////VIDEO
    //public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    //}

    //public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    MonoBehaviour.print(
    //        "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    //}

    //public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    //}

    //public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    //}

    //public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    //}

    //public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    //{
    //    string type = args.Type;
    //    double amount = args.Amount;
    //    MonoBehaviour.print(
    //        "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    //}

    //public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    //}




    //void HandleRewardBasedVideoADEvents(bool subscribe)
    //{
    //    if (subscribe)
    //    {
    //        // Called when an ad request has successfully loaded.
    //        videoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
    //        // Called when an ad request failed to load.
    //        videoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
    //        // Called when an ad is clicked.
    //        videoAd.OnAdOpening += HandleRewardBasedVideoOpened;
    //        //
    //        videoAd.OnAdStarted += HandleRewardBasedVideoStarted;
    //        // Called when the user returned from the app after an ad click.
    //        videoAd.OnAdClosed += HandleRewardBasedVideoClosed;
    //        //
    //        videoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
    //        // Called when the ad click caused the user to leave the application.
    //        videoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    //    }
    //    else
    //    {
    //        // Called when an ad request has successfully loaded.
    //        videoAd.OnAdLoaded -= HandleRewardBasedVideoLoaded;
    //        // Called when an ad request failed to load.
    //        videoAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
    //        // Called when an ad is clicked.
    //        videoAd.OnAdOpening -= HandleRewardBasedVideoOpened;
    //        //
    //        videoAd.OnAdStarted -= HandleRewardBasedVideoStarted;
    //        // Called when the user returned from the app after an ad click.
    //        videoAd.OnAdClosed -= HandleRewardBasedVideoClosed;
    //        //
    //        videoAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
    //        // Called when the ad click caused the user to leave the application.
    //        videoAd.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
    //    }

    //}




}
