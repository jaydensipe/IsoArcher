using Godot;
using System;

public class Player : Spatial
{

  private int playerHealth = 90;

  // Player Movement Script 
  private void Movement()
  {

    var playerKinematicBody = (KinematicBody) GetNode("KinematicBody");

    // Moves the player based on which button they press
    if (Input.IsActionPressed("W"))
    {
      playerKinematicBody.RotationDegrees = new Vector3(0, 90, 0);
    }

    if (Input.IsActionJustPressed("A"))
    {
      playerKinematicBody.RotationDegrees = new Vector3(0, 180, 0);
    }

    if (Input.IsActionJustPressed("S"))
    {
      playerKinematicBody.RotationDegrees = new Vector3(0, 270, 0);
    }

    if (Input.IsActionJustPressed("D"))
    {
      playerKinematicBody.RotationDegrees = new Vector3(0, 360, 0);
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
  