using Godot;
using System;

public class HudUI : Control
{
    private PackedScene shop = new PackedScene();

    [Signal]
    private delegate void startWave();
    
    // Player hits shop button
    void _on_Shop_pressed()
    {
        shop = (PackedScene) ResourceLoader.Load("res://IsoArcher/Shop/Shop.tscn");
        GetTree().ChangeSceneTo(shop);
    }

    // Tells game to start wave
    void _on_StartWave_pressed()
    {
        EmitSignal(nameof(startWave));
    }
}
