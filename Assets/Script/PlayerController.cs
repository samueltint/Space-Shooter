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
    public int projCount,
        projPiercing;
    public float projSpread,
        projSpeed,
        projSize,
        projPerSecond,
        projLifetime,
        projDamage;
    public bool tapFire;

    public GameObject Reticle;
    public GameObject Projectile;

    private Vector3 vel = Vector3.zero;
    private float timeSinceLastShot;

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

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

        // TODO: soft boundaries
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
        // TODO: dont rotate when pushed against the boundaries
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
            if (timeSinceLastShot >= 1 / projPerSecond)
            {
                fireProjectile();
                timeSinceLastShot = 0;
            }
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
            print("onTarget");
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(130);
        }
        currentProj.transform.LookAt(targetPoint);
        currentProj.transform.Rotate(
            new Vector3(
                Random.Range(-projSpread, projSpread),
                Random.Range(-projSpread, projSpread),
                0
            )
        );
        ProjectileController currentProjController =
            currentProj.GetComponent<ProjectileController>();
        currentProjController.Initialise(
            projSpeed,
            projDamage,
            projLifetime,
            projPiercing,
            projSize
        );
    }
}
