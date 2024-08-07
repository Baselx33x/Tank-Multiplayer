using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{


    [SerializeField] float m_LifeTime = 4f;
    [SerializeField] float m_BulletSpeed = 10f;

    void Start()
    {
        Destroy(gameObject, m_LifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * m_BulletSpeed * Time.deltaTime);
    }

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

            Destroy(gameObject);
    }
}
