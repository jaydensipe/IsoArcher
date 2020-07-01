using Godot;
using System;
using Godot.Collections;


// All of Game Logic for IsoArcher
public class GameController : Node
{
    // Variables for picking 1 of 4 locations for an enemy to spawn
    private PackedScene goblin = new PackedScene();
    private readonly Random rng = new Random();
    private readonly Array<Position3D> positionArray = new Array<Position3D>();

    // Wave spawning enemy logic
    public static int globalCurrentWave = 0;
    public static int globalEnemiesAmount = 0;
    private bool waveEnded = false;
    
    // Variable for global player gold
    public static int globalGold = 0;

    // UI Elements
    private Control shopUI;
    private Control spawnEnemyUI;


    
    public override void _Ready()
    {
        // Determines what position enemies should spawn from
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D"));
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D2"));
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D3"));
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D4"));

        // Connects start wave signal
        var hudUINode = GetNode<Control>("UI/HudUI");
        hudUINode.Connect("startWave", this, "_start_Wave");
        
        // Gets UI Element Nodes
        shopUI = GetNode<Control>("UI/HudUI/Shop/HBoxContainer/Shop");
        spawnEnemyUI = GetNode<Control>("UI/HudUI/StartWave/HBoxContainer/StartWave");
    }
    
    
    public override void _Process(float delta)
    {
        UpdateUI();
    }
    

    // Controls the waves and how the enemies spawn, and spawns an enemy every x seconds
    private async void WaveSystem()
    {
        waveEnded = false;
        globalCurrentWave += 1;
        globalEnemiesAmount = (int)Math.Ceiling(globalCurrentWave * 3.5);
        EnemyBaseClass.globalEnemiesRemaining = globalEnemiesAmount;

        for (int i = 0; i < globalEnemiesAmount; i++)
        {
            await ToSignal(GetNode<Timer>("Timers/EnemySpawnTimer"), "timeout");
            SpawnEnemy(positionArray);
        }
        
        waveEnded = true;
    }

    private void SpawnEnemy(Array<Position3D> positionArraysForSpawning)
    {
        // Randomly selects a spawn location for enemies
        var randomEnemySelection = rng.Next(positionArraysForSpawning.Count);
        var spawnLocation = positionArraysForSpawning[randomEnemySelection];
        
        // Instances enemy
        goblin = (PackedScene) ResourceLoader.Load("res://IsoArcher/Enemies/Goblin/Goblin.tscn");
        var enemyInstance = (KinematicBody) goblin.Instance();

        // Changes scale of enemy and transform
        enemyInstance.Scale = (new Vector3(0.15f, 0.15f, 0.15f));
        enemyInstance.GlobalTransform = (spawnLocation.GlobalTransform);

        AddChild(enemyInstance);
    }

    // Updates player HUD (gold, enemies remaining, etc.)
    private void UpdateUI()
    {
        GetNode<Label>("UI/HudUI/CurrentWave/HBoxContainer/WaveAmount").Text = globalCurrentWave.ToString();
        GetNode<Label>("UI/HudUI/Gold/HBoxContainer/GoldAmount").Text = globalGold.ToString();
        GetNode<Label>("UI/HudUI/EnemiesLeft/HBoxContainer/EnemiesLeftAmount").Text = EnemyBaseClass.globalEnemiesRemaining.ToString();
        
        if (EnemyBaseClass.globalEnemiesRemaining == 0)
        {
            spawnEnemyUI.Show();
            shopUI.Show();
        }
    }

    // Stars current wave
    void _start_Wave()
    {
        WaveSystem();
        spawnEnemyUI.Hide();
        shopUI.Hide();
    }
}
