using Godot;
using System;

public class Shop : Spatial
{
    public static int bowDamageCount = 0;
    public static int bowRofCount = 0;
    public static int arrowVelocityCount = 0;
    
    private PackedScene game = new PackedScene();
    
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    // Variables for updating the show UI
    private Control bowDamageUpgrade;
    private Label bowDamageUpgradeLabel;
    private Control bowROFUpgrade;
    private Label bowROFUpgradeLabel;
    private Control arrowVelocityUpgrade;
    private Label arrowVelocityUpgradeLabel;

    private void UpdateBowStats()
    {
       
        // GlobalCurrentBowStatsManager.currentBowRofSpeed += bowRofUpdateCount * 0.25f;
        // GlobalCurrentBowStatsManager.currentBowArrowVelocity += arrowVelocityUpdateCount * 10;
        // GD.Print(GlobalCurrentBowStatsManager.currentBowArrowVelocity);
    }

    public override void _Ready()
    {
        // Connects the button for upgrading bow damage, and updates the bow damage stats number
        bowDamageUpgrade = GetNode<Control>("isoArcherShopScene/isoArcherShopMenu/Menu/Panel4/1d7c79ae-8f31-608d-802d-585f347b82d7/BowDamageHudIn3D/Viewport/GUI/Label/Button");
        bowDamageUpgrade.Connect("pressed", this, "_bow_Damage_Upgrade_Pressed");
        bowDamageUpgradeLabel = GetNode<Label>("isoArcherShopScene/isoArcherShopMenu/Menu/Panel4/1d7c79ae-8f31-608d-802d-585f347b82d7/BowDamageHudIn3D/Viewport/GUI/Label/Label2");
        bowDamageUpgradeLabel.Text = bowDamageCount.ToString();
        
        // Connects the button for upgrading bow ROF, and updates the bow ROF stats number
        bowROFUpgrade = GetNode<Control>("isoArcherShopScene/isoArcherShopMenu/Menu/Panel2/86e2cb04-5db3-89bb-b928-76d24eda122e/BowROFHudIn3DNew/Viewport/GUI/Label/Button");
        bowROFUpgrade.Connect("pressed", this, "_bow_Rof_Upgrade_Pressed");
        bowROFUpgradeLabel = GetNode<Label>("isoArcherShopScene/isoArcherShopMenu/Menu/Panel2/86e2cb04-5db3-89bb-b928-76d24eda122e/BowROFHudIn3DNew/Viewport/GUI/Label/Label2");
        bowROFUpgradeLabel.Text = bowRofCount.ToString();
        
        arrowVelocityUpgrade = GetNode<Control>("isoArcherShopScene/isoArcherShopMenu/Menu/Panel3/b832e5c1-4bdd-975c-63fe-b90ede206dfa/BowArrowVelocityHudIn3D/Viewport/GUI/Label/Button");
        arrowVelocityUpgrade.Connect("pressed", this, "_arrow_Velocity_Upgrade_Pressed");
        arrowVelocityUpgradeLabel = GetNode<Label>("isoArcherShopScene/isoArcherShopMenu/Menu/Panel3/b832e5c1-4bdd-975c-63fe-b90ede206dfa/BowArrowVelocityHudIn3D/Viewport/GUI/Label/Label2");
        arrowVelocityUpgradeLabel.Text = arrowVelocityCount.ToString();

        UpdateGold();

    }

    private void UpdateGold()
    {
        var goldUpdate = GetNode<Label>("GoldUI/Gold/HBoxContainer/GoldAmount");
        goldUpdate.Text = GameController.globalGold.ToString();
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

    // Signals to manage button presses for shop and updates stats for the bow
    void _bow_Damage_Upgrade_Pressed()
    {
        if (GameController.globalGold >= 200 && bowDamageCount == 0)
        {
            GameController.globalGold -= 200;
            bowDamageCount++;
            GlobalCurrentBowStatsManager.currentBowDamage = (bowDamageCount * 50) + 50;
            bowDamageUpgradeLabel.Text = bowDamageCount.ToString();
        }
        else if (GameController.globalGold > 499 * bowDamageCount)
        {
            GameController.globalGold -= 500 * bowDamageCount;
            bowDamageCount++;
            GlobalCurrentBowStatsManager.currentBowDamage = (bowDamageCount * 50) + 50;
            bowDamageUpgradeLabel.Text = bowDamageCount.ToString();
        }

        UpdateGold();
        
    }

    void _bow_Rof_Upgrade_Pressed()
    {
        if (GameController.globalGold >= 200 && bowRofCount == 0)
        {
            GameController.globalGold -= 200;
            bowRofCount++;
            GlobalCurrentBowStatsManager.currentBowRofSpeed = 1.25f + (bowRofCount * 0.50f);
            bowROFUpgradeLabel.Text = bowRofCount.ToString();
        }
        else if (GameController.globalGold > 499 * bowRofCount)
        {
            GameController.globalGold -= 500 * bowRofCount;
            bowRofCount++;
            GlobalCurrentBowStatsManager.currentBowRofSpeed = 1.25f + (bowRofCount * 0.50f);
            bowROFUpgradeLabel.Text = bowRofCount.ToString();
        }
        UpdateGold();
        
    }

    void _arrow_Velocity_Upgrade_Pressed()
    {
        if (GameController.globalGold >= 200 && arrowVelocityCount == 0)
        {
            GameController.globalGold -= 200;
            arrowVelocityCount++;
            GlobalCurrentBowStatsManager.currentBowArrowVelocity = (arrowVelocityCount * 50) + 250;
            arrowVelocityUpgradeLabel.Text = arrowVelocityCount.ToString();
        }
        else if (GameController.globalGold > 499 * arrowVelocityCount)
        {
            GameController.globalGold -= 500 * arrowVelocityCount;
            arrowVelocityCount++;
            GlobalCurrentBowStatsManager.currentBowArrowVelocity = (arrowVelocityCount * 50) + 250;
            arrowVelocityUpgradeLabel.Text = arrowVelocityCount.ToString();
        }
        UpdateGold();
    }
}
