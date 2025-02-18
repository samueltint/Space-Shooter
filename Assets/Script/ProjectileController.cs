using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed,
        damage,
        lifetime,
        size;
    public int piercing;

    private Camera cam;
    private float scalingDist;

    public void Initialise(float speed, float damage, float lifetime, int piercing, float size)
    {
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;
        this.piercing = piercing;
        this.size = size;

        cam = FindFirstObjectByType<Camera>();
        scalingDist = cam.GetComponent<CameraController>().scalingDist;
    }

    void Update()
    {
        transform.position += transform.forward * speed;

        lifetime -= Time.deltaTime;

        if (lifetime <= 0 || piercing < 0)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, cam.transform.position);
        float scaleFactor = size * (1 - Mathf.Exp(-distance / scalingDist));
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
