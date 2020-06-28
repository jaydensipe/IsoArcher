using Godot;
using System;

public class Player : Spatial
{
  
  // Players health
  private int playerHealth = 90;
  
  // Variables for controlling bow shooting/animations
  private bool hasPlayerShot = false;
  private bool canShootBow = false;
  private bool playingBackwards = false;
  private PackedScene arrow = new PackedScene();

  // Player Movement Script 
  private void Movement(float delta)
  {

    // Moves the player based on which button they press
    if (Input.IsActionJustPressed("W"))
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
  
  // Everything dealing with bow shooting and playing the animations
  private async void PlayerBowAnimationAndShoot()
  {
    
    // Instances bow and player animations
    var playerAnimations = GetNode<AnimationPlayer>("CurrentPlayerModel/isoArcherPlayer/AnimationPlayerNew");
    playerAnimations.PlaybackSpeed = GlobalCurrentBowStatsManager.currentBowRofSpeed;
    
    var bowAnimations = GetNode<AnimationPlayer>("CurrentBow/" + GlobalCurrentBowStatsManager.currentBowName + "/" + GlobalCurrentBowStatsManager.currentBowModelName +  "/AnimationPlayer");
    bowAnimations.PlaybackSpeed = GlobalCurrentBowStatsManager.currentBowRofSpeed;
    var bowTimer = GetNode<Timer>("Timers/BowDelay");
    bowTimer.WaitTime = 0.96f / GlobalCurrentBowStatsManager.currentBowRofSpeed;
    // var bowParticles = GetNode<Particles>("CurrentBow/Particles");
    
    
    // Logic for bow shooting and playing animations
    if (Input.IsActionPressed("Fire") && canShootBow == false && hasPlayerShot == false)
    {
      playingBackwards = false;
      canShootBow = false;
      
      if (playerAnimations.IsPlaying() == false)
      {
        playerAnimations.Play("PlayerbowDraw");
        bowAnimations.Play(GlobalCurrentBowStatsManager.currentBowDrawNameAnim);
        bowTimer.Start();
      }
      
      await ToSignal(bowTimer, "timeout");
      if (playingBackwards == true)
      {
        canShootBow = false;
      }
      else
      {
        canShootBow = true;
      }
    }
    if (Input.IsActionJustReleased("Fire") && canShootBow == false && hasPlayerShot == false)
    {
      playingBackwards = true;
      playerAnimations.PlayBackwards("PlayerbowDraw");
      bowAnimations.PlayBackwards(GlobalCurrentBowStatsManager.currentBowDrawNameAnim);
      bowTimer.Stop();
    }
    
    if (Input.IsActionJustReleased("Fire") && canShootBow == true && hasPlayerShot == false)
    {
      // bowParticles.Emitting = true;
      canShootBow = false;
      hasPlayerShot = true;
      playerAnimations.Stop(false);
      playerAnimations.Queue("PlayerbowRelease");
      bowAnimations.Stop(false);
      bowAnimations.Queue(GlobalCurrentBowStatsManager.currentBowRelNameAnim);
      
      arrow = (PackedScene) ResourceLoader.Load("res://IsoArcher/Arrows/WoodArrow/WoodArrow.tscn");
      var arrowInstance = (Area)arrow.Instance();
      arrowInstance.Transform = (GetNode<Position3D>("CurrentBow/" + GlobalCurrentBowStatsManager.currentBowName + "/ArrowPosition").Transform);
      arrowInstance.Translate(Vector3.Forward);
      arrowInstance.Scale = new Vector3(0.7f, 0.7f, 0.7f);
      AddChild(arrowInstance);
      arrowInstance.SetAsToplevel(true);
      
      await ToSignal(playerAnimations, "animation_finished");
      hasPlayerShot = false;
    }
  }

  // Logic for determining what happens when a player takes damage
  void _on_PlayerArea_area_entered(Area area)
  {
    if (area.IsInGroup("Enemy"))
    {
      playerHealth -= 30;
      if (playerHealth <= 0)
      {
        GetTree().ReloadCurrentScene();
      }
    }
  }
  
  public override void _Process(float delta)
  {
    // Update methods for moving and shooting
    Movement(delta);
    PlayerBowAnimationAndShoot();
  }
  
}
  