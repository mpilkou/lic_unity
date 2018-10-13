using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa monstrów
public class Monster : Creature {

    protected Vector3 PlayerDirection { set; get; }

    protected GameObject Player_GObject { set; get; }
    private static Character1 Player_class { set; get; }

    protected bool Elive_player { set; get; }

    protected override void Awake()
    {
        base.Awake();
        Player_GObject = GameObject.Find("Character1");
        Player_class = Player_GObject.GetComponent<Character1>();
        
        
    }

    protected virtual void Update() { }

    // Aktualizacja pozycji bochatera
    protected virtual void FixedUpdate()
    {
        if (Player_class.IsDie())
        {
            speed = 0f;
        }

        PlayerDirection = Player_GObject.transform.position;


    }

    public override void Damage() { }

    // Napotkanie na object
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Creature character = collider.gameObject.GetComponent<Character1>();
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet && bullet.Parent == character)
        {
            Damage();
        }

        if (character)
        {
            PlayerDamage();
        }
    }

    protected virtual void PlayerDamage()
    {
        
    }
    
    // Ustawienie animacji
    protected States State
    {
        get { return (States)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    // Stany animacji
    protected enum States
    {
        walk,
        fight
    }
}
