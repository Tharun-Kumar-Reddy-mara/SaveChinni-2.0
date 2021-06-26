//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject shop_Menu;
   [SerializeField] private GameObject video_Menu;
   [SerializeField] private VideoPlayer video;
   private void Awake()
   {     
        shop_Menu.SetActive(false);

        // Demo Video
        if(PlayerPrefs.GetFloat("LevelsName") < 1 ){
            video_Menu.SetActive(true);
            PlayerPrefs.SetFloat("LevelsName",1);
        }
        else{
            video.Stop();
            video.enabled = false;
            video_Menu.SetActive(false);
        }
   }
   private void Update()
   {
        if(Time.time > 38.5f){
             video.Stop();
             video.enabled = false;
             video_Menu.SetActive(false);          
        }
   }
   
   public void ExitGame(){
       #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
       #else
            Application.Quit();   
       #endif       
   }
   public void Levels(){
       SceneManager.LoadScene("Levels");
   }
   public void Shop(){
         shop_Menu.SetActive(true);
   }
   public void Name(){
        SceneManager.LoadScene("Name");
   }
   public void Reset(){
        PlayerPrefs.DeleteKey("LevelsName");
        PlayerPrefs.DeleteKey("Name");
        PlayerPrefs.DeleteKey("RobotsKilled");
        PlayerPrefs.DeleteKey("WeaponIndex");
        for(int i = 1 ; i < 11 ; i++){
          //For deleting stars data   
          PlayerPrefs.DeleteKey("Star"+i);
          //For deleting gun data to stop decrementing deaths from player
          if(i < 8){
             PlayerPrefs.DeleteKey("WeaponCheck"+i);
          }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
 
}
