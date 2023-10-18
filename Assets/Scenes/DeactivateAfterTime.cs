using System.Collections;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    public float minDeactivationTime = 3f;
    public float maxDeactivationTime = 8f;

    private void OnEnable()
    {
        StartCoroutine(DelayedDeactivate());
    }

    IEnumerator DelayedDeactivate()
    {
        float randomTime = Random.Range(minDeactivationTime, maxDeactivationTime);
        yield return new WaitForSeconds(randomTime);
        this.gameObject.SetActive(false);
    }
}
