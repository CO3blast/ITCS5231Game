using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GUIHandler : MonoBehaviour
{   

    [SerializeField] public PlayerManager playMan;
    [SerializeField] public Transform camTrans;
    public TextMeshProUGUI ammoCounter;
    public Image crosshair;
    public static GUIHandler instance;
    public TextMeshProUGUI scoreCounter;
    public Image healthbar;
    public Image grappleBar;
    
    
    // Start is called before the first frame update
    private void Awake(){
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {   
        
        RaycastHit hit;
        if(Physics.Raycast(camTrans.position,camTrans.forward, out hit,50)){
            if(hit.transform.tag=="Enemy" || hit.transform.tag=="Trigger" || hit.transform.tag=="Item"){
                crosshair.color=Color.red;
            }
            else{
                crosshair.color=Color.green;
            }
        }
        else{
            crosshair.color=Color.white;
        }
        healthbar.fillAmount=playMan.health/100;
    }

}
