using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
  // We only keep reference the base class.
  // And use polyorphism to determind at the runtime
  // what actual camera we using.
  private TPC mTpc;

  public Transform mPlayer;

  // Start is called before the first frame update
  void Start()
  {
    mTpc = new TPCTrack(mPlayer, transform);
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void LateUpdate()
  {
    mTpc.Calculate();
  }
}
