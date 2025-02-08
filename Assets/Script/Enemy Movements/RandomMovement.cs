using UnityEngine;

public class RandomMovement : EnemyMovement
{
    public float minDelay = 1;
    public float maxDelay = 1;
    public float movementRange = 2;
    public float smoothingFactor = 3;

    private float timeSinceLastMove = 0;
    private float nextMoveTime = 3;

    public override void Move(Transform enemyTransform, ref Vector3 vel, BoundedVector3 bounds)
    {
        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= nextMoveTime)
        {
            Vector3 randomTarget = new Vector3(
                Random.Range(bounds.minX, bounds.maxX),
                Random.Range(bounds.minY, bounds.maxY),
                Random.Range(bounds.minZ, bounds.maxZ)
            );

            Vector3 direction = randomTarget - enemyTransform.position;
            float distance = direction.magnitude;

            if (distance > movementRange)
            {
                targetPos = enemyTransform.position + direction.normalized * movementRange;
            }
            else
            {
                targetPos = randomTarget;
            }

            nextMoveTime = Random.Range(minDelay, maxDelay);
            timeSinceLastMove = 0f;
        }

        enemyTransform.position = Vector3.SmoothDamp(
            enemyTransform.position,
            targetPos,
            ref vel,
            smoothingFactor
        );
    }
}
