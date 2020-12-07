



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    /**
    ** Ускорение игрока
    **/
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

    private enum AnimationState { idle, running, jumping, falling };
    private AnimationState currentAnimationState = AnimationState.idle;

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

    // Обновляем местоположение игрока
    private void updatePlayerPosition()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float jumpInput = Input.GetAxis("Jump");

        if (moveInput < 0)
        { // Влево
            rigidBody.velocity = new Vector2(-xVelocity, rigidBody.velocity.y);
            spriteRenderer.flipX = false;
        }
        else if (moveInput > 0)
        { // Вправо
            rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
            spriteRenderer.flipX = true;
        }
        else if (coll.IsTouchingLayers(ground))
        {
            rigidBody.velocity = Vector2.zero; // Отключение инерции в стороны
        }

        if (jumpInput > 0 && coll.IsTouchingLayers(ground))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity);
        }

        setAnimationState();
    }

    // Выбираем текущую анимацию
    private void setAnimationState()
    {
        // Персонаж касается земли 
        if (coll.IsTouchingLayers(ground))
        {
            // При помощи Mathf.Abs получаем модуль значения ускорения (если бежим влево, оно отрицательное)
            // Если оно больше 0.1 (не стоим на месте), то персонаж бежит
            if (Mathf.Abs(rigidBody.velocity.x) > 0.1f)
            {
                currentAnimationState = AnimationState.running;
            }
            else
            {
                // Если нет - стоим на месте  
                currentAnimationState = AnimationState.idle;
            }
            // Персонаж не касается земли  
        }
        else
        {
            // Ставим текущей анимацией прыжок
            currentAnimationState = AnimationState.jumping;

            if (currentAnimationState == AnimationState.jumping)
            {
                // Если усорение уходит в отрицательное значение, значит персонаж падает вниз
                if (rigidBody.velocity.y < .1f)
                {
                    currentAnimationState = AnimationState.falling;
                }
            }
            else if (currentAnimationState == AnimationState.falling)
            {
                // Если он коснулся земли, то персонаж переходит в состояние спокойствия
                if (coll.IsTouchingLayers(ground))
                {
                    currentAnimationState = AnimationState.idle;
                }
            }
        }

        // Изменияем значение state в аниматоре
        animatorComponent.SetInteger("State", (int)currentAnimationState);
    }
}
