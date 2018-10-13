using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    protected Character1 player;

    // Use this for initialization
    protected virtual void Start () {
        player = Character1.FindObjectOfType<Character1>();
    }
    
}
