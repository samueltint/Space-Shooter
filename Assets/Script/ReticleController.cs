using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public Camera cam;
    public GameObject Player;

    public float nearReticleZ = 13f;
    public float farReticleZ = 50f;
    public float smoothPos = 3f;

    public Vector3 targetPos;

    private Transform nearReticle,
        farReticle;

    private Vector3 nearVel = Vector3.zero;
    private Vector3 farVel = Vector3.zero;

    public void Start()
    {
        nearReticle = gameObject.transform.GetChild(0);
        farReticle = gameObject.transform.GetChild(1);
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        targetPos = cam.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, nearReticleZ - cam.transform.position.z)
        );

        Vector3 PlayerReticleDif = targetPos - Player.transform.position;

        Vector3 farTargetPos =
            PlayerReticleDif * (farReticleZ / PlayerReticleDif.z) + Player.transform.position;

        nearReticle.position = Vector3.SmoothDamp(
            nearReticle.position,
            targetPos,
            ref nearVel,
            smoothPos
        );

        farReticle.position = Vector3.SmoothDamp(
            farReticle.position,
            farTargetPos,
            ref farVel,
            smoothPos
        );
    }
}
