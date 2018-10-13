using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private GameObject Player { set; get; }

    private float speed = 3F;
    public Vector3 DirectionVector { set; get; }

    private SpriteRenderer spriteRenderer;

    public Color MyColor { set { spriteRenderer.color = value; } }

    //Inicjalizacja komponentów
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Player = GameObject.Find("Character1");
    }

    // Ustawienie czasu życia i kierunku
    private void Start()
    {
        Destroy(gameObject, 5F);
        spriteRenderer.flipX = DirectionVector.x < 0;
    }

    // Aktualizacja pozycji
    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + DirectionVector,
            speed * Time.deltaTime);
    }

    // Napotkanie objektu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Creature creature = collision.GetComponent<Creature>();
        
        if (creature.gameObject == Player)
        {
            creature.Damage();
            Destroy(gameObject);
        }
    }
}
