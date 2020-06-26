using Godot;
using System;

public static class GlobalGoldManager
{
    // Players current gold
    public static float globalGold = 0;

    public static void Reset()
    {
        globalGold = 0;
    }
}
