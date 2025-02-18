using UnityEngine;

public class NonMovement : EnemyMovement
{
    public override void Move(
        Transform enemyTransform,
        ref Vector3 posVel,
        ref Vector3 rotVel,
        BoundedVector3 bounds
    )
    {
        Rotate(enemyTransform, ref rotVel);
    }
}
