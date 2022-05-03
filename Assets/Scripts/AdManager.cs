using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using TMPro;

public class AdManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private bool isRewardEarned = false;
    private bool isAdShowing = false;

    private float currentTime = 0;
    private const int TIMEOUT_TIME = 5;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private TextMeshProUGUI clockText;
    [SerializeField]
    private TextMeshProUGUI messageText;


    private void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }

    public void Start()
    {
        CreateAndLoadRewardAd();
    }

    private void CreateAndLoadRewardAd()
    {
        print("CreateAndLoadRewardAd");

        string adUnitId = "ca-app-pub-4240081825903903/5968432735"; // real reward ad id

        /*#if UNITY_ANDROID
                adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
                    adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
                    adUnitId = "unexpected_platform";
        #endif*/

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        LoadRewardedVideoAd();
    }

    /*    private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                LoadRewardedVideoAd();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                ShowRewardedVideoAd();
            }
        }*/

    private void ShowRewardedVideoAd()
    {
        print("ShowRewardedVideoAd");
        if(rewardedAd.IsLoaded())
        {
            isAdShowing = true;
            Debug.Log("Showing rewarded ad");
            rewardedAd.Show();
            CreateAndLoadRewardAd();
        }
        else
        {
            print("Ad wasn't loaded");
            LoadRewardedVideoAd();
            StartCoroutine(AdLoadingAid());
        }
    }

    private IEnumerator AdLoadingAid()
    {
        canvas.SetActive(true);

        currentTime = 0;
        for(int time = TIMEOUT_TIME; time >= 0; time--)
        {
            clockText.text = time.ToString();
            yield return new WaitForSeconds(1);
            currentTime += 1;
        }

        print("Timeout...");

        if(!isAdShowing)
        {
            print("but ad already loaded");
            AdCouldntBeLoadedThing();
        }

        yield return null;
    }

    private void AdCouldntBeLoadedThing()
    {
        print("AdCouldntBeLoadedThing");
        // canvas.SetActive(true);
        messageText.text = "Ad coudn't be loaded";
        Invoke("ShowGameOverDelayed", 2);
    }

    private void ShowGameOverDelayed()
    {
        print("ShowGameOverDelayed");
        canvas.SetActive(false);
        GameManager.Instance.GameOver();
    }

    private void LoadRewardedVideoAd()
    {
        print("Loading... RewardedVideoAd");
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        print("HandleRewardedAdLoaded event received");

        if(GameManager.Instance.WasWatchAdButtonClicked())
        {
            if(HasTimeRunOutForShowingAd() == false)
            {
                ShowRewardedVideoAd();
            }

            GameManager.Instance.ResetWatchAdButtonClickedBool();
        }
    }

    private bool HasTimeRunOutForShowingAd()
    {
        return currentTime < TIMEOUT_TIME;
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        print("HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void ShowRewardAd()
    {
        ShowRewardedVideoAd();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        print("HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received");

        isAdShowing = false;

        print("isRewardEarned: " + isRewardEarned);
        if(isRewardEarned)
        {
            isRewardEarned = false;
            GameManager.Instance.ResumeAsAReward();
        }
        else
        {
            print("Calling Game over...");
            GameManager.Instance.GameOver();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        isRewardEarned = true;
        string type = args.Type;
        double amount = args.Amount;
        print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
    }
}