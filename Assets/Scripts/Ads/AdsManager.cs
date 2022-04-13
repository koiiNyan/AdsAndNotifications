using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class AdsManager : MonoBehaviour, IUnityAdsListener
    {
        [SerializeField]
        private string _androidGameId;
        [SerializeField]
        private string _iOSGameId;

        [SerializeField]
        private string _androidInterstitialAdId;
        [SerializeField]
        private string _iOSInterstitialAdId;

        [SerializeField]
        private string _androidRewardedAdId;
        [SerializeField]
        private string _iOSRewardedAdId;

        [SerializeField]
        private string _androidBannerAdId;
        [SerializeField]
        private string _iOSBannerAdId;

        [SerializeField]
        private bool _testMode;
        private string GameId => Application.platform == RuntimePlatform.IPhonePlayer ? _iOSGameId : _androidGameId;

        private string InterstitialAdId => Application.platform == RuntimePlatform.IPhonePlayer ? _iOSInterstitialAdId : _androidInterstitialAdId;
        private string RewardedAdId => Application.platform == RuntimePlatform.IPhonePlayer ? _iOSRewardedAdId : _androidRewardedAdId;
        private string BannerAdId => Application.platform == RuntimePlatform.IPhonePlayer ? _iOSBannerAdId : _androidBannerAdId;

        private void Awake()
        {
            Advertisement.Initialize(GameId, _testMode, true);
            Advertisement.AddListener(this);

            StartCoroutine(WaitForInit());
        }

        private IEnumerator WaitForInit()
        {
            while (!Advertisement.isInitialized)
            {
                yield return null;
            }

            DisableControls();
            StartInterstitialAd();
        }

        public void StartInterstitialAd()
        {
            if (!Advertisement.isInitialized) return;
            Advertisement.Load(InterstitialAdId);
        }

        public void StartRewardedAd()
        {
            if (!Advertisement.isInitialized) return;
            Advertisement.Load(RewardedAdId);
        }

        public void StartBannerAd(BannerPosition position = BannerPosition.BOTTOM_CENTER)
        {
            if (!Advertisement.isInitialized) return;

            Advertisement.Banner.SetPosition(position);
            Advertisement.Banner.Load(BannerAdId, new BannerLoadOptions()
            {
                errorCallback = OnBannerLoadError,
                loadCallback = OnBannerLoad
            });
        }

        private void OnBannerLoad()
        {
            Advertisement.Banner.Show();
        }

        private void OnBannerLoadError(string message)
        {
            Debug.LogError("Ad banner error: " + message);
        }

        public void StopBannerAd()
        {
            Advertisement.Banner.Hide();
        }

        public void OnUnityAdsReady(string placementId)
        {
            Advertisement.Show(placementId);
        }

        public void OnUnityAdsDidError(string message)
        {
            Debug.LogError("Ad error: " + message);
        }

        public void OnUnityAdsDidStart(string placementId)
        {

        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (placementId == RewardedAdId && showResult == ShowResult.Finished)
            {
                // Reward Player
            }

            EnableControls();
        }

        #region Enable/Disable Player Controls While Ads
        private void EnableControls()
        {
          // Enable player controls (movement, etc.)
        }

        private void DisableControls()
        {
            // Disable player controls (movement, etc.)
        }
        #endregion
    }
}

