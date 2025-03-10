using UnityEngine;

public class RewardedAdButton : MonoBehaviour
{
    [System.Obsolete]
    public void OnClickWatchAd()
    {
        FindObjectOfType<AdMobManager>().ShowRewardedAd();
    }
}
