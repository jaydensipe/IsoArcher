using Godot;
using System;

public class WoodArrow : Area
{
    // Controls how fast arrows fly
    [Export] public int arrowSpeed = 250;
    
    // Controls which direction the arrows fly
    public override void _PhysicsProcess(float delta)
    {
        Translate((Vector3.Forward) * arrowSpeed * delta);
    }
}
