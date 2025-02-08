using UnityEngine;

public class NonMovement : EnemyMovement
{
    public override void Move(Transform enemyTransform, ref Vector3 vel, BoundedVector3 bounds)
    {
        return;
    }
}
