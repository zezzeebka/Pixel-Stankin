using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
  
  [Header("Player velocity")]
    // Ось Ox
    public int xVelocity = 5;
    // Ось Oy
    public int yVelocity = 8;

  [SerializeField] private LayerMask ground;

    private Rigidbody2D rigidBody;
    private Collider2D coll;
    private SpriteRenderer spriteRenderer;
    private Animator animatorComponent;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animatorComponent = gameObject.GetComponent<Animator>();
    }
    private void Update() 
    {
      updatePlayerPosition();
    }
    private void updatePlayerPosition()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float jumpInput = Input.GetAxis("Jump");

        if (moveInput < 0)
        { // Влево
            rigidBody.velocity = new Vector2(-xVelocity, rigidBody.velocity.y);
            animatorComponent.SetInteger("State", 1); // Бег
            spriteRenderer.flipX =false;
        }
        else if (moveInput > 0)
        { // Вправо
            rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
            animatorComponent.SetInteger("State", 1); // Бег
            spriteRenderer.flipX = true;
        }
        else if (coll.IsTouchingLayers(ground))
        {
            rigidBody.velocity = Vector2.zero; // Отключение инерции в стороны
            animatorComponent.SetInteger("State", 0); // Стоим
        }

        if (jumpInput > 0 && coll.IsTouchingLayers(ground))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity);
        }
    }
}
