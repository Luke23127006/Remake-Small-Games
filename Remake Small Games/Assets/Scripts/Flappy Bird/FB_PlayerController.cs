using UnityEngine;

public class FB_PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public delegate void FlapAction();
    public event FlapAction OnFlap;

    [SerializeField] private float speed;
    [SerializeField] private float tiltSmooth = 5f;
    [SerializeField] private float maxTiltUp = 30f;
    [SerializeField] private float maxTiltDown = -90f;
    private float defaultGravity;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        defaultGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f; // Disable gravity at start
        playerRb.freezeRotation = true;
    }

    void Update()
    {
        if (!FB_GameManager.Instance.isGameOver && Input.GetMouseButtonDown(0))
        {
            if (!FB_GameManager.Instance.isGameStarted)
            {
                FB_GameManager.Instance.isGameStarted = true;
                playerRb.gravityScale = defaultGravity; // Enable gravity when the game starts
                playerRb.freezeRotation = false;
            }

            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, speed);

            OnFlap?.Invoke();
        }

        // Smooth rotation based on vertical velocity
        float angle = 0f;
        if (playerRb.linearVelocity.y > -5)
        {
            angle = Mathf.Min(maxTiltUp, playerRb.linearVelocity.y * 2 + 10);
        }
        else
        {
            angle = Mathf.Max(maxTiltDown, playerRb.linearVelocity.y * 3 - 30);
        }

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSmooth * Time.deltaTime);
    }
}
