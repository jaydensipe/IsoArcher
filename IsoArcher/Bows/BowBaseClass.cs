using Godot;
using System;

public class BowBaseClass : Spatial
{
    // Bow variables
    [Export] private string bowName = "";
    [Export] private string bowDrawNameAnim = "";
    [Export] private string bowRelNameAnim = "";
    [Export] private string bowModelName = "";

    // Initializes bow as current bow
    public override void _Ready()
    {
        GlobalCurrentBowStatsManager.currentBowName = this.bowName;
        GlobalCurrentBowStatsManager.currentBowDrawNameAnim = this.bowDrawNameAnim;
        GlobalCurrentBowStatsManager.currentBowRelNameAnim = bowRelNameAnim;
        GlobalCurrentBowStatsManager.currentBowModelName = this.bowModelName;
    }
}
