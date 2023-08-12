using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        playerRB.velocity = movement * moveSpeed;

        if (GameManager.CurrentGameStatus == GameStatus.Dialogue) // Check the game status
        {
            playerRB.velocity = Vector2.zero;
            playerAnimator.Play("Rogue_idle_01");
            return;
        }

        if (movement.magnitude > 0.1f)
        {
            playerAnimator.Play("Rogue_walk_01");

            if (horizontalInput > 0)
            {
                transform.localScale = new Vector2(0.25f, 0.25f);
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector2(-0.25f, 0.25f);
            }
        }
        else
        {
            playerAnimator.Play("Rogue_idle_01");
        }
    }
}
