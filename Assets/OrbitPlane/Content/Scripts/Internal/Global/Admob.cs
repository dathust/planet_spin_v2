using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class Admob : MonoBehaviour {

	 // Start is called before the first frame update
    private string APP_ID = "ca-app-pub-4625543203821613~8533202159";
    // private BannerView bannerAD = null;
    // private BannerView[] arrBanner;
    // private InterstitialAd interstitialAd;
    // private RewardBasedVideoAd rewardBasedVideoAd;
    public static Admob instance = null;

    // AdRequest adRequestBanner = null;
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        MakeInstance();
        MobileAds.Initialize(APP_ID);
        // this.RequestBanner();
        // RequestInterstitial();
    }

    public void RequestBanner()
    {
        GameStatus.bannerAD = null;
        // deviceid = 4A259F2FEF8F58DE69DECC0A1BC5F1CD
        // deviceid = 2077ef9a63d2b398840261c8221a0c9b
        string bannerId = "ca-app-pub-3940256099942544/6300978111"; //test 
        GameStatus.bannerAD = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);
        GameStatus.bannerAD.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // bannerAD.OnAdLoaded += HandleOnAdLoaded;
        // AdRequest.Builder.addTestDevice("4A259F2FEF8F58DE69DECC0A1BC5F1CD")
        // AdRequest adRequest = new AdRequest.Builder().AddTestDevice("4A259F2FEF8F58DE69DECC0A1BC5F1CD").Build();
        // adRequestBanner = new AdRequest.Builder().AddTestDevice("4A259F2FEF8F58DE69DECC0A1BC5F1CD").Build();
        AdRequest adRequest = new AdRequest.Builder().Build();
        GameStatus.bannerAD.LoadAd(adRequest);
        // ShowBanner();
    }

    public void RequestInterstitial()
    {
        string interstitialId = "ca-app-pub-3940256099942544/1033173712"; // test 
        // string interstitialId = "ca-app-pub-4418447600360926/9103693317";
        // string xiaoMi = "4A259F2FEF8F58DE69DECC0A1BC5F1CD";
        // string samSung = "A02A7F0D688B96A0656043674E4B2387";

        GameStatus.Interstitial = new InterstitialAd(interstitialId);
        GameStatus.Interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // AdRequest adRequest = new AdRequest.Builder().AddTestDevice("4A259F2FEF8F58DE69DECC0A1BC5F1CD").Build();
        AdRequest adRequest = new AdRequest.Builder().Build();
        GameStatus.Interstitial.LoadAd(adRequest);
    }

    public void HideBanner() {
        if (GameStatus.bannerAD != null) {
        GameStatus.bannerAD.Hide();
        }
    }

    public void ShowBanner() {
        if (GameStatus.bannerAD != null) {
            GameStatus.bannerAD.Show();
        }
        // bannerAD.Show();
    }

    public void ShowInterstitial()
    {
        if (GameStatus.Interstitial.IsLoaded())
        {
            GameStatus.Interstitial.Show();
        }
        GameStatus.Interstitial = null;
    }
    ///
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        // this.RequestBanner();
        // this.RequestInterstitial();
    }
}
