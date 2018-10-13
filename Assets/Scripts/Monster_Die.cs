using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Die : MonoBehaviour {

    private GameController gameController;

    // Inicjalizacja (śmierć potwora)
    protected virtual void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        try
        {
            gameController.Add();
        }
        catch (System.Exception) { }


        Destroy(this.gameObject, 0.65f);

        
        int randomInt = Random.Range(0, 30);
        if (randomInt == 0)
        {
            gameController.Bonus_1(transform.position);
        }
        else if (randomInt == 1)
        {
            gameController.Bonus_2(transform.position);
        }
    }

}
