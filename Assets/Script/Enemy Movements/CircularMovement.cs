using UnityEngine;

public class CircularMovement : EnemyMovement
{
    public float speed = .5f;
    public float radius = 2f;
    private float angle = 0f;
    private Vector3 center = Vector3.zero;

    public override void InitialiseMovement(Transform enemyTransform, BoundedVector3 bounds)
    {
        targetRot = Quaternion.Euler(0, 180, 0);

        angle = Mathf.Atan2(targetPos.y, targetPos.x);
        center = new Vector3(
            targetPos.x - radius * Mathf.Cos(angle),
            targetPos.y - radius * Mathf.Sin(angle),
            targetPos.z
        );

        center.x = Mathf.Clamp(center.x, bounds.minX + radius, bounds.maxX - radius);
        center.y = Mathf.Clamp(center.y, bounds.minY + radius, bounds.maxY - radius);
    }

    public override void Move(
        Transform enemyTransform,
        ref Vector3 posVel,
        ref Vector3 rotVel,
        BoundedVector3 bounds
    )
    {
        angle += speed * Time.deltaTime;

        targetPos = new Vector3(
            center.x + radius * Mathf.Cos(angle),
            center.y + radius * Mathf.Sin(angle),
            center.z
        );
        enemyTransform.position = Vector3.SmoothDamp(
            enemyTransform.position,
            targetPos,
            ref posVel,
            activeMovementSmoothing
        );

        Rotate(enemyTransform, ref rotVel);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, .5f);
    }
}
