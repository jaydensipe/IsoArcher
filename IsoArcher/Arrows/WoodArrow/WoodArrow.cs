using Godot;
using System;

public class WoodArrow : Area
{

    // Controls which direction the arrows fly
    public override void _Process(float delta)
    {
        Translate((Vector3.Forward) * GlobalCurrentBowStatsManager.currentBowArrowVelocity * delta);
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
