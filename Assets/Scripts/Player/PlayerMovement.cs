using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    [Header("References")]

    [SerializeField] InputReader m_InputReader;
    [SerializeField] Transform m_TankBody;
    [SerializeField] Transform m_TankMuzzle;
    [SerializeField] Rigidbody2D m_Rb;
    //[SerializeField] GameObject m_Light;

    [Header("Movement")]

    [SerializeField] Vector2 m_MovementVector;
    [SerializeField] float m_MovementSpeed;
    [SerializeField] float m_TurningSpeed;

    private void OnMove(Vector2 InputVector)
    {
        m_MovementVector = InputVector;
    }



    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner) return;

        m_InputReader.onMove += OnMove;
    }

   
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        if (!IsOwner) return;

        m_InputReader.onMove -= OnMove;
    }
   


    private void Update()
    {
        if (!IsOwner) return;

        //if (m_MovementVector != Vector2.zero)
        //{
        //    m_TankBody.rotation = Quaternion.RotateTowards(m_TankBody.rotation, Quaternion.LookRotation(Vector3.forward, m_MovementVector), m_TurningSpeed * Time.deltaTime);
        //    m_Rb.velocity = m_MovementVector * m_MovementSpeed;
        //}

        BodyRoation();

        MuzzleRoation();



    }

    private void BodyRoation()
    {
        float zRotation = m_MovementVector.x * (-m_TurningSpeed * Time.deltaTime);
        m_TankBody.Rotate(0, 0, zRotation);
    }

    private void MuzzleRoation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = m_TankMuzzle.position.z;
        Vector3 direction = mousePosition - m_TankMuzzle.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust by -90 degrees for upward pointing
        m_TankMuzzle.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        m_Rb.velocity = (Vector2)m_TankBody.up * (m_MovementVector.y * m_MovementSpeed);

    }
}
