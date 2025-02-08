using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    public Vector3 targetPos = new Vector3(0, 0, 20);
    public float smoothPos = .1f;

    public void SpawnMove(Transform enemyTransform, ref Vector3 vel)
    {
        enemyTransform.position = Vector3.SmoothDamp(
            enemyTransform.position,
            targetPos,
            ref vel,
            smoothPos
        );
    }

    public abstract void Move(Transform enemyTransform, ref Vector3 vel, BoundedVector3 bounds);
}
