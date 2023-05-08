using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class PowerupScript : MonoBehaviour
{
    public float health;
    public int ammoCount;
    public int score=20;
    public void Pickup(AbilitiesManager am, PlayerManager pm){
     //   foreach(var field in typeof(PowerupScript).GetFields()){
    //     try{
            
     //       }catch{
    //
    //    }
     //   }
    GameManager.instance.score+=score;
    pm.health+=health;
    Mathf.Clamp(pm.health,0,100);
    am.ammoCount+=ammoCount;
    Destroy(gameObject);
    }
}
