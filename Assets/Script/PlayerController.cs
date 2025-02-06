using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Vector2 shipPadding;

    // movement
    public float speed = 20f;
    public float smoothPos = 3f;
    public float smoothRot = 5f;
    public float pitchAngle = 25f;
    public float rollAngle = 15f;

    // projectile values
    public int projCount;
    public float projSpread,
        projSpeed,
        projSize,
        projPerSecond,
        projLifetime,
        projDamage,
        projPiercing;
    public bool tapFire;

    public GameObject Reticle;
    public GameObject Projectile;

    private Vector3 vel = Vector3.zero;

    void Update()
    {
        // ship movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 targetPos =
            transform.position + new Vector3(horizontalInput, verticalInput, 0f).normalized * speed;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothPos);

        Vector3 bottomLeft = cam.ViewportToWorldPoint(
            new Vector3(0, 0, transform.position.z - cam.transform.position.z)
        );
        Vector3 topRight = cam.ViewportToWorldPoint(
            new Vector3(1, 1, transform.position.z - cam.transform.position.z)
        );

        transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                bottomLeft.x + shipPadding.x,
                topRight.x - shipPadding.x
            ),
            Mathf.Clamp(
                transform.position.y,
                bottomLeft.y + shipPadding.y,
                topRight.y - shipPadding.y
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

        // Projectile Handling
        if (
            (tapFire && Input.GetKeyDown(KeyCode.Mouse0))
            || (!tapFire && Input.GetKey(KeyCode.Mouse0))
        )
        {
            //TODO: check for firerate
            fireProjectile();
        }
    }

    void fireProjectile()
    {
        GameObject currentProj = Instantiate(Projectile, transform.position, Quaternion.identity);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Reticle.transform.position.z)
        );
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = Reticle.transform.position;
        }
        currentProj.transform.LookAt(
            targetPoint
                + new Vector3(
                    Random.Range(-projSpread, projSpread),
                    Random.Range(-projSpread, projSpread),
                    0
                )
        );

        ProjectileController currentProjController =
            currentProj.GetComponent<ProjectileController>();
        currentProjController.Initialize(
            projSpeed,
            projDamage,
            projLifetime,
            projPiercing,
            projSize
        );
    }
}
