using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klassa istot
public class Creature : MonoBehaviour {

    protected int MyLives { set; get; }
    protected float speed;

    protected float NextFire { set; get; }

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected bool wait = false;
    // Ładowanie objektów
    protected virtual void Awake()
    {
        NextFire = 0f;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Otrzymanie uszkodzeń
    public virtual void Damage()
    {
        Die();
    }

    // Śmierć
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
