using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    public Vector3 targetPos = new Vector3(0, 0, 0);
    public Quaternion targetRot = Quaternion.identity;
    public float spawnMovementSmoothing = .1f;
    public float activeMovementSmoothing = .1f;
    public float rotationSmoothing = .1f;

    public virtual void SpawnMove(
        Transform enemyTransform,
        ref Vector3 posVel,
        ref Vector3 rotVel,
        BoundedVector3 bounds
    )
    {
        enemyTransform.position = Vector3.SmoothDamp(
            enemyTransform.position,
            targetPos,
            ref posVel,
            spawnMovementSmoothing
        );
        SpawnRotate(enemyTransform, ref rotVel);
    }

    public virtual void SpawnRotate(Transform enemyTransform, ref Vector3 rotVel)
    {
        enemyTransform.LookAt(targetPos);
    }

    public virtual void InitialiseMovement(Transform enemyTransform, BoundedVector3 bounds)
    {
        targetRot = Quaternion.Euler(0, 180, 0);
    }

    public abstract void Move(
        Transform enemyTransform,
        ref Vector3 posVel,
        ref Vector3 rotVel,
        BoundedVector3 bounds
    );

    public virtual void Rotate(Transform enemyTransform, ref Vector3 rotVel)
    {
        enemyTransform.rotation = Quaternion.Lerp(
            enemyTransform.rotation,
            targetRot,
            rotationSmoothing
        );
    }
}
