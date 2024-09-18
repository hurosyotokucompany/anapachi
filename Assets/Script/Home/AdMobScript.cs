using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdMobScript : MonoBehaviour
{
  private BannerView bannerView;
  public void Start()
  {
    // Google AdMob Initial
    MobileAds.Initialize(initStatus => { });
    this.RequestBanner();
  }
  private void RequestBanner()
  {
#if UNITY_ANDROID
    string adUnitId="ca-app-pub-6195226537990500/5053751594";
    //string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // テスト用広告ユニットID
#elif UNITY_IPHONE
    string adUnitId="ca-app-pub-6195226537990500/1208478181";
    //string adUnitId = "ca-app-pub-3940256099942544/2934735716"; // テスト用広告ユニットID
#else
    string adUnitId = "unexpected_platform";
#endif

  if (bannerView != null)
        {
            bannerView.Destroy();
            Debug.Log("ad:バナー広告作成前に既にあるBannerViewを破棄する");
        }
        else if (bannerView == null)
        {
            Debug.Log("ad:バナー広告作成前にBannerViewは存在しない");
        }

    // Create a 320x50 banner at the bottom of the screen.
    AdSize customAdSize = new AdSize(320, 100);
    this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
    //this.bannerView = new BannerView(adUnitId,customAdSize, AdPosition.Bottom);
    // Create an empty ad request.
    AdRequest request = new AdRequest();
    // Load the banner with the request.
    bannerView.LoadAd(request);
  }
}