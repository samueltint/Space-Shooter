using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 shipBounds;
    public Vector2 reticalBounds;
    public Vector3 targetReticalPos;
    public Vector3 screenCenter;

    public float pitchAngle = 25f;
    public float rollAngle = 15f;

    public float speed = 20f;
    public float smoothPos = 3f;
    public float smoothRot = 5f;

    public GameObject Reticle;

    private Vector3 vel = Vector3.zero;
    private Vector3 reticalVel = Vector3.zero;

    void Update()
    {
        // ship movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 targetPos =
            transform.position + new Vector3(horizontalInput, verticalInput, 0f).normalized * speed;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothPos);

        transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                screenCenter.x - shipBounds.x / 2,
                screenCenter.x + shipBounds.x / 2
            ),
            Mathf.Clamp(
                transform.position.y,
                screenCenter.y - shipBounds.y / 2,
                screenCenter.y + shipBounds.y / 2
            ),
            0f
        );

        // ship rotation

        Quaternion targetRotation = Quaternion.Euler(
            -verticalInput * pitchAngle,
            0,
            -horizontalInput * rollAngle
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            smoothRot * Time.deltaTime
        );
    }
}
