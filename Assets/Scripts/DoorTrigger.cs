using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DoorTrigger : MonoBehaviour
{   
    [SerializeField] public string scene;
    private Material portalMat;
    public bool open;
    public void Awake(){
        portalMat=GetComponent<MeshRenderer>().material;
    }
    public void Open(){
        portalMat.SetFloat("_RadialEffect",0f);
        StartCoroutine(LerpMat(0.0f,2.0f));
    }
    public void Close(){
        portalMat.SetFloat("_RadialEffect",2.0f);
        StartCoroutine(LerpMat(2.0f,0.0f));
    }
    public void Warp(){
        if(open){
        GameManager.instance.sceneTransfer(scene);
        }
    }
    private IEnumerator LerpMat(float a,float b){
        while(Mathf.Abs(b-a)-0.005f>0){
        a=Mathf.Lerp(a,b,0.1f);    
        portalMat.SetFloat("_RadialEffect",a);
        yield return null;
        }
        portalMat.SetFloat("_RadialEffect",b);
        open=!open;
        yield return 0;
    }
}
