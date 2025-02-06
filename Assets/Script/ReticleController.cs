using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public Camera cam;
    public GameObject Player;

    public float reticleDistance = 13f;
    public float secondReticleOffset = 50f;
    public float smoothPos = 3f;

    private Transform nearReticle,
        farReticle;

    private Vector3 nearTargetPos;
    private Vector3 farTargetPos;
    private Vector3 nearVel = Vector3.zero;
    private Vector3 farVel = Vector3.zero;

    private void Awake()
    {
        Cursor.visible = false;

        nearReticle = gameObject.transform.GetChild(0);
        farReticle = gameObject.transform.GetChild(1);
        getTargetPositions();

        nearReticle.transform.position = nearTargetPos;
        farReticle.transform.position = farTargetPos;
    }

    void Update()
    {
        // orient reticles to the camera, like a billboard sprite
        nearReticle.LookAt(cam.transform, Vector3.left);
        farReticle.LookAt(cam.transform, Vector3.left);

        getTargetPositions();

        nearReticle.position = Vector3.SmoothDamp(
            nearReticle.position,
            nearTargetPos,
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

    void getTargetPositions()
    {
        Vector2 mousePos = Input.mousePosition;

        Ray cameraRay = cam.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, reticleDistance));
        nearTargetPos = cameraRay.GetPoint(reticleDistance);
        Ray playerRay = new Ray(
            Player.transform.position,
            nearTargetPos - Player.transform.position
        );
        farTargetPos = playerRay.GetPoint(
            (nearTargetPos - Player.transform.position).magnitude + secondReticleOffset
        );
    }
}
