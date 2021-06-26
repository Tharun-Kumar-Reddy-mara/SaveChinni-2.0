using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopScript : MonoBehaviour
{ 
    [SerializeField] private Sprite[] gunImages;
    [SerializeField] private Image[] lockImages;
    [SerializeField] private Image image_Holder;
    [SerializeField] private TextMeshProUGUI weapon_Name;
    [SerializeField] private TextMeshProUGUI bullets;
    [SerializeField] private TextMeshProUGUI reload_Speed;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI range;
    [SerializeField] private WeaponHandler[] weaponHandler;
    [SerializeField] private GameObject shop_Menu;
    [SerializeField] private TextMeshProUGUI robot_Killed_Count;
    private int index_To_Change_Gun;
    private bool enough_Kills = false;
    private bool selected_Weapon = false;
    private float weapon_Value;
    [SerializeField] private AudioSource buy_Audio;
    [SerializeField] private TextMeshProUGUI not_available_Text;
    [SerializeField] private string [] weapon_names;
    private bool locked;

    private void Awake()
    {   
        robot_Killed_Count.text = ((int)PlayerPrefs.GetFloat("RobotsKilled")).ToString();
        PlayerPrefs.SetString("WeaponCheck1",weapon_names[0]);
        gunDetails(PlayerPrefs.GetInt("WeaponIndex"));          
    }
    private void OnEnable()
    {
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 0) || (PlayerPrefs.GetString("WeaponCheck1") == weapon_names[index_To_Change_Gun])){
             lockImages[0].enabled = false;
        }
        if(((PlayerPrefs.GetFloat("RobotsKilled") >= 10 && PlayerPrefs.GetFloat("LevelsName") > 1)) || (PlayerPrefs.GetString("WeaponCheck2") == weapon_names[index_To_Change_Gun])){
             lockImages[1].enabled = false;     
        }
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 25 && PlayerPrefs.GetFloat("LevelsName") > 2) || (PlayerPrefs.GetString("WeaponCheck3") == weapon_names[index_To_Change_Gun])){
             lockImages[2].enabled = false;     
        }
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 50 && PlayerPrefs.GetFloat("LevelsName") > 4) || (PlayerPrefs.GetString("WeaponCheck4") == weapon_names[index_To_Change_Gun])){
             lockImages[3].enabled = false;     
        }
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 75 && PlayerPrefs.GetFloat("LevelsName") > 6) || (PlayerPrefs.GetString("WeaponCheck5") == weapon_names[index_To_Change_Gun])){
             lockImages[4].enabled = false;     
        }
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 100 && PlayerPrefs.GetFloat("LevelsName") > 8) || (PlayerPrefs.GetString("WeaponCheck6") == weapon_names[index_To_Change_Gun])){
             lockImages[5].enabled = false;     
        }
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 150 && PlayerPrefs.GetFloat("LevelsName") > 9) || (PlayerPrefs.GetString("WeaponCheck7") == weapon_names[index_To_Change_Gun])){
             lockImages[6].enabled = false;     
        }
    }

    private void gunDetails(int index){
        image_Holder.sprite = gunImages[index];
        weapon_Name.text = " ' "+ weaponHandler[index].name.ToString() + " ' ";
        bullets.text = "Bullets" + " : " + weaponHandler[index].maxBullets.ToString();
        reload_Speed.text = "Reload" + " : " + weaponHandler[index].maxAmmo.ToString();
        damage.text = "Damage" + " : " + weaponHandler[index].damage.ToString();
        range.text = "Range" + " : " + ((int)weaponHandler[index].weapon_Range).ToString();
        index_To_Change_Gun = index;
    }
    public void MashVirtual(){
        gunDetails(0);
        selected_Weapon = true;
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 0) || (PlayerPrefs.GetString("WeaponCheck1") == weapon_names[index_To_Change_Gun])){
             weapon_Value = 0f;
             enough_Kills = true;
             locked = false; 
        }
        else{
             enough_Kills = false;
             locked = true;
        }
        
    }
    public void GrimBrand(){
        gunDetails(1);
        selected_Weapon = true;
        if(((PlayerPrefs.GetFloat("RobotsKilled") >= 10 && PlayerPrefs.GetFloat("LevelsName") > 1)) || (PlayerPrefs.GetString("WeaponCheck2") == weapon_names[index_To_Change_Gun])){
            weapon_Value = 10f;
            enough_Kills = true;
            locked = false;
        }
        else{
             enough_Kills = false;
             locked = true;
             
        }
    }
    public void Mauler(){
        gunDetails(2);
        selected_Weapon = true;
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 25 && PlayerPrefs.GetFloat("LevelsName") > 2) || (PlayerPrefs.GetString("WeaponCheck3") == weapon_names[index_To_Change_Gun])){
             weapon_Value = 25f;
             enough_Kills = true;
             locked = false;
        }
        else{
             enough_Kills = false;
             locked = true;
        }
    }
    public void Marker(){
        gunDetails(3);
        selected_Weapon = true;
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 50 && PlayerPrefs.GetFloat("LevelsName") > 4) || (PlayerPrefs.GetString("WeaponCheck4") == weapon_names[index_To_Change_Gun])){
            weapon_Value = 50f;
            enough_Kills = true;
            locked = false;
        }
        else{
             enough_Kills = false;
             locked = true;
        }
    }
    public void Archtronic(){
        gunDetails(4);
        selected_Weapon = true;
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 75 && PlayerPrefs.GetFloat("LevelsName") > 6) || (PlayerPrefs.GetString("WeaponCheck5") == weapon_names[index_To_Change_Gun])){
             weapon_Value = 75f;
             enough_Kills = true;
             locked = false;
        }
        else{
            enough_Kills = false;
            locked = true;
        }
    }
    public void HellWailer(){
        gunDetails(5);
        selected_Weapon = true;
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 100 && PlayerPrefs.GetFloat("LevelsName") > 8) || (PlayerPrefs.GetString("WeaponCheck6") == weapon_names[index_To_Change_Gun])){
             weapon_Value = 100f;
             enough_Kills = true;
             locked = false;
        }
        else{
            enough_Kills = false;
            locked = true;
        }
    }
    public void FireSleet(){
        gunDetails(6);
        selected_Weapon = true;
        if((PlayerPrefs.GetFloat("RobotsKilled") >= 150 && PlayerPrefs.GetFloat("LevelsName") > 9) || (PlayerPrefs.GetString("WeaponCheck7") == weapon_names[index_To_Change_Gun])){
            enough_Kills = true;
            weapon_Value = 150f;
            locked = false;
        }
        else{
             enough_Kills = false;
             locked = true;
        }
    }
    public void Exit(){
        shop_Menu.SetActive(false);
    }
    public void OnPressBuy(){
        if(enough_Kills && selected_Weapon){
            //Button Sound 
            buy_Audio.Play();
            //checking the weapon bought or not
            int weapon_Name_Index = index_To_Change_Gun + 1;
            if(PlayerPrefs.GetString("WeaponCheck"+weapon_Name_Index) != weapon_names[index_To_Change_Gun]){
                PlayerPrefs.SetFloat("RobotsKilled",PlayerPrefs.GetFloat("RobotsKilled") - weapon_Value);
                //To decrease deaths from robot and stop decreasing if gun bought
                PlayerPrefs.SetString("WeaponCheck"+weapon_Name_Index,weapon_names[index_To_Change_Gun]);
                PlayerPrefs.SetInt("WeaponIndex",index_To_Change_Gun);
                not_available_Text.text = "Bought".ToString();        
            }else{
                PlayerPrefs.SetInt("WeaponIndex",index_To_Change_Gun);
                not_available_Text.text = "Selected".ToString(); 
            }

            enough_Kills = false; 
            robot_Killed_Count.text = ((int)PlayerPrefs.GetFloat("RobotsKilled")).ToString();
        }else if(!selected_Weapon){
            not_available_Text.text = "Select A Weapon".ToString();
        }
        else{
            int weapon_Name_Index = index_To_Change_Gun + 1;
            foreach (var item in weapon_names)
             {       
                if(PlayerPrefs.GetString("WeaponCheck"+weapon_Name_Index) == item){
                    //Check a value is locked or not
                    not_available_Text.text = "Already Bought".ToString();
                }else{
                    if(!enough_Kills && locked){
                        not_available_Text.text = "Can't Buy".ToString();
                    }
                }
             }
        
        }
    }
    public void OnUpBuy(){
        StartCoroutine(NotEnoughKillsText());
    }
    private IEnumerator NotEnoughKillsText(){

        yield return new WaitForSeconds(1.5f);
        not_available_Text.text = "".ToString();

    }
   

}
