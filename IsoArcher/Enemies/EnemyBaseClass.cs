using Godot;
using System;

public class EnemyBaseClass : KinematicBody
{
    // Enemy variables
    [Export] private int enemyMoveSpeed = 0;
    [Export] private int enemyHealth = 0;

    // Determines how the enemies attack the player, by moving towards them
    private void AttackPlayer(float delta)
    {
        var enemyPosition = GlobalTransform.origin;
        var playerPosition = new Vector3(0, 3, 0); 
        var directionToPlayer = playerPosition - enemyPosition;

        MoveAndCollide(directionToPlayer.Normalized() * enemyMoveSpeed * delta);

    }
    
    // Update method for enemy attack
    public override void _PhysicsProcess(float delta)
    {
        AttackPlayer(delta);
    }

    // Logic for when an enemy is hit by an arrow
    void _on_Area_area_entered(Area area)
    {
        enemyHealth -= CurrentBowStatsManager.currentBowDamage;
        if (enemyHealth <= 0)
        {
            QueueFree();
        }
    }
    
}
