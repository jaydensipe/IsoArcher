using Godot;
using System;

public class BowBaseClass : Spatial
{
    // Bow variables
    [Export] private int bowDamage = 0;
    [Export] private string bowName = "";
    [Export] private string bowAnimationName = "";
    [Export] private string bowModelName = "";
    private PackedScene arrow = new PackedScene();


    // Code for firing current weapon
    private void FireWeapon()
    {
        if (Input.IsActionJustPressed("Fire"))
        {
            
            // Plays bow firing animation
            var animationPlayerWeapon = GetNode<AnimationPlayer>(bowModelName + "/AnimationPlayer");
            animationPlayerWeapon.Play(bowAnimationName);
            
            // Instances arrow
            arrow = (PackedScene) ResourceLoader.Load("res://IsoArcher/Arrows/WoodArrow/WoodArrow.tscn");
            var arrowInstance = (Area)arrow.Instance();
            arrowInstance.Translate(GetNode<Position3D>("ArrowPosition").Translation);
            AddChild(arrowInstance);
            arrowInstance.SetAsToplevel(true);


        }
    }
    
    // Update method for shooting
    public override void _PhysicsProcess(float delta)
    {
        FireWeapon();
    }

    
    // Fires ability of current Bow
    private void FireAbility()
    {
        
    }
    
    public override void _Ready()
    {
        // Initializes bow as current bow
        CurrentBowStatsManager.currentBowDamage = this.bowDamage;
        CurrentBowStatsManager.currentBowName = this.bowName;
        CurrentBowStatsManager.currentBowAnimationName = this.bowAnimationName;
        CurrentBowStatsManager.currentBowModelName = this.bowModelName;
    }
}
