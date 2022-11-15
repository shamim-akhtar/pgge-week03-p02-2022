using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  // We will need access to the CharacterController and the 
  // animator for our player to work.
  //[SerializeField]
  CharacterController mCharacterController;

  [SerializeField]
  Animator mAnimator;

  [SerializeField]
  float mWalkSpeed = 1.5f;
  [SerializeField]
  float mSidewayWalkSpeed = 0.75f;
  [SerializeField]
  float mRotationSpeed = 25.0f;
  [SerializeField]
  float mTurnRate = 200.0f;

  public FixedJoystick mJoystick;
  public bool FollowCameraForward = false;

  void Start()
  {
    mCharacterController = GetComponent<CharacterController>();
  }

  // Update is called once per frame
  void Update()
  {
    // We cannot move the player if either 
    // the animator or the charactercontroll is null.
    if (mAnimator == null) return;
    if (mCharacterController == null) return;

    //// Get input from the joytick or the horizontal and vertical axis.
    //float hInput = Input.GetAxis("Horizontal");
    //float vInput = Input.GetAxis("Vertical");

    float hInput = 2.0f * mJoystick.Horizontal;
    float vInput = 2.0f * mJoystick.Vertical;

    float speed = mWalkSpeed;

    if (Input.GetKey(KeyCode.LeftShift))
    {
      speed = 2 * mWalkSpeed;
    }

    // Rotate the player.
    // We will rotate the player based on the horiontal input.
    //transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
    if (FollowCameraForward)
    {
      if (Mathf.Abs(hInput) > 0.1f)
      {
        // rotate Player towards the camera forward.
        Vector3 eu = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0.0f, eu.y, 0.0f),
            mTurnRate * Time.deltaTime);
      }
    }
    else
    {
      if (Mathf.Abs(hInput) > 0.1f)
      {
        transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
      }
    }


    // now we move the player based on the speed and the input values.
    Vector3 forward = transform.forward;
    mCharacterController.Move(forward * vInput * speed * Time.deltaTime + Vector3.right * hInput * mSidewayWalkSpeed * Time.deltaTime);

    mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed));
    mAnimator.SetFloat("PosX", hInput);
  }
}
