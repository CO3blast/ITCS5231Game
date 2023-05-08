using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float range;
    public float fireRPM;
    [SerializeField] public Transform rayCastOrig;
    [SerializeField] public ParticleSystem partsys;
    [SerializeField] public AbilitiesManager am;
    private float fireRate;
    public bool shotReady=true;
    public bool playerGun;
    private LayerMask mask = (1 << 3) | (1 << 6);
    void Start()
    {
        partsys.GetComponent<Bullet>().damage=damage;
        fireRate=1/(fireRPM/60);
        //i.e. 120 bullets per min, 2 per second so wait 0.5 second between fires or 40 per min, 0.66 per second so wait 1.5 seconds
    }

    // Update is called once per frame
    public void DoShoot(){
        if(shotReady){
        StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot(){
        shotReady=false;
        partsys.Play();
        if(playerGun){
        am.ammoCount-=1;
        }
        yield return new WaitForSeconds(fireRate);
        shotReady=true;
    }
    
    
}
