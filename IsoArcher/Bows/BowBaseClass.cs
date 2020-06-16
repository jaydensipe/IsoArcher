using Godot;
using System;

public class BowBaseClass : Spatial
{
    // Bow variables
    [Export] private int bowDamage = 0;
    [Export] private string bowName = "";
    [Export] private string bowDrawNameAnim = "";
    [Export] private string bowHoldNameAnim = "";
    [Export] private string bowRelNameAnim = "";
    [Export] private string bowModelName = "";
    private PackedScene arrow = new PackedScene();

    // Handles bow firing delay
    private bool hasBowFired = true;
    private bool canSpawnArrow = false;
    
    // Code for firing current bow
    private async void FireWeapon()
    {
        if (Input.IsActionPressed("Fire"))
        {
            if (hasBowFired == true)
            {
                // Plays bow draw animation
                var animationPlayerWeapon = GetNode<AnimationPlayer>(bowModelName + "/AnimationPlayer");
                animationPlayerWeapon.Play(bowDrawNameAnim);
                
                if (!Input.IsActionJustReleased("Fire"))
                {
                    canSpawnArrow = false;
                }
                
                hasBowFired = false;
                await ToSignal(animationPlayerWeapon, "animation_finished");
                canSpawnArrow = true;
            }
        }
        
        if (Input.IsActionJustReleased("Fire"))
        {
            if (canSpawnArrow == true)
            {
                // Plays bow firing animation and determines if bow can shoot again
                hasBowFired = true;
                var animationPlayerWeapon = GetNode<AnimationPlayer>(bowModelName + "/AnimationPlayer");
                animationPlayerWeapon.Play(bowRelNameAnim);
            
                // Instances arrow
                arrow = (PackedScene) ResourceLoader.Load("res://IsoArcher/Arrows/WoodArrow/WoodArrow.tscn");
                var arrowInstance = (Area)arrow.Instance();
                arrowInstance.Translate(GetNode<Position3D>("ArrowPosition").Translation);
                AddChild(arrowInstance);
                arrowInstance.SetAsToplevel(true);
            }

        }
    }
    
    // Update method for shooting
    public override void _PhysicsProcess(float delta)
    {
        FireWeapon();
    }
    
    // Initializes bow as current bow
    public override void _Ready()
    {
        
        CurrentBowStatsManager.currentBowDamage = this.bowDamage;
        CurrentBowStatsManager.currentBowName = this.bowName;
        CurrentBowStatsManager.currentBowDrawNameAnim = this.bowDrawNameAnim;
        CurrentBowStatsManager.currentBowHoldNameAnim = this.bowHoldNameAnim;
        CurrentBowStatsManager.currentBowRelNameAnim = bowRelNameAnim;
        CurrentBowStatsManager.currentBowModelName = this.bowModelName;
    }
}
