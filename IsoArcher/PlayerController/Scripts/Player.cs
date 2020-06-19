using Godot;
using System;

public class Player : Spatial
{

  private int playerHealth = 90;
  private bool hasPlayerShot = false;
  private bool canShootBow = false;
  private bool playingBackwards = false;
  private float time;
  
  [Signal]
  public delegate void ShootBow();

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
  
  private async void PlayerBowAnimation()
  {
    
    GD.Print(canShootBow);

    var animationPlayerWeapon = GetNode<AnimationPlayer>("CurrentPlayerModel/isoArcherPlayer/AnimationPlayerNew");
    animationPlayerWeapon.PlaybackSpeed = GlobalCurrentBowStatsManager.currentBowRofSpeed;
    
    if (Input.IsActionPressed("Fire") && canShootBow == false && hasPlayerShot == false)
    {
      playingBackwards = false;
      canShootBow = false;
      if (animationPlayerWeapon.IsPlaying() == false)
      {
        animationPlayerWeapon.Play("PlayerbowDraw");
      }
      
      await ToSignal(animationPlayerWeapon, "animation_finished");
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
      animationPlayerWeapon.PlayBackwards("PlayerbowDraw");
    }
    
    if (Input.IsActionJustReleased("Fire") && canShootBow == true && hasPlayerShot == false)
    {
      EmitSignal(nameof(ShootBow));
      canShootBow = false;
      hasPlayerShot = true;
      animationPlayerWeapon.Stop(false);
      animationPlayerWeapon.Queue("PlayerbowRelease");
      await ToSignal(animationPlayerWeapon, "animation_finished");
      hasPlayerShot = false;
    }
  }

  // Logic for determining what happens when a player takes damage
  void _on_PlayerArea_area_entered(Area area)
  {
    GlobalGoldManager.globalGold = 0;
    GlobalEnemyBaseRemaining.enemiesRemaining = 0;
    GetTree().ReloadCurrentScene();
    // playerHealth -= 30;
    // var playerHitTimer = GetNode<Timer>("PlayerHitDelay");
    // playerHitTimer.Start();
    // await ToSignal(playerHitTimer, "timeout");
  }

// Update method for moving 
  public override void _Process(float delta)
  {
    Movement(delta);
    PlayerBowAnimation();
  }
}
  