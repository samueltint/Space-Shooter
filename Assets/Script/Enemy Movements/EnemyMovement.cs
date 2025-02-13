using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    public Vector3 targetPos = new Vector3(0, 0, 0);
    public float spawnMovementSmoothing = .1f;
    public float activeMovementSmoothing = .1f;

    public virtual void SpawnMove(Transform enemyTransform, ref Vector3 vel, BoundedVector3 bounds)
    {
        enemyTransform.position = Vector3.SmoothDamp(
            enemyTransform.position,
            targetPos,
            ref vel,
            spawnMovementSmoothing
        );
    }

    public virtual void InitialiseMovement(Transform enemyTransform, BoundedVector3 bounds)
    {
        return;
    }

    public abstract void Move(Transform enemyTransform, ref Vector3 vel, BoundedVector3 bounds);
}
