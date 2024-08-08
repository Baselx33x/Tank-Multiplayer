using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerSideBullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
  
    
     
        if (collision.gameObject.name == "TrackBody")
        {
            Debug.Log("Bullet hit player");

            var Player = collision.gameObject;
            if (Player != null)
            {
                var Health = Player.GetComponentInParent<Health>();
                Health.TakeDamage(10f);

                Debug.Log("Player Health: " + Health.m_Health.Value);

            }

        }
    }
}
