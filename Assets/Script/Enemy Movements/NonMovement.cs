using UnityEngine;

public class NonMovement : EnemyMovement
{
    public override void Move(Transform enemyTransform)
    {
        return;
    }
}
