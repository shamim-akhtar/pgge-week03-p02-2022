using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class contains pure C# code for 
// manipulating third-person camera controls.

// TPC class below doesnt know how to implement the camera math.
// We will let it be for the derived classes.
abstract public class TPC
{
  // What do we need to create our third-petson camera?
  // 1. Camera itself.
  // 2. Target / Player

  protected Transform mPlayer;
  protected Transform mCamera;

  public Transform Camera
  {
    get { return mCamera; }
  }


  public TPC(Transform player, Transform camera)
  {
    mPlayer = player;
    mCamera = camera;
  }

  // all derived classes must implement the Calculate function.
  // This is where the real math for a specific third-person camera happens.
  public abstract void Calculate();
}

// Now we implement the Track camera control.
public class TPCTrack : TPC
{
  public TPCTrack(Transform player, Transform camera)
    : base(player, camera)
  { }
  public override void Calculate()
  {
    Vector3 targetPos = mPlayer.position;

    // Lets not track the foot of the player. Intead, we add
    // a height so that we can track the head of the player.
    targetPos.y += 2.0f;

    mCamera.LookAt(targetPos);
  }

}
