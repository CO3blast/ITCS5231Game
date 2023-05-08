using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Aim;
    [SerializeField] public  Rig aimRig;
    [SerializeField] public Gun gunScript;
    [SerializeField] private Transform head;
    public Transform lookObj = null;
    public Transform gunBarrel;
    public Transform grip;
    public Transform foregrip;
    public Transform gun;
    public Transform gunHoldPosition;
    public Transform RightHand;
    public Transform LeftHand;
    public Transform rightShoulder;
    public Transform LeftPalm;
    public float aimTurnSpeed;
    public Animator Animator;
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [Header("Engage")]
    public bool aggro;
    [SerializeField] public float engageRange;
    [SerializeField] public float aggroRange;
    private float distance;
    public float visionConeAngle;

    private Vector3 direction;
    public float health;
    // Start is called before the first frame update
    void Start()
    {   
        Animator = gameObject.GetComponent<Animator>();
        agent  = GetComponent<NavMeshAgent>();
        player=PlayerManager.instance.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        aggroAgent();
        if(aggro){
          
            Aim.position=Vector3.Lerp(Aim.position,player.position,0.01f);
            distance=Vector3.Distance(player.position,transform.position);
            Animator.SetFloat("playerDistance",distance);
            //gunHoldPosition.rotation=Quaternion.LookRotation(direction);
            moveEnemy();
            if(distance>aggroRange){
                deaggroAgent();
            }
        }
        else{
            Animator.SetBool("Aggro",false);
        }
    }
    public void Hurt(float damage){
        health-=damage;
        if(health<=0){
            Destroy(gameObject);
            GameManager.instance.score+=50;
        }
    }
    private void moveEnemy(){
        if(distance>engageRange){
            agent.isStopped=false;
            agent.destination=player.position;
        }
        if(distance<=engageRange){
            agent.isStopped=true;
            direction=(player.position-transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), Time.deltaTime *aimTurnSpeed);
            gunScript.DoShoot();
        }
    }
    private void aggroAgent(){
        RaycastHit hit;
        if(player){
            if(Vector3.Angle(player.position-head.position,head.forward)<=visionConeAngle){
                if(Physics.Raycast(head.position,(player.position-head.position).normalized,out hit,aggroRange)){
                    if(hit.transform.tag=="Player"){
                        aggro=true;
                        lookObj=player;
                        Animator.SetBool("Aggro",true);
                        aimRig.weight=Mathf.Lerp(0f,1f,1f);
                    }
                }
            }
        }
    }
    private void deaggroAgent(){
        aggro=false;
        Animator.SetBool("Aggro",false);
        aimRig.weight=Mathf.Lerp(1f,0f,1f);
        lookObj=null;
        agent.destination=transform.position;
    }
    void OnAnimatorIK(){
        if(Animator){
                   // Animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                   // Animator.SetIKPosition(AvatarIKGoal.RightHand,grip.position);
                   // Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                    //Animator.SetIKPosition(AvatarIKGoal.LeftHand,foregrip.position);
            if(aggro){
                if(lookObj!=null){
                    Animator.SetLookAtWeight(1);
                    Animator.SetLookAtPosition(player.position);
   
                  //  Animator.SetBoneLocalRotation(HumanBodyBones.RightHand,head.rotation);
                   
        
                }
            }
        }
    }
}
