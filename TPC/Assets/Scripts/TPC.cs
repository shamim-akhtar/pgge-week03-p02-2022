using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstants
{
  public static Vector3 CameraPositionOffset = Vector3.zero;
  public static Vector3 CameraRotationOffset = Vector3.zero;
  public static float DampingFactor = 2.0f;
}

// We want to implement third-persn camera that is not associated
// with monobehavior. Instead we allow pure C# classes to create
// the third-person camera behavior.
// We also do not want the TPC class to know how to implement the math and phyics
// of the different types of cameras.
abstract public class TPC
{
  // what do we need to make the third-person camera.
  // 1. The camera itself
  // 2. The target.
  protected Transform mCamera;
  protected Transform mPlayer;

  // let's have a constructor.
  public TPC(Transform camera, Transform player)
  {
    mCamera = camera;
    mPlayer = player;
  }

  // Since I (TPC) do not know how to implement the math for the 
  // camera, I will allow all classes derived from me
  // to implement the real behavior.
  public abstract void Calculate();
}

public class TPCTrack : TPC
{
  public TPCTrack(Transform camera, Transform player)
    : base(camera, player)
  {
  }

  public override void Calculate()
  {
    // Get the target position.
    Vector3 targetPos = mPlayer.position;

    // The camera does not want to track the foot of the player,
    // instead we track the head of the player.
    targetPos.y += 2.0f;

    // make the camera look at the target pos.
    mCamera.LookAt(targetPos);

  }
}

public class TPCFollow : TPC
{
  public TPCFollow(Transform camera, Transform player)
    : base(camera, player)
  {
  }

  public override void Calculate()
  {
    //Vector3 forward = mCamera.forward;
    //Vector3 right = mCamera.right;
    //Vector3 up = mCamera.up;

    Vector3 forward = mCamera.rotation * Vector3.forward;
    Vector3 right = mCamera.rotation * Vector3.right;
    Vector3 up = mCamera.rotation * Vector3.up;

    // we then calculate the target position of the camera.
    Vector3 targetPos = mPlayer.position;

    Vector3 desiredPos = targetPos
      + forward * CameraConstants.CameraPositionOffset.z
      + right * CameraConstants.CameraPositionOffset.x
      + up * CameraConstants.CameraPositionOffset.y;

    // finaly lets lerp the movement.
    mCamera.position = Vector3.Lerp(mCamera.position, desiredPos, Time.deltaTime * CameraConstants.DampingFactor);
  }
}

public class TPCFollowTrackPosAndRotation : TPCFollow
{
  public TPCFollowTrackPosAndRotation(Transform camera, Transform player)
    : base(camera, player)
  {
  }

  public override void Calculate()
  {
    Quaternion initialRotation = Quaternion.Euler(CameraConstants.CameraRotationOffset);
    mCamera.rotation = Quaternion.Lerp(mCamera.rotation, mPlayer.rotation * initialRotation, Time.deltaTime * CameraConstants.DampingFactor);
    base.Calculate();
  }
}
