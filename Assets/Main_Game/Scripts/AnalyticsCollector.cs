using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class AnalyticsCollector : MonoBehaviour
{
    public bool shouldDataBeSentToGoogleForms = false;
    private string googleFormURL = "https://docs.google.com/forms/u/0/d/1Bzx0bE5zS9WPcNZXTGq1ZVLMDmkgT8iWEa-L3u_wnEU/formResponse";

    public void SendPlayer1Data(string player1Data)
    {   
        if(shouldDataBeSentToGoogleForms)
        {
            StartCoroutine(Post(player1Data, "entry.1941188781"));
        }

    }
        

    public void SendPlayer2Data(string player2Data)
    {
        if(shouldDataBeSentToGoogleForms)
        {
            StartCoroutine(Post(player2Data, "entry.839068046"));
        }
        
    }

    private IEnumerator Post(string playerData, string playerFormFieldKey)
    {
        WWWForm form = new WWWForm();
        form.AddField(playerFormFieldKey, playerData);

        using (UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data sent successfully to Google Form.");
            }
            else
            {
                Debug.LogError("Error sending data to Google Form: " + www.error);
            }
        }
    }
}


/* DO NOT REMOVE THIS CODE. WE MIGHT NEED THIS IN FUTURE */

// public class PlayerData
// {
//     public string clicks;
//     public string activeTime;
//     public string score;
//     public string killedByBlackHole;
//     public string killedByPlayer;
//     public string goodCollectibleCount;
//     public string badCollectibleCount;

//     public PlayerData(string clicks, string activeTime, string score, string killedByBlackHole, string killedByPlayer, string goodCollectibleCount, string badCollectibleCount)
//     {
//         this.clicks = clicks;
//         this.activeTime = activeTime;
//         this.score = score;
//         this.killedByBlackHole = killedByBlackHole;
//         this.killedByPlayer = killedByPlayer;
//         this.goodCollectibleCount = goodCollectibleCount;
//         this.badCollectibleCount = badCollectibleCount;
//     }
// }

// public class AnalyticsCollector : MonoBehaviour
// {
//     private string googleFormURL = "https://docs.google.com/forms/u/0/d/1Bzx0bE5zS9WPcNZXTGq1ZVLMDmkgT8iWEa-L3u_wnEU/formResponse";

//     private string player1ClicksFormFieldKey = "entry.1884265043";
//     private string player1ActiveTimeFormFieldKey = "entry.1800548838";
//     private string player1ScoreFormFieldKey = "entry.1800548838";
//     private string player1KilledByBlackHoleFormFieldKey = "entry.1800548838";
//     private string player1KilledByPlayerFormFieldKey = "entry.1884265043";
//     private string player1GoodCollectibleCountFormFieldKey = "entry.1800548838";
//     private string player1BadCollectibleCountFormFieldKey = "entry.1884265043";

//     private string player2ClicksFormFieldKey = "entry.176201646";
//     private string player2ActiveTimeFormFieldKey = "entry.1800548838";
//     private string player2ScoreFormFieldKey = "entry.1800548838";
//     private string player2KilledByBlackHoleFormFieldKey = "entry.1800548838";
//     private string player2KilledByPlayerFormFieldKey = "entry.1884265043";
//     private string player2GoodCollectibleCountFormFieldKey = "entry.1800548838";
//     private string player2BadCollectibleCountFormFieldKey = "entry.1884265043";

//     public void SendPlayer(PlayerData playerData, int playerNumber)
//     {
//         string clickKey = (playerNumber == 1) ? player1ClicksFormFieldKey : player2ClicksFormFieldKey;
//         string activeTimeKey = (playerNumber == 1) ? player1ActiveTimeFormFieldKey : player2ActiveTimeFormFieldKey;
//         string scoreKey = (playerNumber == 1) ? player1ScoreFormFieldKey : player2ScoreFormFieldKey;
//         string killedByBlackHoleKey = (playerNumber == 1) ? player1KilledByBlackHoleFormFieldKey : player2KilledByBlackHoleFormFieldKey;
//         string killedByPlayerKey = (playerNumber == 1) ? player1KilledByPlayerFormFieldKey : player2KilledByPlayerFormFieldKey;
//         string goodCollectibleCountKey = (playerNumber == 1) ? player1GoodCollectibleCountFormFieldKey : player2GoodCollectibleCountFormFieldKey;
//         string badCollectibleCountKey = (playerNumber == 1) ? player1BadCollectibleCountFormFieldKey : player2BadCollectibleCountFormFieldKey;

//         StartCoroutine(Post(playerData, clickKey, activeTimeKey,scoreKey, killedByBlackHoleKey, killedByPlayerKey, goodCollectibleCountKey, badCollectibleCountKey, playerNumber));
//     }

//     private IEnumerator Post(PlayerData playerData, string clickKey, string activeTimeKey, string scoreKey, string killedByBlackHoleKey, string killedByPlayerKey, string goodCollectibleCountKey, string badCollectibleCountKey, int playerNumber)
//     {
//         WWWForm form = new WWWForm();
//         // form.AddField(clickKey, playerData.clicks);
//         // form.AddField(activeTimeKey, playerData.activeTime);
//         // form.AddField(scoreKey, playerData.score);
//         // form.AddField(killedByBlackHoleKey, playerData.killedByBlackHole);
//         // form.AddField(killedByPlayerKey, playerData.killedByPlayer);
//         // form.AddField(goodCollectibleCountKey, playerData.goodCollectibleCount);
//         // form.AddField(badCollectibleCountKey, playerData.badCollectibleCount);

//         Debug.Log("Player " + playerNumber + " --- " + playerData);

//         using (UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form))
//         {
//             yield return www.SendWebRequest();

//             if (www.result == UnityWebRequest.Result.Success)
//             {
//                 Debug.Log("Player " + playerNumber + " data sent successfully to Google Form.");
//             }
//             else
//             {
//                 Debug.LogError("Error sending data for Player " + playerNumber + " to Google Form: " + www.error);
//             }
//         }
//     }
// }