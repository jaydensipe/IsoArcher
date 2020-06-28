using Godot;
using System;

public class EnemyBaseClass : KinematicBody
{
    // Enemy variables
    [Export] private int enemyMoveSpeed = 0;
    [Export] private int enemyHealth = 0;
    [Export] private int howMuchGoldToDrop = 0;
    [Export] private String goblinName = "";
    
    // Variable for how many enemies remain
    public static int globalEnemiesRemaining = 0;
    
    
    public override void _Ready()
    {
        // Sets animation to loop
        var enemyAnimation = GetNode<AnimationPlayer>(goblinName + "/AnimationPlayer").GetAnimation("enemyGoblinWalk");
        enemyAnimation.Loop = true;
        GetNode<AnimationPlayer>(goblinName + "/AnimationPlayer").Play("enemyGoblinWalk");
        
        // Looks at player position
        LookAt(new Vector3(0, 2.0f, 0), Vector3.Up);
        
        // Initializes enemy health
        enemyHealth += 50 * Mathf.CeilToInt(GameController.globalCurrentWave / 5);
    }
    
    
    private void AttackPlayer(float delta)
    {
        // Determines how the enemies attack the player, by moving towards them
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

    // Logic for when an enemy is hit and killed by an arrow
    void _on_Area_area_entered(Area area)
    {
        if (area.IsInGroup("Arrows"))
        {
            // MainCamera cameraShake = GetNode<MainCamera>("/root/MainCamera");
            // cameraShake.Shake(5f, 40f, 5f);
            var bowParticles = GetNode<Particles>("BloodParticles");
            bowParticles.Emitting = true;
            enemyHealth -= GlobalCurrentBowStatsManager.currentBowDamage;
            
            // Enemy knockback
            Translate(Vector3.Back * 3.0f);
            
            if (enemyHealth <= 0)
            {
                GameController.globalGold += howMuchGoldToDrop;
                globalEnemiesRemaining -= 1;
                QueueFree();
            }
        } else if (area.IsInGroup("Player"))
        {
            QueueFree();
        }
        
    }
    
}