using UnityEngine;
using Steamworks; 

public class RuneCollector : MonoBehaviour
{
    public string achievementID = "Rune1Found"; // change this for each rune
    private bool hasBeenCollected = false; // prevent multiple triggers

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTERED - Object: " + other.name);

        // check if player entered the trigger
        if (other.CompareTag("Player") && !hasBeenCollected)
        {
            hasBeenCollected = true;
            CollectRune();
        }
    }

    void CollectRune()
    {

        // unlock the achievement (you'll add platform-specifc code here later)
        Debug.Log("Rune Collected! Achievement: " + achievementID);

        // optional: play sound, particle effect, etc.

        #if UNITY_STANDALONE // Steam/PC
            if (SteamManager.Initialized)
            {
                SteamUserStats.SetAchievement(achievementID);
            }
        #elif UNITY_XBOXONE || UNITY_GAMECORE // Xbox
            //   XboxAchievements.Unlock(achievementID);
        #elif UNITY_PS4 || UNITY_PS5 // PlayStation
            //    PSNManager.UnlockTrophy(achievementID);
        #endif

        // optional: disable the rune visually
        // gameObject.SetActive(false);
    }
}