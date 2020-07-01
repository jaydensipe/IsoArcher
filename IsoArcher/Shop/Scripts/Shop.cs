using Godot;
using System;

public class Shop : Spatial
{
    private int bowDamageCount = 0;
    private float bowRofUpdateCount = 0;
    private int arrowVelocityUpdateCount = 0;
    
    private PackedScene game = new PackedScene();
    
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    private void UpdateBowStats()
    {
        // GlobalCurrentBowStatsManager.currentBowDamage += bowDamageCount * 50;
        // GlobalCurrentBowStatsManager.currentBowRofSpeed += bowRofUpdateCount * 0.25f;
        // GlobalCurrentBowStatsManager.currentBowArrowVelocity += arrowVelocityUpdateCount * 10;
        // GD.Print(GlobalCurrentBowStatsManager.currentBowArrowVelocity);
    }

    public override void _Process(float delta)
    {
        UpdateBowStats();
    }

    void _on_AnimationTimer_timeout()
    {
        var animationPlayerShopKeeper = GetNode<AnimationPlayer>("isoArcherShopScene/IsoArcherShopkeeper/AnimationPlayer");
        var animationPlayerSign = GetNode<AnimationPlayer>("isoArcherShopScene/isoArcherShopMenu/AnimationPlayer");
        animationPlayerShopKeeper.Play("Entrance");
        animationPlayerSign.Play("shopMenuDropDown");
    }

    void _on_RandomAnimationSlap_timeout()
    {
        var animationPlayerShopKeeper = GetNode<AnimationPlayer>("isoArcherShopScene/IsoArcherShopkeeper/AnimationPlayer");
        var randomNumber = rng.RandiRange(0, 1);
        GD.Print(randomNumber);
        if (randomNumber == 1)
        {
            animationPlayerShopKeeper.PlaybackSpeed = 1.0f;
            animationPlayerShopKeeper.Play("Idle-GoggleAdjust");
        }
    }

    void _on_LeaveShop_pressed()
    {
        game = (PackedScene) ResourceLoader.Load("res://IsoArcher/GameController/GameController.tscn");
        GetTree().ChangeSceneTo(game);
    }
}
