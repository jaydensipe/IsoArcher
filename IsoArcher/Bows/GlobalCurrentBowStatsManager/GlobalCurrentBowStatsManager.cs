using Godot;
using System;

public static class GlobalCurrentBowStatsManager
{ 
    
    // Current bow variables for global use
    public static int currentBowDamage = 0;
    public static string currentBowName = "";
    public static string currentBowDrawNameAnim = "";
    public static string currentBowRelNameAnim = "";
    public static string currentBowModelName = "";
    public static float currentBowRofSpeed = 1.25f;
    public static int currentBowArrowVelocity = 250;

    public static void Reset()
    {
        currentBowDamage = 0;
        currentBowName = "";
        currentBowDrawNameAnim = "";
        currentBowRelNameAnim = "";
        currentBowModelName = "";
        currentBowRofSpeed = 1.25f;
        currentBowArrowVelocity = 250;
    }

}