//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BulletType{
    BULLET,
    LASER
     
}
public enum FireType{
    SINGLE,
    MULTIPLE
}
public enum AimType{
    AIM
}
public class WeaponHandler : MonoBehaviour
{   

    private Animator anim;
    public BulletType bullet_Type;
    public FireType fire_Type;
    public AimType aim_Type;
    public int damage = 100;
    public float weapon_Range = 100f;
    [SerializeField] private AudioSource gun_Sound;
    [SerializeField] private AudioClip shoot,reload;
    [SerializeField] GameObject muzzleFlash;
    public Transform gun_End;
    public int maxBullets = 30;
    [HideInInspector] public int currentBullets;

    private WeaponManager weapon_Manager;
    public bool stopFiring;
    public int maxAmmo = 30;
    [SerializeField] private Image crossHairUi;
    [HideInInspector] public int currentAmmo;
    public LineRenderer laser;
    [SerializeField] Sprite crossHair;

    private void Start()
    {
        currentAmmo = maxAmmo;
        currentBullets = maxBullets;
    }

    private void Awake()
    {   
        crossHairUi.sprite = crossHair;
        weapon_Manager = GetComponentInParent<WeaponManager>();
        anim = GetComponent<Animator>();
    }

    public void MuzzleFlash(){
        GameObject effect = Instantiate(muzzleFlash,gun_End.position,Quaternion.identity);
        effect.transform.parent = gun_End;
        Destroy(effect,0.1f);
    } 
    public void laserStartPosition(){
        laser.SetPosition(0,gun_End.position);
    }
    public void laserEndPosition(RaycastHit hit){
        laser.SetPosition(1,hit.point);
    }
    public void laserEndNoHitPosition(Camera cam){
        laser.SetPosition(1,cam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0.0f))+cam.transform.forward*weapon_Range);
    }
    public void WeaponSoundPlay(){ 
          gun_Sound.clip = shoot;      
          gun_Sound.Play();
    }
    public void WeaponSoundStop(){       
          gun_Sound.Stop();
    }
    public void ReloadSoundPlay(){
        gun_Sound.clip = reload;
        gun_Sound.Play();
    }
    public void BulletsDecrement(int bullets_Used){
        currentBullets -= bullets_Used;
        if(currentBullets <= 0){
              currentBullets = 0;
              WeaponSoundStop();
        }
    }
    public void StartShootAnimation(){
        anim.SetBool("Shoot",true);
    }
    public void StopShootAnimation(){
        anim.SetBool("Shoot",false);
    }
    public void StartReloadAnimation(){
        anim.SetBool("Reload",true);
    }
    public void StopReloadAnimation(){
        anim.SetBool("Reload",false);
    }
    public void startWalkAnimation(){
        anim.SetBool("Walk",true);
    }
    public void stopWalkAnimation(){
        anim.SetBool("Walk",false);
    }
    public void startIdleAnimation(){
        anim.SetBool("Idle",true);
    }
    public void stopIdleAnimation(){
        anim.SetBool("Idle",false);
    }

}
