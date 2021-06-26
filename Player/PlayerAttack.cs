using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_Manager;
    private int damaging;
    private bool stopFiring;
    private Camera mainCam;
    private float fireRate = 15f;
    private float nextFireRate;
    private float range;
    private Transform gunEnd;
    private WaitForSeconds time_Delay = new WaitForSeconds(0.01f);
    private CharacterController character;
    [SerializeField] private GameObject hole_Flash;
    [SerializeField] private GameObject blood_Flash;
    [SerializeField] private GameObject boom_Flash;
    [HideInInspector] public bool isFiring;
    private bool isReloading;
    private float reloadTimer = 1f;
    private int bulletsUsed = 1;
    private GameObject muzzle_flash_Parent;
    private bool isMoving;
    private PlayerSprintAndCrouch sprintAndCrouch;

    void Awake()
    {    
        weapon_Manager = GetComponent<WeaponManager>();
        sprintAndCrouch = GetComponent<PlayerSprintAndCrouch>();
        character = GetComponent<CharacterController>();
        muzzle_flash_Parent = GameObject.FindGameObjectWithTag("MFP");
        mainCam = Camera.main;
    }
    void Update()
    {   
        if(GetComponent<WeaponManager>().GetCurrentWeapon().currentBullets <= 0){
             return;
        }
        if(isReloading){
             return;
        }
        if(GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo <=0 ){
             StartCoroutine(Reload());
             return;
        }
        //Movement Animation
        MovementAnimation();
        //WeaponShoot
        WeaponShoot();
    }
     private void WeaponShoot(){
        
        if(weapon_Manager.GetCurrentWeapon().fire_Type == FireType.SINGLE){
            if(isFiring && !isMoving){
                BulletFire();
                isFiring = false;
            }else{
                 GetComponent<WeaponManager>().GetCurrentWeapon().StopShootAnimation();
            }
        }//Revolver
        if(weapon_Manager.GetCurrentWeapon().bullet_Type == BulletType.LASER){
            if(isFiring && !isMoving){
                 LaserFire();
            }else{
                weapon_Manager.GetCurrentWeapon().WeaponSoundStop();
                GetComponent<WeaponManager>().GetCurrentWeapon().StopShootAnimation();
            }
        }//GunLight
        else if(weapon_Manager.GetCurrentWeapon().fire_Type == FireType.MULTIPLE){
            if(isFiring && !isMoving){
                BulletsFire();
            }else{
                weapon_Manager.GetCurrentWeapon().WeaponSoundStop();
                GetComponent<WeaponManager>().GetCurrentWeapon().StopShootAnimation();
            }
        }//ShortGun,MachineGun,AssaultRifle

    }
    private void BulletFire(){
             
             //for reloading purpose
             GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo--; 
             //for limiting the bullets
             GetComponent<WeaponManager>().GetCurrentWeapon().BulletsDecrement(bulletsUsed);
             //Shoot Animation
             GetComponent<WeaponManager>().GetCurrentWeapon().StartShootAnimation();
             GetComponent<WeaponManager>().GetCurrentWeapon().stopIdleAnimation(); 
             RaycastHit hit;
             range = weapon_Manager.GetCurrentWeapon().weapon_Range;
             //muzzleflash
             weapon_Manager.GetCurrentWeapon().MuzzleFlash();
             //Weapon sound
             weapon_Manager.GetCurrentWeapon().WeaponSoundPlay();
             damaging = weapon_Manager.GetCurrentWeapon().damage;

             if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit,range)){
                 //Enemy alias robot
                 if(hit.transform.gameObject.layer == 8){
                     //Muzzleflash
                     BoomMuzzleFlash(hit);                
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     RobotStats headshot_Ui = hit.transform.GetComponent<RobotStats>();
                     if(hit.collider.CompareTag("Head")){
                        health.Damage(damaging*5);
                        headshot_Ui.DisplayHeadShot();
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                 //Towerdefence
                 else if(hit.transform.gameObject.layer == 12){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     if(hit.collider.CompareTag("Head")){
                        hit.transform.GetComponentInParent<HealthScript>().Damage(damaging*5);
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                 //Lilly
                 else if(hit.transform.gameObject.layer == 11){
                     //bloodMuzzleflash
                     BloodFlash(hit);                   
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     if(hit.collider.CompareTag("Head")){
                        health.Damage(damaging*5);
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                 //Industry
                 else if(hit.transform.gameObject.layer == 18){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                 }
             }
    }
    private void BulletsFire(){


        if(Time.time >= nextFireRate){
             
             //for reloading purpose
             GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo--;
             //for limiting the bullets
             GetComponent<WeaponManager>().GetCurrentWeapon().BulletsDecrement(bulletsUsed); 
             //Shoot Animation
             GetComponent<WeaponManager>().GetCurrentWeapon().StartShootAnimation();
             GetComponent<WeaponManager>().GetCurrentWeapon().stopIdleAnimation(); 
             nextFireRate = Time.time + 1/fireRate;
             RaycastHit hit;
             range = weapon_Manager.GetCurrentWeapon().weapon_Range;
             //Muzzleflash
             weapon_Manager.GetCurrentWeapon().MuzzleFlash();
             //Weapon sound
             weapon_Manager.GetCurrentWeapon().WeaponSoundPlay();
             damaging = weapon_Manager.GetCurrentWeapon().damage;

             if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit,range)){
                 //Enemy alias robot
                 if(hit.transform.gameObject.layer == 8){
                     //Muzzleflash
                     BoomMuzzleFlash(hit);                
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     RobotStats headshot_Ui = hit.transform.GetComponent<RobotStats>();
                     if(hit.collider.CompareTag("Head")){
                        health.Damage(damaging*5);
                        headshot_Ui.DisplayHeadShot();
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                  //Towerdefence
                 else if(hit.transform.gameObject.layer == 12){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     if(hit.collider.CompareTag("Head")){
                        hit.transform.GetComponentInParent<HealthScript>().Damage(damaging*5);
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                 //Lilly
                 else if(hit.transform.gameObject.layer == 11){
                     //bloodMuzzleflash
                     BloodFlash(hit);                   
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     if(hit.collider.CompareTag("Head")){
                        health.Damage(damaging*5);
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                 //Industry
                 else if(hit.transform.gameObject.layer == 18){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                 }
             }
        }
    }
    private void LaserFire(){

        

        if(Time.time >= nextFireRate){
            
             //laser start positiom
             weapon_Manager.GetCurrentWeapon().laserStartPosition();
             //limts the lasertime
             StartCoroutine(LaserEffect()); 
             //for reloading purpose
             GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo--;
             //for limiting the bullets
             GetComponent<WeaponManager>().GetCurrentWeapon().BulletsDecrement(bulletsUsed);
             //Shoot Animation
             GetComponent<WeaponManager>().GetCurrentWeapon().StartShootAnimation();
             GetComponent<WeaponManager>().GetCurrentWeapon().stopIdleAnimation();  
             nextFireRate = Time.time + 1/fireRate*2;
             RaycastHit hit;
             range = weapon_Manager.GetCurrentWeapon().weapon_Range;
             //Muzzleflash
             weapon_Manager.GetCurrentWeapon().MuzzleFlash();
             //Weapon sound
             weapon_Manager.GetCurrentWeapon().WeaponSoundPlay();
             damaging = weapon_Manager.GetCurrentWeapon().damage;

             if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit,range)){
                 //laserline endposition
                  weapon_Manager.GetCurrentWeapon().laserEndPosition(hit); 
                 //Enemy alias robot
                 if(hit.transform.gameObject.layer == 8){
                     //Muzzleflash
                     BoomMuzzleFlash(hit);                
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     RobotStats headshot_Ui = hit.transform.GetComponent<RobotStats>();
                     if(hit.collider.CompareTag("Head")){
                        health.Damage(damaging*5);
                        headshot_Ui.DisplayHeadShot();
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                  //Towerdefence
                 else if(hit.transform.gameObject.layer == 12){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     if(hit.collider.CompareTag("Head")){
                        hit.transform.GetComponentInParent<HealthScript>().Damage(damaging*5);
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                 //Lilly
                 else if(hit.transform.gameObject.layer == 11){
                     //bloodMuzzleflash
                     BloodFlash(hit);                   
                     HealthScript health = hit.transform.GetComponent<HealthScript>();
                     if(hit.collider.CompareTag("Head")){
                        health.Damage(damaging*5);
                     }
                     else{
                        health.Damage(damaging);
                     }
                 }
                 //Industry
                 else if(hit.transform.gameObject.layer == 18){
                     //HoleMuzzleflash
                     HoleFlash(hit);                   
                 }

             }else{
                weapon_Manager.GetCurrentWeapon().laserEndNoHitPosition(mainCam); 
             }
        }
    }
    private void HoleFlash(RaycastHit hit){
        GameObject hole = Instantiate(hole_Flash,hit.point,Quaternion.LookRotation(-hit.normal));
        hole.transform.parent = muzzle_flash_Parent.transform;
        Destroy(hole,0.25f);
    }
    private void BloodFlash(RaycastHit hit){
        GameObject blood = Instantiate(blood_Flash,hit.point,Quaternion.LookRotation(-hit.normal));
        blood.transform.parent = muzzle_flash_Parent.transform;
        Destroy(blood,0.25f);
    }
    private void BoomMuzzleFlash(RaycastHit hit){
        GameObject boom = Instantiate(boom_Flash,hit.point,Quaternion.LookRotation(-hit.normal));
        boom.transform.parent = muzzle_flash_Parent.transform;
        Destroy(boom,0.25f);
    }
    public void ButtonBulletStartFiring(){
               isFiring = true; 
    }
    public void ButtonBulletStopFiring(){
               isFiring = false;
    }
     
    private IEnumerator Reload(){
        isReloading = true;
        GetComponent<WeaponManager>().GetCurrentWeapon().StartReloadAnimation();
        GetComponent<WeaponManager>().GetCurrentWeapon().ReloadSoundPlay();
        yield return new WaitForSeconds(reloadTimer - 0.25f);
        GetComponent<WeaponManager>().GetCurrentWeapon().WeaponSoundStop();   
        GetComponent<WeaponManager>().GetCurrentWeapon().StopReloadAnimation();
        yield return new WaitForSeconds(0.25f);
        GetComponent<WeaponManager>().GetCurrentWeapon().currentAmmo = GetComponent<WeaponManager>().GetCurrentWeapon().maxAmmo;
        isReloading = false;
    }
    private IEnumerator LaserEffect(){
        weapon_Manager.GetCurrentWeapon().laser.enabled = true;
        yield return new WaitForSeconds(0.05f); 
        weapon_Manager.GetCurrentWeapon().laser.enabled = false;
    }
    private void MovementAnimation(){
        if((character.velocity.magnitude > 1f && character.isGrounded) && character.height == sprintAndCrouch.stand_Height){
            isMoving = true;
            weapon_Manager.GetCurrentWeapon().stopIdleAnimation();
            StartCoroutine(FixingJumpAnimation());
        }
        else if(character.isGrounded && character.height == sprintAndCrouch.crouch_Height){
            isMoving = false;
            weapon_Manager.GetCurrentWeapon().stopWalkAnimation();
            weapon_Manager.GetCurrentWeapon().startIdleAnimation();
        }
        else{
            isMoving = false;
            weapon_Manager.GetCurrentWeapon().stopWalkAnimation();
            weapon_Manager.GetCurrentWeapon().startIdleAnimation();
        }
    }
    private IEnumerator FixingJumpAnimation(){
        yield return new WaitForSeconds(0.025f);
        weapon_Manager.GetCurrentWeapon().startWalkAnimation();
    }
}
