using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    float smoothPos = .1f;

    public void SpawnMove(Transform enemyTransform, Vector3 targetPos, Vector3 vel)
    {
        enemyTransform.position = Vector3.SmoothDamp(
            enemyTransform.position,
            targetPos,
            ref vel,
            smoothPos
        );
    }

    public abstract void Move(Transform enemyTransform);
}
