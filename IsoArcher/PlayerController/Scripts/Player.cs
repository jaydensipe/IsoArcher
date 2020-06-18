using Godot;
using System;

public class Player : Spatial
{

  private int playerHealth = 90;

  // Player Movement Script 
  private void Movement()
  {
    

    // Moves the player based on which button they press
    if (Input.IsActionPressed("W"))
    {
      RotationDegrees = new Vector3(0, 90, 0);
    }

    if (Input.IsActionJustPressed("A"))
    {
      RotationDegrees = new Vector3(0, 180, 0);
    }

    if (Input.IsActionJustPressed("S"))
    {
      RotationDegrees = new Vector3(0, 270, 0);
    }

    if (Input.IsActionJustPressed("D"))
    {
      RotationDegrees = new Vector3(0, 360, 0);
    }
  }

  // Logic for determining what happens when a player takes damage
  void _on_PlayerArea_area_entered(Area area)
  {
    GetTree().ReloadCurrentScene();
    // playerHealth -= 30;
    // var playerHitTimer = GetNode<Timer>("PlayerHitDelay");
    // playerHitTimer.Start();
    // await ToSignal(playerHitTimer, "timeout");
  }

// Update method for moving 
  public override void _PhysicsProcess(float delta)
  {
    Movement();
  }
}
  