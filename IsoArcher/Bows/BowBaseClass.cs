using Godot;
using System;

public class BowBaseClass : Spatial
{
    // Bow variables
    [Export] private int bowDamage = 0;
    [Export] private string bowName = "";
    [Export] private string bowDrawNameAnim = "";
    [Export] private string bowRelNameAnim = "";
    [Export] private string bowModelName = "";
    [Export] private float bowRofSpeed = 0;

    // Initializes bow as current bow
    public override void _Ready()
    {
        GlobalCurrentBowStatsManager.currentBowDamage = this.bowDamage;
        GlobalCurrentBowStatsManager.currentBowName = this.bowName;
        GlobalCurrentBowStatsManager.currentBowDrawNameAnim = this.bowDrawNameAnim;
        GlobalCurrentBowStatsManager.currentBowRelNameAnim = bowRelNameAnim;
        GlobalCurrentBowStatsManager.currentBowModelName = this.bowModelName;
        GlobalCurrentBowStatsManager.currentBowRofSpeed = this.bowRofSpeed;
    }
}
