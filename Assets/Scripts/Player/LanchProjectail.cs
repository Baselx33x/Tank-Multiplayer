using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LanchProjectail : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] InputReader m_InputReader;
    [SerializeField] GameObject m_ServerProjectilePrefab;
    [SerializeField] GameObject m_ClientProjectilePrefab;
    [SerializeField] Transform m_FirePoint;


    [Header("Settings")]
    [SerializeField] float m_ProjectileSpeed = 10f;
    [SerializeField] float m_FireRate = 0.5f;
    private float m_NextFireTime = 0f;

    private bool m_EnableFire = false;



    private void Update()
    {
            if (!IsOwner) return;
            if (!m_EnableFire) return;

        if (Time.time >= m_NextFireTime )
        {

            SpawnBulletServerRpc(m_FirePoint.position, m_FirePoint.up);
            SpawnDummyBullet(m_FirePoint.position, m_FirePoint.up);


            m_NextFireTime = Time.time + ( 1 / m_FireRate) ;
        }

      
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner) return;

        m_InputReader.onPrimaryFire += OnFire;
    }


    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        if (!IsOwner) return;

        m_InputReader.onPrimaryFire -= OnFire;
    }

    private void OnFire(bool IsFiring)
    {
        
        m_EnableFire = IsFiring;
        Debug.Log( "Firing !!" + m_EnableFire.ToString() );
    }


    private void SpawnDummyBullet(Vector3 spawnPosition , Vector3 direction)
    {

        GameObject bullet = Instantiate(m_ClientProjectilePrefab, spawnPosition, Quaternion.identity);
        bullet.transform.up = direction;

    }


    [ServerRpc]
    private void SpawnBulletServerRpc(Vector3 spawnPosition, Vector3 direction)
    {

        GameObject bullet = Instantiate(m_ServerProjectilePrefab, spawnPosition, Quaternion.identity);
        bullet.transform.up = direction;



        SpawnBulletClientRpc(spawnPosition, direction);

    }

    [ClientRpc]
    private void SpawnBulletClientRpc(Vector3 spawnPosition, Vector3 direction)
    {
        if (IsOwner) return;  

        SpawnDummyBullet(spawnPosition, direction); 

    }
}
