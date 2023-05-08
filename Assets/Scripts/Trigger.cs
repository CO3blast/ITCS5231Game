using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{   
    [SerializeField] public DoorTrigger target;
    public Material activeMat;
    private Material baseMat;
    public bool active;
    public string activateMethod;
    [Header("Timer")]
    public float maxTime;
    public string  deactivateMethod;
    private MeshRenderer mesh;
    // Start is called before the first frame update
    private void Awake(){
        target=transform.parent.gameObject.GetComponent<DoorTrigger>();
        baseMat=GetComponent<MeshRenderer>().material;
    }
    public void Activate(){
        if(!active){
        
        target.Invoke(activateMethod,0f);
        StartCoroutine(Timer(maxTime));
        activateEffects();
        active=true;
        }
    }
    public void Deactivate(){
        active=false;
        deactivateEffects();
        target.Invoke(deactivateMethod,0f);
    }

    private void activateEffects(){
    
        transform.localPosition+=new Vector3(0.0f,0.0f,0.1f);
        GetComponent<MeshRenderer>().material=activeMat;
    }
    private void deactivateEffects(){
          
        transform.localPosition-=new Vector3(0.0f,0.0f,0.1f);
        GetComponent<MeshRenderer>().material=baseMat;
    }
    private IEnumerator Timer(float time){
        while(time>0){
            time-=Time.deltaTime;
            yield return null;
        }
        if(target && time<=0){
            Deactivate();
           
        }
        yield return 0;
        
    }
}
