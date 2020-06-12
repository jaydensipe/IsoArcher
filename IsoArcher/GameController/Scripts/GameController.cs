using Godot;
using System;
using Godot.Collections;


// All of Game Logic for IsoArcher
public class GameController : Node
{
    
    private PackedScene testEnemy = new PackedScene();
    private readonly Random rng = new Random();
    private readonly Array<Position3D> positionArray = new Array<Position3D>();
    
    public override void _Ready()
    {
        positionArray.Add(GetNode<Position3D>("World1/EnemySpawnPositions/Position3D"));
        positionArray.Add(GetNode<Position3D>("World1/EnemySpawnPositions/Position3D2"));
        positionArray.Add(GetNode<Position3D>("World1/EnemySpawnPositions/Position3D3"));
        positionArray.Add(GetNode<Position3D>("World1/EnemySpawnPositions/Position3D4"));
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

    // Spawns an enemy every x seconds
    void _on_EnemySpawnTimer_timeout()
    {
        SpawnEnemy(positionArray);
    }
    
}
