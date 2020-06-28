using Godot;
using System;

public class WoodArrow : Area
{
    // Controls arrow velocity
    private int arrowSpeed = 250;

    // Initializes arrow velocity
    public override void _Ready()
    {
        GlobalCurrentBowStatsManager.currentBowArrowVelocity = this.arrowSpeed;
    }

    // Controls which direction the arrows fly
    public override void _Process(float delta)
    {
        Translate((Vector3.Forward) * arrowSpeed * delta);
    }

    // Determines whether the arrow hit an Area, and if so deletes the arrow
    void _on_WoodArrow_area_entered(Area area)
    {
        if (area.IsInGroup("Enemy"))
        {
            QueueFree();
        }
    }
}
