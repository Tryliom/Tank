using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            KillBullet();
        }
        else
        {
            Target target = other.GetComponent<Target>();
            
            if (target != null)
            {
                target.DestroySelf();
                KillBullet();
            }
        }
    }
    
    void Update()
    {
        if (transform.position.y < -10)
        {
            KillBullet();
        }
    }

    private void KillBullet()
    {
        Destroy(gameObject);
    }
}
