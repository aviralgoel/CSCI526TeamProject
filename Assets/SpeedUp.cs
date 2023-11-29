using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{   
    public AreaEffector2D effector;
    public SpriteRenderer rn;
    public bool isChargeActive = false;
    public bool beginHealing = false;
    public bool beginDamage = false;
    // Start is called before the first frame update
    void Awake()
    {
        //effector = GetComponent<AreaEffector2D>();
        rn = GetComponent<SpriteRenderer>();
        rn.color = Color.clear;

    }
    private void Start()
    {
        
    }


}
