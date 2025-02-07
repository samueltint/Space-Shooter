using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10;
    public float entrySpeed = 1;
    public Vector3 targetStartingPos = new Vector3(0, 0, 20);
    public bool active = false;
    public Camera cam;

    private EnemyMovement movement;
    private Vector3 vel = Vector3.zero;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        cam = FindFirstObjectByType<Camera>();
        vel = new Vector3(-transform.position.x, -transform.position.y).normalized * entrySpeed;
    }

    void Update()
    {
        if (active)
        {
            movement.Move(transform);
            if (health <= 0)
            {
                handleDeath();
            }
        }
        else
        {
            movement.SpawnMove(transform, targetStartingPos, vel);
            if (transform.position.z - targetStartingPos.z < .05f)
            {
                active = true;
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
}
