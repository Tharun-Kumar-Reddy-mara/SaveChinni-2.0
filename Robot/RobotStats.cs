using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotStats : MonoBehaviour
{
   [SerializeField] private Image blood_Stats;
   private float actualHealth;
   [SerializeField] private GameObject head_Shot_Ui;

   private void Start()
   {
       actualHealth = GetComponent<HealthScript>().initialHealth;
   }
   private void Awake()
   {
       head_Shot_Ui.SetActive(false);
   }
   public void DisplayBloodStats(float health){
       health /= actualHealth;
       blood_Stats.fillAmount = health;
   }
   public void DisplayHeadShot(){
        if(!head_Shot_Ui.activeInHierarchy){
           StartCoroutine(HeadShot());
        }
   }
   private IEnumerator HeadShot(){
       head_Shot_Ui.SetActive(true);
       yield return new WaitForSeconds(0.25f);
       head_Shot_Ui.SetActive(false);
   }
}