using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster {

    private Monster_Die die;

    private Vector3 directionVector;

    private float fireExpectation = 0.6F;

    private bool fire = false;
    
    public void Lives(int lives)
    {
        this.MyLives = lives;
    }
    
    // Ładowanie komponentów
    protected override void Awake()
    {
        base.Awake();
        speed = 0.5F;
        MyLives = 1;
        die = Resources.Load<Monster_Die>("Zombie_die");
        
    }

    // Otrzymanie uszkodzeń
    public override void Damage()
    {
        MyLives--;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().material.color = Color.HSVToRGB(0f, 0.4f, 0.9f);
        if (MyLives <= 0)
        {
            Die();
        }
    }

    // Śmierć
    public override void Die()
    {
        die.transform.right = this.gameObject.transform.right * (spriteRenderer.flipX ? -1f : 1f);

        Instantiate(die, transform.position, die.transform.rotation);

        base.Die();
        
    }

    // Chodzenie za bochaterem
    protected override void FixedUpdate()
    {

        if (Time.time > NextFire)
        {
            fire = false;
        }

        if (!fire)
        {
            base.FixedUpdate();

            directionVector = PlayerDirection;

            State = States.walk;

            directionVector = Vector3.MoveTowards(
                transform.position, 
                PlayerDirection,
                speed * Time.deltaTime);

            spriteRenderer.flipX = (directionVector.x - PlayerDirection.x) > 0F;
            transform.position = directionVector;
        }
    }

    // Uszkodzenie bochatera
    protected override void PlayerDamage()
    {
        base.PlayerDamage();

        fire = true;
        NextFire = Time.time + fireExpectation;

        State = States.fight;

    }
    
}