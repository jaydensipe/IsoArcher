using Godot;
using System;

public class WoodArrow : Area
{
    // Controls how fast arrows fly
    [Export] private int arrowSpeed = 250;
    
    
    // Controls which direction the arrows fly
    public override void _Process(float delta)
    {
        Translate((Vector3.Forward) * arrowSpeed * delta);
    }

    // Determines whether the arrow hit an Area or KinematicBody, and if so deletes the arrow
    private void _on_WoodArrow_area_entered(Area area)
    {
        QueueFree();
    }

    private void _on_WoodArrow_body_entered(KinematicBody body)
    {
        QueueFree();
    }
}
