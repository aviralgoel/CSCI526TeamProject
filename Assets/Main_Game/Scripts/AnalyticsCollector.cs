using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class AnalyticsCollector : MonoBehaviour
{
    private string googleFormURL = "https://docs.google.com/forms/u/0/d/1Bzx0bE5zS9WPcNZXTGq1ZVLMDmkgT8iWEa-L3u_wnEU/formResponse";

    private string playerSessionFormFieldKey = "entry.167573485";
    private string playerWinnerFormFieldKey = "entry.789064671";
    private string playerActiveTimeFormFieldKey = "entry.381794890";
    private string playerTotalCollectibleFormFieldKey = "entry.1708779778";

    private string player1SuccessRateFormFieldKey = "entry.65508597";
    private string player1KilledByBlackHoleFormFieldKey = "entry.295074975";
    private string player1KilledByPlayerFormFieldKey = "entry.377703588";
    private string player1BadCollectibleCountFormFieldKey = "entry.528433352";
    private string player1GoodCollectibleCountFormFieldKey = "entry.1773993650";
    private string player1FireWallCountFieldKey = "entry.433277146";
    private string player1FreezeCountFormFieldKey = "entry.306319702";
    private string player1HealthFormFieldKey = "entry.510534800";

    private string player2SuccessRateFormFieldKey = "entry.1425639501";
    private string player2KilledByBlackHoleFormFieldKey = "entry.634025400";
    private string player2KilledByPlayerFormFieldKey = "entry.1655115362";
    private string player2BadCollectibleCountFormFieldKey = "entry.1344989641";
    private string player2GoodCollectibleCountFormFieldKey = "entry.1436143141";
    private string player2FireWallCountFieldKey = "entry.535421147";
    private string player2FreezeCountFormFieldKey = "entry.2044412748";
    private string player2HealthFormFieldKey = "entry.1394166838";

    

    public void SendPlayerData(PlayerAnalyticsData playerData, int playerNumber)
    {
        string playerSuccessRate = (playerNumber == 1) ? player1SuccessRateFormFieldKey : player2SuccessRateFormFieldKey;
        string killedByBlackHoleKey = (playerNumber == 1) ? player1KilledByBlackHoleFormFieldKey : player2KilledByBlackHoleFormFieldKey;
        string killedByPlayerKey = (playerNumber == 1) ? player1KilledByPlayerFormFieldKey : player2KilledByPlayerFormFieldKey;
        string badCollectibleCountKey = (playerNumber == 1) ? player1BadCollectibleCountFormFieldKey : player2BadCollectibleCountFormFieldKey;
        string goodCollectibleCountKey = (playerNumber == 1) ? player1GoodCollectibleCountFormFieldKey : player2GoodCollectibleCountFormFieldKey;
        string firewallCountKey = (playerNumber == 1) ? player1FireWallCountFieldKey : player2FireWallCountFieldKey;
        string freezeCountKey = (playerNumber == 1) ? player1FreezeCountFormFieldKey : player2FreezeCountFormFieldKey;
        string healthCountKey = (playerNumber == 1) ? player1HealthFormFieldKey : player2HealthFormFieldKey;



        StartCoroutine(Post(playerData, playerSessionFormFieldKey, playerWinnerFormFieldKey, playerActiveTimeFormFieldKey, playerTotalCollectibleFormFieldKey, playerSuccessRate, killedByBlackHoleKey, killedByPlayerKey, badCollectibleCountKey, goodCollectibleCountKey, firewallCountKey, freezeCountKey, healthCountKey));
    }

    private IEnumerator Post(PlayerAnalyticsData playerData, string playerSessionFormFieldKey, string playerWinnerFormFieldKey, string playerActiveTimeFormFieldKey, string playerTotalCollectibleFormFieldKey, string playerSuccessRate, string killedByBlackHoleKey, string killedByPlayerKey, string badCollectibleCountKey, string goodCollectibleCountKey, string firewallCountKey, string freezeCountKey, string healthCountKey)
    {
        WWWForm form = new WWWForm();
        form.AddField(playerSessionFormFieldKey, playerData.SessionID);
        form.AddField(playerWinnerFormFieldKey, playerData.Winner);
        form.AddField(playerActiveTimeFormFieldKey, playerData.TimeActive.ToString());
        form.AddField(playerTotalCollectibleFormFieldKey, playerData.TotalCollectibles);
        
        form.AddField(playerSuccessRate, playerData.SuccessRate);
        form.AddField(killedByBlackHoleKey, playerData.KilledByBlackHole);
        form.AddField(killedByPlayerKey, playerData.KilledByPlayer);
        form.AddField(badCollectibleCountKey, playerData.BadCollectiblesCollected);
        form.AddField(goodCollectibleCountKey, playerData.GoodCollectiblesCollected);
        form.AddField(firewallCountKey, playerData.FirewallPowerUP);
        form.AddField(freezeCountKey, playerData.FreezePowerUP);
        form.AddField(healthCountKey, playerData.HealthPowerUP);

        using (UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player data sent successfully to Google Form.");
            }
            else
            {
                Debug.LogError("Error sending data for Player to Google Form: " + www.error);
            }
        }
    }
}

// public class AnalyticsCollector : MonoBehaviour
// {
//     public bool shouldDataBeSentToGoogleForms = false;
//     private string googleFormURL = "https://docs.google.com/forms/u/0/d/1Bzx0bE5zS9WPcNZXTGq1ZVLMDmkgT8iWEa-L3u_wnEU/formResponse";

//     public void SendPlayer1Data(string player1Data)
//     {   
//         if(shouldDataBeSentToGoogleForms)
//         {
//             StartCoroutine(Post(player1Data, "entry.1941188781"));
//         }

//     }
        
//     public void GetPlayer1Data(PlayerAnalyticsData player1Data)
//     {   
//         // if(shouldDataBeSentToGoogleForms)
//         // {
//         //     StartCoroutine(Post(player1Data, "entry.1941188781"));
//         // }

//     }

//     public void SendPlayer2Data(string player2Data)
//     {
//         if(shouldDataBeSentToGoogleForms)
//         {
//             StartCoroutine(Post(player2Data, "entry.839068046"));
//         }
        
//     }

//     private IEnumerator Post(string playerData, string playerFormFieldKey)
//     {
//         WWWForm form = new WWWForm();
//         form.AddField(playerFormFieldKey, playerData);

//         using (UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form))
//         {
//             yield return www.SendWebRequest();

//             if (www.result == UnityWebRequest.Result.Success)
//             {
//                 Debug.Log("Data sent successfully to Google Form.");
//             }
//             else
//             {
//                 Debug.LogError("Error sending data to Google Form: " + www.error);
//             }
//         }
//     }
// }