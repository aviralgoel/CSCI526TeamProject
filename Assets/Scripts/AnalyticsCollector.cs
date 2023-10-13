using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class AnalyticsCollector : MonoBehaviour
{
    // [SerializeField] private InputField player1InputField;
    // [SerializeField] private InputField player2InputField;

    private string googleFormURL = "https://docs.google.com/forms/u/0/d/1Bzx0bE5zS9WPcNZXTGq1ZVLMDmkgT8iWEa-L3u_wnEU/formResponse";

    public void SendPlayer1(string player1Click, string player1StartTime)
    {
        StartCoroutine(Post(player1Click, "entry.1884265043", player1StartTime, "entry.1800548838"));
    }

    public void SendPlayer2(string player2Click, string player2StartTime)
    {
        StartCoroutine(Post(player2Click, "entry.176201646", player2StartTime, "entry.645579446"));
    }

    private IEnumerator Post(string playerClickData, string playerClickFormFieldKey, string playerTimeData, string playerTimeFormFieldKey)
    {
        WWWForm form = new WWWForm();
        form.AddField(playerClickFormFieldKey, playerClickData);
        form.AddField(playerTimeFormFieldKey, playerTimeData);

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






