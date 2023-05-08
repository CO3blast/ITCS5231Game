using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    public AbilitiesManager am;
    [SerializeField] public GameManager game;
    public static PlayerManager instance;
    public float health;
    public new Rigidbody rigidbody;
    public float jumpHeight;
    public bool isGrounded;
    public bool isGrappling;
    float horizInput=0f;
    float vertInput=0f;
    float jumpInput=0f;
    public Vector3 jumpDir=Vector3.zero;

    private void Awake(){
        am=GetComponent<AbilitiesManager>();
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        isGrappling=gameObject.GetComponent<AbilitiesManager>().isGrappling;
    }
    
    // Update is called once per frame
    private void FixedUpdate(){
    if(isGrounded){
     horizInput= Input.GetAxis("Horizontal");
     vertInput = Input.GetAxis("Vertical");
     jumpInput= Input.GetAxis("Jump")*jumpHeight;
    }
    else if(isGrappling){
     horizInput= Input.GetAxis("Horizontal")*1f;
     vertInput = Input.GetAxis("Vertical")*1f;
     jumpInput=0f;
     
    } else{
     horizInput= Input.GetAxis("Horizontal")*0.1f;
     vertInput = Input.GetAxis("Vertical")*0.1f;
     jumpInput=0f;
    }
    rigidbody.AddForce(jumpDir*jumpInput, ForceMode.Impulse);
    rigidbody.AddRelativeForce(horizInput*1.3f,0,vertInput*1.3f, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision collision){
        if(collision.collider.tag=="traverse"){
            rigidbody.drag=6;
            isGrounded=true;
            jumpDir=Vector3.up;
        }
        if(collision.collider.tag=="jumpwall"){
            if(Vector3.Angle(collision.contacts[0].normal,Vector3.up)==0){
                jumpDir=Vector3.up;
                rigidbody.drag=6;

            }
            else{
                jumpDir=collision.contacts[0].normal*0.75f+Vector3.up*0.75f;
                rigidbody.drag=0;
            }
            isGrounded=true;
        }
        if(collision.collider.tag=="Respawn"){
            
            rigidbody.drag=6;
            isGrounded=true;
            jumpDir=Vector3.up;
            if(collision.collider.transform!=game.currentRespawnTrans){
            game.score+=100;
            game.currentRespawnTrans=collision.transform;
            }
            
        }

     }
    public void Hurt(float damage){
        health-=damage;
    }
     void OnCollisionExit(Collision collision){
        if(collision.collider.tag=="traverse"){
            rigidbody.drag=0;
            isGrounded=false;

        }
        if(collision.collider.tag=="jumpwall"){
            rigidbody.drag=0;
            isGrounded=false;
        }
        if(collision.collider.tag=="Respawn"){
            rigidbody.drag=0;
            isGrounded=false;
        }

     }
     void OnCollisionStay(Collision collision){
        if(collision.collider.tag=="traverse" || collision.collider.tag=="Respawn"){
            isGrounded=true;
            rigidbody.drag=6;

        }
         if(collision.collider.tag=="jumpwall"){
            if(Vector3.Angle(collision.contacts[0].normal,Vector3.up)==0){
                rigidbody.drag=6;
            }
            isGrounded=true;
        }
     }
     void OnTriggerEnter(Collider collision){
        if(collision.GetComponent<Collider>().tag=="Killbox"){
            health-=collision.gameObject.GetComponent<KillBox>().damage;
            rigidbody.velocity=Vector3.zero;
            if(!game.currentRespawnTrans){
            game.currentRespawnTrans=GameObject.Find("Respawn").transform;
        }
            gameObject.transform.position=game.currentRespawnTrans.position+Vector3.up;
        }
        if(collision.GetComponent<Collider>().tag=="Portal"){
            collision.GetComponent<DoorTrigger>().Warp();
        }
        if(collision.GetComponent<Collider>().tag=="Powerup"){
            collision.gameObject.GetComponent<PowerupScript>().Pickup(am,instance);
        }
     }


}
