using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float speed = 3f;
    public float stopSpeed = 0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool jump = false;
    private bool isJumped = false;
    private WeaponManager weaponManager;
    public StressReceiver stress;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        float curSpeedX = canMove ? (speed) * SimpleInput.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (speed) * SimpleInput.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (canMove && isJumped && characterController.isGrounded)
        {
            weaponManager.GetCurrentWeapon().stopWalkAnimation();
            weaponManager.GetCurrentWeapon().startIdleAnimation();
            moveDirection.y = jumpSpeed;
            jump = true;
            isJumped = false;
            StartCoroutine(FallCameraEffect());
        }
        else
        {
            moveDirection.y = movementDirectionY;
            jump = false;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -SimpleInput.GetAxis("MouseY") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, SimpleInput.GetAxis("MouseX") * lookSpeed, 0);
        }
    }
    public void Jump(){
        isJumped = true;
        stress.InduceStress(0.1f);
    }
    private IEnumerator FallCameraEffect(){
        yield return new WaitForSeconds(0.8f);
        stress.InduceStress(0.1f);
    }
    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    public void ExitGame(){
       SceneManager.LoadScene("Main");
   }
}