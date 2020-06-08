using Godot;
using System;

public class Player : Spatial
{


  private SpatialMaterial spmForLine;
  // Player Movement Script
  private void Movement()
  {

    var playerKinematicBody = (KinematicBody)GetNode("KinematicBody");
    
    // Moves the player based on which button they press
    if(Input.IsActionPressed("W"))
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


  private void AimSystem()
  {

  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    Movement();
    AimSystem();
  }

  public void _Ready()
  {
    // Sets line color for drawing line from player to mouse
    spmForLine.FlagsUnshaded = true;
    spmForLine.FlagsUsePointSize = true;
    spmForLine.AlbedoColor = Colors.White;
  }
}
