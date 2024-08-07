using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        
        Debug.Log(collision.gameObject.name);


        Destroy(gameObject);
    }
}
