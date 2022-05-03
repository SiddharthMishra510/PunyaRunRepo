using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private Sprite topViewSprite;
    [SerializeField]
    private Sprite sideViewSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    public ScooterAudioManager scooterAudioManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scooterAudioManager = GetComponent<ScooterAudioManager>();
    }

    private void FixedUpdate()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        // KeyboardControls(ref x, ref y);
        float avgSpeed = (Mathf.Abs(x) + Mathf.Abs(y)) / 2;
        scooterAudioManager.UpdateSound(avgSpeed);

        // TODO print(avg);

        if (Mathf.Abs(x) >= Mathf.Abs(y))
        {
            if (x != 0)
            {
                spriteRenderer.flipX = x > 0;
                spriteRenderer.flipY = false;
                spriteRenderer.sprite = sideViewSprite;
                boxCollider2D.size = new Vector2(1.37f, 0.6f);
            }
        }
        else
        {
            if (y != 0)
            {
                spriteRenderer.flipY = y < 0;
                spriteRenderer.sprite = topViewSprite;
                boxCollider2D.size = new Vector2(0.56f, 1.34f);
            }
        }

        Vector2 movement = new Vector2(x, y);
        rb.MovePosition((new Vector2(transform.position.x, transform.position.y) + movement * (speed * Time.fixedDeltaTime)));
    }

    private static void KeyboardControls(ref float x, ref float y)
    {
        if (x == 0)
        {
            x = Input.GetAxis("Horizontal");
        }
        if (y == 0)
        {
            y = Input.GetAxis("Vertical");
        }
    }
}