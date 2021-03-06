//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    [SerializeField] private float sprint_Speed = 5f;
    [SerializeField] private float walk_Speed = 3f;
    [SerializeField] private float crouch_Speed = 1f;
    public float stand_Height = 2f;
    public float crouch_Height = 1f;
    [SerializeField] private float stand_Camera_Height = 1.64f;
    [SerializeField] private float crouch_Camera_Height = 0.64f;
    [SerializeField] private float walk_Step_Distance = 0.0525f;
    [SerializeField] private float sprint_Step_Distance = 0.04f;
    [SerializeField] private float crouch_Step_Distance = 0.055f;
    [SerializeField] private float walk_Min_Volume = 0.3f;
    [SerializeField] private float walk_Max_Volume = 0.5f;
    [SerializeField] private float sprint_Volume = 0.75f;
    [SerializeField] private float crouch_Volume = 0.3f;
    private PlayerAudioScript player_Audio;
    private SC_FPSController playerMovement;
    private Transform look_Root;
    private bool isCrouching;
    private PlayerStats playerStats;
    private float sprint_Value = 100f;
    private float sprint_Treshold = 15f;
    private bool isSprinting;
    private CharacterController characterController;
    private Camera mainCamera;

    void Awake()
    {
        playerMovement = GetComponent<SC_FPSController>();
        characterController = GetComponent<CharacterController>();
        player_Audio = GetComponentInChildren<PlayerAudioScript>();
        look_Root = transform.GetChild(0);
        playerStats = GetComponent<PlayerStats>();
        mainCamera = Camera.main;
    }
    void Start(){
        playerMovement.speed = walk_Speed;
        player_Audio.step_Distance = walk_Step_Distance;
        player_Audio.volume_Min = walk_Min_Volume;
        player_Audio.volume_Max = walk_Max_Volume;
    }
    void Update()
    { 
        if(isSprinting){
           Sprint();
        }else{
            if(sprint_Value!=100f){
                sprint_Value += sprint_Treshold/4*Time.deltaTime;
                playerStats.DisplayStaminaStats(sprint_Value);
                if(sprint_Value >= 100f){
                    sprint_Value = 100f;
                }
            }
            playerMovement.speed = walk_Speed;
            player_Audio.step_Distance = walk_Step_Distance;
            player_Audio.volume_Min = walk_Min_Volume;
            player_Audio.volume_Max = walk_Max_Volume;
        }
      // Crouch();
      if(isCrouching){
             Crouching();
      }

    }
    //for Ui Sprint button
    private void Sprint(){
        if(sprint_Value > 0){

            sprint_Value -= sprint_Treshold*Time.deltaTime;
            playerStats.DisplayStaminaStats(sprint_Value);

            if(!isCrouching){
                 playerMovement.speed = sprint_Speed;
                 player_Audio.step_Distance = sprint_Step_Distance;
                 player_Audio.volume_Min = sprint_Volume;
                 player_Audio.volume_Max = sprint_Volume;
            }
        }
        if(sprint_Value!=100f){
                sprint_Value += sprint_Treshold/4*Time.deltaTime;
                playerStats.DisplayStaminaStats(sprint_Value);
                if(sprint_Value >= 100f){
                    sprint_Value = 100f;
                }
        }
        if(sprint_Value < 0f){
                sprint_Value = 0f;
                playerMovement.speed = walk_Speed;
                player_Audio.step_Distance = walk_Step_Distance;
                player_Audio.volume_Min = walk_Min_Volume;
                player_Audio.volume_Max = walk_Max_Volume;
        }
    }
    //For joystick
    private void Crouching(){
                characterController.height = crouch_Height;
                characterController.center = new Vector3(0,characterController.height/2,0);
                mainCamera.transform.localPosition = new Vector3(0,crouch_Camera_Height,0);
                playerMovement.speed = crouch_Speed;
                player_Audio.step_Distance = crouch_Step_Distance;
                player_Audio.volume_Min = crouch_Volume;
                player_Audio.volume_Max = crouch_Volume;
    
    }
    public void StandAndJump(){
                if(characterController.height != crouch_Height){
                    playerMovement.Jump();
                    isCrouching = false;
                }
                else{
                    characterController.height = stand_Height;
                    characterController.center = new Vector3(0,characterController.height/2,0);
                    mainCamera.transform.localPosition = new Vector3(0,stand_Camera_Height,0);
                    playerMovement.speed = walk_Speed;
                    player_Audio.step_Distance = walk_Step_Distance;
                    player_Audio.volume_Min = walk_Min_Volume;
                    player_Audio.volume_Max = walk_Max_Volume;
                    isCrouching = false;
                }
    }
    public void ButtonStartCrouching(){
        isCrouching = true;
    }
    public void ButtonStartSprinting(){
        isSprinting = true;
    }
    public void ButtonStopSprinting(){
         isSprinting = false;
    }
       
}
