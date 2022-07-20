using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
//reward ad

public class Admob : MonoBehaviour
{
	private RewardedAd rewardedAd;

	private string idApp, idReward;
	Game _game;

	[SerializeField] 
	public Button BtnReward;



	void Start()
	{
		idApp = "ca-app-pub-8031020776871928~6802399658";
		idReward = "ca-app-pub-3940256099942544/5224354917";

		//adReward = RewardBasedVideoAd.Instance;
		_game = GameObject.FindObjectOfType<Game>();
		//MobileAds.Initialize(idApp);
		


		MobileAds.Initialize(initStatus => { });

		this.rewardedAd = new RewardedAd(idReward);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;


		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);


	}

	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdFailedToLoad event received with message: " );
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
		//RequestRewardAd();
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
		_game.Continue();
	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		string type = args.Type;
		GameObject.FindObjectOfType<Game>().timeLeft += 30;
		
	}

	public void RequestRewardAd()
	{
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
	}









}