using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpManager : MonoBehaviour
{
    public GameObject[] speedUps;
    public float chargeUpChangeTime = 1f;
    public float chargeUpTotalTime = 5f;
    public float chargeUpEveryXSeconds = 20f;
    public bool isChargeUp = false;

    private void Update()
    {   
        if(isChargeUp)
        {
            isChargeUp = false;
            ChargeUp();
        }
        
    }

    void ChargeUp()
    {

            StartCoroutine(ChargeUpActivation());
    }

    IEnumerator ChargeUpActivation()
    {
        while(true) 
        {
            Color white = Color.white; // new Color(1, 1, 1, 0.05f);
            Color green = Color.green; // new Color(0, 1, 0, 0.05f);
            Color red = Color.red; // new Color(1, 0, 0, 0.05f);
            green.a = 0.05f; white.a = 0.05f; red.a = 0.05f;

            // round clockwise animation
            for (int i = 0; i < speedUps.Length; i++)
            {
                speedUps[i].SetActive(true);
                speedUps[i].GetComponent<SpeedUp>().rn.color = white;
                yield return new WaitForSeconds(chargeUpChangeTime);
                speedUps[i].GetComponent<SpeedUp>().rn.color = Color.clear;
                speedUps[i].SetActive(false);
            }

            int randomGoodIndex = Random.Range(0, speedUps.Length);
            int randomBadIndex = (randomGoodIndex + 3) % 6;
            for (int i = 0; i < randomGoodIndex; i++)
            {
                speedUps[i].SetActive(true);
                speedUps[i].GetComponent<SpeedUp>().rn.color = white;
                yield return new WaitForSeconds(chargeUpChangeTime);
                speedUps[i].GetComponent<SpeedUp>().rn.color = Color.clear;
                speedUps[i].SetActive(false);
            }
            // activate the bad one
            speedUps[randomBadIndex].SetActive(true);
            speedUps[randomBadIndex].GetComponent<PolygonCollider2D>().enabled = true;
            speedUps[randomBadIndex].GetComponent<SpeedUp>().rn.color = red;
            speedUps[randomBadIndex].GetComponent<SpeedUp>().isChargeActive = true;
            speedUps[randomBadIndex].GetComponent<SpeedUp>().beginDamage = true;

            // activate the good one
            speedUps[randomGoodIndex].SetActive(true);
            speedUps[randomGoodIndex].GetComponent<PolygonCollider2D>().enabled = true;
            speedUps[randomGoodIndex].GetComponent<SpeedUp>().rn.color = green;
            speedUps[randomGoodIndex].GetComponent<SpeedUp>().isChargeActive = true;
            speedUps[randomGoodIndex].GetComponent<SpeedUp>().beginHealing = true;


            // reset everything
            yield return new WaitForSeconds(chargeUpTotalTime);
            speedUps[randomGoodIndex].GetComponent<SpeedUp>().rn.color = Color.clear;
            speedUps[randomGoodIndex].GetComponent<SpeedUp>().isChargeActive = false;
            speedUps[randomGoodIndex].GetComponent<PolygonCollider2D>().enabled = false;
            speedUps[randomGoodIndex].GetComponent<SpeedUp>().beginHealing = false;
            speedUps[randomGoodIndex].SetActive(false);

            speedUps[randomBadIndex].GetComponent<SpeedUp>().rn.color = Color.clear;
            speedUps[randomBadIndex].GetComponent<SpeedUp>().isChargeActive = false;
            speedUps[randomBadIndex].GetComponent<PolygonCollider2D>().enabled = false;
            speedUps[randomBadIndex].GetComponent<SpeedUp>().beginDamage = false;
            speedUps[randomBadIndex].SetActive(false);
            yield return new WaitForSeconds(chargeUpEveryXSeconds);
        }
        

        



    }

}
