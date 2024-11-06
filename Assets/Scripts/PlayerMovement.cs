using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public float acceleration;
    float h;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;
    Vector2 targetVelocity; //velocidad que queremos que alcance el player
    Vector2 dampVelocity; // para hacer un movimiento suave de un valor a otro

    void Start()
    {
        //hace la referencia al componente animator
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //transform,Translate(Vector2,right * speed * h * Time.deltaTime); // time delta tiempo entre update
        InputPlayer();
        Animating();
        Movement();
        Flip();
    }

    private void FixedUpdate() //mas estable el update // contrains bloquear el rigidbody, bloquear el Z
    {
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity,targetVelocity, ref dampVelocity,acceleration);
    }

    public void Movement()
    {
        targetVelocity = new Vector2(h * speed, rb2d.velocity.y);
    }
    public void InputPlayer()
    {
        h = Input.GetAxis("Horizontal"); // D -> 1 A->1 0 si no se pulsa tecla
    }

    public void Animating() // si no esta corriendo esta quieto, ejecuta la animacion de idle
    {
        if (h!=0)
            anim.SetBool("IsRunning", true);
        else
            anim.SetBool("IsRunning", false);

    }

    void Flip() //para rotar el diseño del personaje
    {
        if (h > 0)
            spriteRenderer.flipX = false;
        else if (h < 0) 
            spriteRenderer.flipX = true;

    }
}

