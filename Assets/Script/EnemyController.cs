using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10;

    private bool active = false;
    private Camera cam;
    private GameObject player;
    private EnemyMovement movement;
    private Vector3 vel = Vector3.zero;
    private BoundedVector3 movementBounds;

    [Header("Projectiles")]
    public int projCount;
    public int projPiercing;
    public float projSpread,
        projSpeed,
        projSize,
        projPerSecond,
        projLifetime,
        projDamage;
    public GameObject Projectile;

    void Awake()
    {
        InvokeRepeating(nameof(fireProjectile), 0f, 1f / projPerSecond);
    }

    public void Initialise(float targetZ, float entrySpeed)
    {
        cam = FindAnyObjectByType<Camera>();
        player = GameObject.FindWithTag("Player");
        movement = GetComponent<EnemyMovement>();

        vel = new Vector3(-transform.position.x, -transform.position.y).normalized * entrySpeed;
        movement.targetPos = cam.ViewportToWorldPoint(
            new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), targetZ)
        );

        Vector3 minBounds = cam.ViewportToWorldPoint(new Vector3(.1f, .1f, targetZ));
        Vector3 maxBounds = cam.ViewportToWorldPoint(new Vector3(.9f, .9f, targetZ));

        movementBounds.setValues(
            minBounds.x,
            maxBounds.x,
            minBounds.y,
            maxBounds.y,
            minBounds.z,
            maxBounds.z
        );
    }

    void Update()
    {
        if (active)
        {
            movement.Move(transform, ref vel, movementBounds);
            if (health <= 0)
            {
                handleDeath();
            }
        }
        else
        {
            movement.SpawnMove(transform, ref vel, movementBounds);
            if (transform.position.z - movement.targetPos.z < 1f)
            {
                active = true;
                vel = Vector3.zero;
                movement.InitialiseMovement(transform, movementBounds);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Projectile")
        {
            ProjectileController proj = other.GetComponent<ProjectileController>();
            handleDamage(proj.damage);
            proj.piercing--;
        }
    }

    public void handleDamage(float damage)
    {
        //TODO: damage anim
        health -= damage;
    }

    public void handleDeath()
    {
        Destroy(gameObject);
    }

    void fireProjectile()
    {
        if (active)
        {
            GameObject currentProj = Instantiate(
                Projectile,
                transform.position,
                Quaternion.identity
            );
            currentProj.tag = "Enemy_Projectile";
            currentProj.transform.LookAt(player.transform.position);
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
}

public struct BoundedVector3
{
    public float minX,
        maxX,
        minY,
        maxY,
        minZ,
        maxZ;

    public void setValues(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
        this.minZ = minZ;
        this.maxZ = maxZ;
    }
}
