using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShoot : Bonus
{
    
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
    
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.GetComponent<Character1>())
        {
            player.BonusShoot();
            Destroy(gameObject);
        }
    }
}
