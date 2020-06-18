using Godot;
using System;

public class EnemyBaseClass : KinematicBody
{
    // Enemy variables
    [Export] private int enemyMoveSpeed = 0;
    [Export] private int enemyHealth = 0;
    [Export] private int howMuchGoldToDrop = 0;
    [Export] private String goblinName = "";

    
    // Sets animation to loop
    public override void _Ready()
    {
        var enemyAnimation = GetNode<AnimationPlayer>(goblinName + "/AnimationPlayer").GetAnimation("enemyGoblinWalk");
        enemyAnimation.Loop = true;
        GetNode<AnimationPlayer>(goblinName + "/AnimationPlayer").Play("enemyGoblinWalk");
        LookAt(new Vector3(0, 2.0f, 0), Vector3.Up);
    }

    // Determines how the enemies attack the player, by moving towards them
    private void AttackPlayer(float delta)
    {
        var enemyPosition = GlobalTransform.origin;
        var playerPosition = new Vector3(0, 2.0f, 0); 
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
        if (area.IsInGroup("Arrows"))
        {
            // MainCamera cameraShake = GetNode<MainCamera>("/root/MainCamera");
            // cameraShake.Shake(5f, 40f, 5f);
            
            enemyHealth -= CurrentBowStatsManager.currentBowDamage;
            if (enemyHealth <= 0)
            {
                GlobalGoldManager.globalGold += howMuchGoldToDrop;
                GD.Print((GlobalGoldManager.globalGold));
                QueueFree();
            }
        }
        
    }
    
}
