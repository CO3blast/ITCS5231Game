using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public void OnParticleCollision(GameObject other){
        if(other.TryGetComponent(out Enemy enemy)){
            enemy.Hurt(damage);
        }
        if(other.TryGetComponent(out PlayerManager player)){
            player.Hurt(damage);
        }
        if(other.TryGetComponent(out Trigger trigger)){
            trigger.Activate();
        }
    }

}
