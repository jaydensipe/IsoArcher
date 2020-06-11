using Godot;
using System;

public class BowBaseClass : Spatial
{
    // Bow variables
    [Export] public int bowDamage = 0;
    [Export] public string bowName = "";
    [Export] public string bowAnimationName = "";
    [Export] public string bowModelName = "";
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
    
    // Fires ability of current Bow
    public void FireAbility()
    {
        
    }
    
    // Update method for shooting
    public override void _PhysicsProcess(float delta)
    {
        FireWeapon();
    }

}
