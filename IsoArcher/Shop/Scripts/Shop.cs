using Godot;
using System;

public class Shop : Spatial
{
    private int bowDamageCount = 0;
    private float bowRofUpdateCount = 0;
    private int arrowVelocityUpdateCount = 25;

    private void UpdateBowStats()
    {
        GlobalCurrentBowStatsManager.currentBowDamage += bowDamageCount * 50;
        GlobalCurrentBowStatsManager.currentBowRofSpeed += bowRofUpdateCount * 0.25f;
        GlobalCurrentBowStatsManager.currentBowArrowVelocity += arrowVelocityUpdateCount * 10;
        GD.Print(GlobalCurrentBowStatsManager.currentBowArrowVelocity);
    }

    // public override void _Process(float delta)
    // {
    //     UpdateBowStats();
    // }
}
