using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*enum Direction
{
    None,
    Left,
    Right,
    Up,
    Down
}*/
public class Zombie : MonoBehaviour
{
    [SerializeField] 
    private Directions direction = Directions.None;
    [SerializeField] float speed = 10;
    
    private SpriteRenderer sprite;
    private Animator animator;

    Vector2 movingDirection;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.speed = speed / 10;

        if(direction == Directions.None)
        {
            GenerateRandomDirection();
        }
        SetMovementDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GenerateRandomDirection();
        SetMovementDirection();
    }

    private void GenerateRandomDirection()
    {
        int tempDirection = Random.Range(1, 5);
        if(direction != (Directions)tempDirection)
        {
           direction = (Directions)tempDirection;
        }
        else
        {
            GenerateRandomDirection();
        }
    }

    private void SetMovementDirection()
    {
        print("bhaag");
        if (direction == Directions.Left)
        {
            sprite.flipX = true;
            animator.SetTrigger("horizontal");
            movingDirection = new Vector2(-1, 0);
        }
        else if (direction == Directions.Right)
        {
            sprite.flipX = false;
            animator.SetTrigger("horizontal");
            movingDirection = new Vector2(1, 0);
        }
        else if (direction == Directions.Up)
        {
            animator.SetTrigger("vertical");
            movingDirection = new Vector2(0, 1);
        }
        else if (direction == Directions.Down)
        {
            animator.SetTrigger("vertical");
            movingDirection = new Vector2(0, -1);
        }
        rb.velocity = movingDirection*speed;
    }
}
