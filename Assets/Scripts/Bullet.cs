using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject Parent { set; get; }
    
    private float speed = 10F;
    public Vector3 MyDirectionVector { set; get; }

    private SpriteRenderer spriteRenderer;

    public Color MyColor { set { spriteRenderer.color = value; } }

    //Inicjalizacja komponentów
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Ustawienie czasu życia i kierunku
    private void Start () {
        Destroy(gameObject, 1.5F);
        spriteRenderer.flipX = MyDirectionVector.x < 0;
    }

    // Aktualizacja pozycji
    private void Update () {
        transform.position = Vector3.MoveTowards(
            transform.position, 
                transform.position + MyDirectionVector, 
                    speed * Time.deltaTime);
	}

    // Napotkanie objektu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Creature creature = collision.GetComponent<Creature>();

        if (creature && creature.gameObject != Parent)
        {
            creature.Damage();
            Destroy(gameObject);
        }
    }
}
