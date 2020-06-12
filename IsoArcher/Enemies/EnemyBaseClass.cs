using Godot;
using System;

public class EnemyBaseClass : KinematicBody
{
    [Export] private int enemyMoveSpeed = 0;
    [Export] private int enemyHealth = 0;

    private void AttackPlayer(float delta)
    {
        var enemyPosition = GlobalTransform.origin;
        var playerPosition = new Vector3(0, 3, 0); 
        var directionToPlayer = playerPosition - enemyPosition;

        MoveAndCollide(directionToPlayer.Normalized() * enemyMoveSpeed * delta);

    }

    public override void _PhysicsProcess(float delta)
    {
        AttackPlayer(delta);
    }
}
