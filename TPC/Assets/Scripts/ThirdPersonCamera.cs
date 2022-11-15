using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
  [SerializeField]
  Transform mPlayer;
  [SerializeField]
  Vector3 CameraOffsetPos = Vector3.zero;
  [SerializeField]
  Vector3 CameraOffsetRot = Vector3.zero;
  [SerializeField]
  float DampingFactor = 5.0f;
  [SerializeField]
  float RotationSpeed = 20.0f;
  [SerializeField]
  float MinPitch = 0.0f;
  [SerializeField]
  float MaxPitch = 20.0f;
  [SerializeField]
  FixedTouchField mTouchField;

  TPC mTpc;

  // Start is called before the first frame update
  void Start()
  {
    CameraConstants.DampingFactor = DampingFactor;
    CameraConstants.CameraPositionOffset = CameraOffsetPos;
    CameraConstants.CameraRotationOffset = CameraOffsetRot;

    //mTpc = new TPCTrack(transform, mPlayer);
    //mTpc = new TPCFollowTrackPosAndRotation(transform, mPlayer);
    mTpc = new TPCIndependent(transform, mPlayer, mTouchField);
  }

  // Update is called once per frame
  void Update()
  {
    CameraConstants.DampingFactor = DampingFactor;
    CameraConstants.CameraPositionOffset = CameraOffsetPos;
    CameraConstants.CameraRotationOffset = CameraOffsetRot;
    CameraConstants.RotationSpeed = RotationSpeed;
    CameraConstants.MinPitch = MinPitch;
    CameraConstants.MaxPitch = MaxPitch;

  }

  private void LateUpdate()
  {
    mTpc.Calculate();
  }
}
