using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{   
    public new Rigidbody rigidbody;
    [SerializeField] private Transform grappleBarrel;
    [SerializeField] private Camera playerCam;
    [Header("Grappling")]
    public float grappleLength;
    public float grapplePullForce;
    public float grappleCooldown;
    private float grappleAlarm;
    private Vector3 grapplePoint;
    public LineRenderer grappleCable;
    public bool isGrappling;
    public float minCableDist;
    public float maxCableDist;
    private bool canGrappleAgain;
    private SpringJoint swingJoint;
    public LayerMask mask = ~(1 << 3);
    [SerializeField] public GameObject hook;
    [Header("Shooting")]
    [SerializeField] private Transform gunBarrel;
    [SerializeField] public GameObject gun;
    private Gun gunScript;
    [Header("GUI")]
    public int ammoCount;
    public GUIHandler GUI;
    
    
  
    // Start is called before the first frame update
    void Awake(){
        GUI.ammoCounter.text=ammoCount.ToString();
        grappleCable.enabled=false;
        gunScript=gun.GetComponent<Gun>();

    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        newGrapple();
        Shoot();
    }
    void castGrapple(){
        //gets point from which to grapple
        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out hit, grappleLength,mask)){
            grapplePoint= hit.point;
        }
    }
    void endGrapple(){
        canGrappleAgain=true;
        grapplePoint=Vector3.zero;
        isGrappling=false;
        grappleCable.enabled=false;
        grappleAlarm=grappleCooldown;
        GUI.grappleBar.fillAmount=0;
        hook.SetActive(true);
        Destroy(gameObject.GetComponent<SpringJoint>());
    }
    void newGrapple(){
        if(grappleAlarm>0){
         grappleAlarm-=Time.deltaTime;
         GUI.grappleBar.fillAmount=(grappleCooldown-grappleAlarm)/grappleCooldown;
         return;
        }
        if(Input.GetKey("q")){
            //perform grapple at point grapplePoint
            
            if(Input.GetKeyDown("q")){
                if(canGrappleAgain){
                    castGrapple();
            
                    canGrappleAgain=false;
                    newStartGrapple();
                }
            }
            //add force in direction of grapplePoint
            if(isGrappling){
            grappleCable.SetPosition(0, grappleBarrel.position);
            rigidbody.AddForce((grapplePullForce*(360-Vector3.Angle(playerCam.transform.forward,grapplePoint-playerCam.transform.position))/360)*((grapplePoint-playerCam.transform.position).normalized),ForceMode.Force);
            }
        } 
        else if(!Input.GetKey("q")){
            if(canGrappleAgain==false){
            endGrapple();
            }
        }
    }
    void newStartGrapple(){
        isGrappling=true;
        hook.SetActive(false);
        Destroy(gameObject.GetComponent<SpringJoint>());
        if(grapplePoint!=Vector3.zero){
        swingJoint=gameObject.AddComponent<SpringJoint>();
        swingJoint.autoConfigureConnectedAnchor=false;
        swingJoint.connectedAnchor=grapplePoint;
        float grappledistance=Vector3.Distance(grappleBarrel.position,grapplePoint);
        swingJoint.minDistance=grappledistance*minCableDist;
        swingJoint.maxDistance=grappledistance*maxCableDist;
        grappleCable.enabled=true;
        grappleCable.SetPosition(0, grappleBarrel.position);
        grappleCable.SetPosition(1,grapplePoint);
        }
    }
    void Shoot(){
        //particle projectile
        if(Input.GetButton("Fire1") && ammoCount>0){
            gunScript.DoShoot();
            GUI.ammoCounter.text=ammoCount.ToString();
        }
    }
}
