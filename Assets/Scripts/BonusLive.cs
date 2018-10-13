using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLive : Bonus {

    private LivesBar livesBar;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        livesBar = FindObjectOfType<LivesBar>();
    }
    
    protected void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.GetComponent<Character1>())
        {
            livesBar.AddLive();
            player.BonusLive();
            Destroy(gameObject);
        }
    }
}
