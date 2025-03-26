using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementState : State
{
    private Rigidbody2D _rb;
    private Transform _target;
    private LayerMask obstacleLayer;
    private float avoidanceDistance;

    public override void Enter()
    {
        _rb = human.GetComponent<Rigidbody2D>();
        _target = human.GetTree().transform;
        obstacleLayer = LayerMask.GetMask("Root");
        avoidanceDistance = human.GetAvoidenceDistance();
    }

    public override void Perform()
    {
        MoveTowardsTree();
        AvoidObstacles();
        //animazione corsa
    }

    public void Stop()
    {
        _rb.linearVelocity = new Vector2(0, 0);
    }

    private void MoveTowardsTree()
    {
        Vector2 direction = (_target.position - human.transform.position).normalized;
        _rb.linearVelocity = direction * human.GetMovementSpeed();
        float sqrDistance = (_target.position - human.transform.position).sqrMagnitude;
        if (sqrDistance <= human.GetRange() * human.GetRange())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    private void AvoidObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            human.transform.position,
            _rb.linearVelocity.normalized,
            avoidanceDistance,
            obstacleLayer);

        if (hit.collider != null)
        {
            Vector2 avoidDirection = Vector2.Perpendicular(hit.normal).normalized;
            float dot = Vector2.Dot(avoidDirection, _rb.linearVelocity.normalized);
            avoidDirection *= (dot > 0) ? -1 : 1;
            Vector2 newVelocity = _rb.linearVelocity + (avoidDirection * human.GetMovementSpeed() * 0.5f);
            _rb.linearVelocity = newVelocity.normalized * human.GetMovementSpeed();
        }
    }
}
