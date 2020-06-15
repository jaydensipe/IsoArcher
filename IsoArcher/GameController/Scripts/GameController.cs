using Godot;
using System;
using Godot.Collections;


// All of Game Logic for IsoArcher
public class GameController : Node
{
    // Logic for picking 1 of 4 locations for an enemy to spawn
    private PackedScene testEnemy = new PackedScene();
    private readonly Random rng = new Random();
    private readonly Array<Position3D> positionArray = new Array<Position3D>();

    // Wave spawning enemy logic
    private int currentWave = 0;
    private bool waveEnded = false;
    
    public override void _Ready()
    {
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D"));
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D2"));
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D3"));
        positionArray.Add(GetNode<Position3D>("CurrentWorld/World1/EnemySpawnPositions/Position3D4"));
        
    }

    // Debug to spawn waves
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("TestWave"))
        {
            WaveSystem();
        }
    }

    // Controls the waves and how the enemies spawn, and spawns an enemy every x seconds
    private async void WaveSystem()
    {
        waveEnded = false;
        currentWave += 1;

        for (int i = 0; i < Math.Ceiling(currentWave*4.5); i++)
        {
            SpawnEnemy(positionArray);
            await ToSignal(GetNode<Timer>("Timers/EnemySpawnTimer"), "timeout");
        }

        waveEnded = true;
    }

    private void SpawnEnemy(Array<Position3D> positionArraysForSpawning)
    {
        // Randomly selects a spawn location for enemies
        var randomEnemySelection = rng.Next(positionArraysForSpawning.Count);
        var spawnLocation = positionArraysForSpawning[randomEnemySelection];
        
        // Instances enemy
        testEnemy = (PackedScene) ResourceLoader.Load("res://IsoArcher/Enemies/TestEnemy/TestEnemy.tscn");
        var enemyInstance = (KinematicBody) testEnemy.Instance();
        enemyInstance.Translate(spawnLocation.Translation);
        AddChild(enemyInstance);
        
    }

 

}
