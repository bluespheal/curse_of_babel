using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mario : MonoBehaviour
{
    public float velocidadx;
    public float velocidadSalto;
    public Sprite idle;
    public Sprite walk;
    public Sprite jump;
    public Sprite ded;

    private Rigidbody2D rigi;
    private SpriteRenderer dibujo;
    private Transform pies;
    private bool dead=false;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        pies = transform.Find("pies");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 mov = rigi.velocity;

        if (!dead)
        { 
            mov.x = h * velocidadx;

            if (h > 0f)
            {
                dibujo.flipX = true;
            }
            else if (h < 0f)
            {
                dibujo.flipX = false;
            }
            if (!Physics2D.OverlapPoint(pies.position))
            {
                dibujo.sprite = jump;
            }
            else
            {
                if (h == 0f)
                {
                    dibujo.sprite = idle;
                }
                else
                {
                    dibujo.sprite = walk;
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (Physics2D.OverlapPoint(pies.position))
                {
                    dibujo.sprite = jump;
                    mov.y = velocidadSalto;
                }
            }
        }
        else {
            dibujo.sprite = ded;
            dibujo.flipY = true;
            mov.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.F)||transform.position.y<=-2f)
        {
            dead = true;
        }
        rigi.velocity = mov;
    }
}
