using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    [SerializeField] Retry retryScript; // Reference to the Retry script

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Disable the button until the ad is ready to show:
        if (_showAdButton != null)
        {
            _showAdButton.interactable = false;
        }
    }

    void OnEnable()
    {
        if (_showAdButton != null && _showAdButton.gameObject.activeSelf) // Ensure the button is active and not null
        {
            LoadAd();  // Load the ad when the button becomes active
        }
    }

    public void LoadAd()
    {
        if (_adUnitId != null)
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);  // Load the ad asynchronously
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId) && _showAdButton != null)
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    public void ShowAd()
    {
        if (_showAdButton != null)
        {
            // Disable the button to prevent multiple clicks:
            _showAdButton.interactable = false;
        }
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");

            // Update the cooldownTime value in the Retry script
            if (retryScript != null)
            {
                retryScript.cooldownTime = 0; // Set the cooldownTime value to 0
                Debug.Log("Cooldown Time set to 0");
            }
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        if (_showAdButton != null)
        {
            // Clean up the button listeners when the object is destroyed:
            _showAdButton.onClick.RemoveAllListeners();
        }
    }
}