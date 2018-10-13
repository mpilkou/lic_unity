using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

// Bochater
public class Character1 : Creature {

    private Bullet bullet;
    private GameController game;
    

    private string horizontal = "Horizontal";
    private string vertical = "Vertical";

    private float fireExpectation = 0.5F;

    private bool die = false;

    private States State
    {
        get { return (States)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    // Ładowanie objektów
    protected override void Awake()
    {
        base.Awake();
        
        speed = 3f;
        MyLives = 1;

        bullet = Resources.Load<Bullet>("Bullet");
        game = FindObjectOfType<GameController>();
        
    }

    // Sterowanie bohaterem
    private void FixedUpdate(){
        
        if (die)
            return;
        else
            State = States.sol_stand;

        if (Input.GetButton(horizontal))
            RunHorizontal();

        if (Input.GetButton(vertical))
            RunVertical();

        if (Input.GetButton("Fire1") && Time.time > NextFire){
            NextFire = Time.time + fireExpectation;
            Shoot();
        }
	}
    // Sterowanie w poziome
    private void RunHorizontal()
    {
        Vector3 directionVector = transform.right * Input.GetAxis(horizontal);
        spriteRenderer.flipX = directionVector.x < 0F;
        directionVector = Vector3.MoveTowards(transform.position,   transform.position + directionVector,   speed * Time.deltaTime);
        if (directionVector.x > -10f && directionVector.x < 10f)
        {
            transform.position = directionVector;
            State = States.sol_walk;
        }
    }
    // Sterowanie w pionie
    private void RunVertical()
    {
        Vector3 directionVector = transform.up * Input.GetAxis(vertical);
        directionVector.z = directionVector.y;
        directionVector = Vector3.MoveTowards(transform.position,   directionVector + transform.position,   speed * Time.deltaTime);
        if (directionVector.y > -4.5f && directionVector.y < 0.5f){
            transform.position = directionVector;
            State = States.sol_walk;
        }
    }
    // Napotkanie na object
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Monster creature = collider.gameObject.GetComponent<Monster>();
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (creature){
            Damage();
        } else if (bullet && bullet.Parent != gameObject){
            Damage();
        }
    }
    

    // Strzelanie
    private void Shoot()
    {
        Vector3 positionVector = transform.position;
        bool flipX = spriteRenderer.flipX;
        positionVector.x += flipX ? -0.5F : 0.5F;
        Bullet newBullet = Instantiate(bullet, positionVector, bullet.transform.rotation);
        newBullet.Parent = gameObject;
        newBullet.MyDirectionVector = newBullet.transform.right * (flipX ? -1F : 1F);
    }


    public void BonusLive()
    {
        if (MyLives != 3)
        {
            MyLives++;
        }
    }

    public void BonusShoot()
    {
        fireExpectation = 0.2f;
        Invoke("BonusShootTime", 5);
    }

    // Otrzymanie uszkodzeń
    public override void Damage()
    {
        
        LivesBar.FindObjectOfType<LivesBar>().SendMessage("SubLive");
        MyLives--;
        if (MyLives == 0)
            Die();
    }
    
    // Smierć bochatera
    public override void Die()
    {
        game.SendMessage("Die");
        State = States.sol_die;
        die = true;
    }

    public bool IsDie()
    {
        return die;
    }
    
    private void BonusShootTime()
    {
        fireExpectation = 0.5f;
    }

    // Stany aniemacji
    protected enum States
    {
        sol_stand,
        sol_walk,
        sol_die
    }
}

