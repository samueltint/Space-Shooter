// using UnityEngine;

// public class ContinuousRandomMovement : EnemyMovement
// {
//     public float angularAcceleration = 0.3f;
//     public float speed = .1f;
//     public Vector3 dir = Vector3.left;

//     private float angularVelocity;

//     public override void Move(Transform enemyTransform, ref Vector3 vel, BoundedVector3 bounds)
//     {
//         angularVelocity +=
//             Random.Range(-angularAcceleration / 2, angularAcceleration / 2) * Time.deltaTime;

//         dir = Quaternion.Euler(0, 0, angularVelocity * Time.deltaTime) * dir;
//         transform.position += dir * speed;
//     }
// }
