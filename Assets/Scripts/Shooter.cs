using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Monster {

    private Monster_Die die;
    private Ball ball;
    private Vector3 directionVector;

    protected float NextShoot { set; get; }
    
    private float fireExpectation = 0.6F;
    private float shootExpectation = 5F;

    private bool fire = false;
    private bool shoot = false;

    private float time;

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
        die = Resources.Load<Monster_Die>("Shooter_die");
        ball = Resources.Load<Ball>("Ball");

        shoot = true;
        NextFire = Time.time + fireExpectation;
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
        time = Time.time;
        if (time > NextFire){
            fire = false;
            if (time > NextShoot)
                shoot = false;
        }

        if (!fire && shoot)
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
        else if (!shoot){
            Shoot();
        }
    }

    private void Shoot()
    {
        shoot = true;
        Vector3 positionVector = transform.position;
        bool flipX = spriteRenderer.flipX;
        positionVector.x += flipX ? -0.5F : 0.5F;
        Ball newBall = Instantiate(ball, positionVector, ball.transform.rotation);
        newBall.DirectionVector = newBall.transform.right * (flipX ? -1F : 1F);
        NextShoot = Time.time + shootExpectation;
        State = States.fight;
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
