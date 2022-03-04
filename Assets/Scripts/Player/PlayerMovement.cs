using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_controller;
    private float m_speed = 12f;

    // Start is called before the first frame update
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float m_horizontalMovement = Input.GetAxisRaw("Horizontal");
        float m_verticallMovement = Input.GetAxisRaw("Vertical");

        Vector3 m_move = transform.right * m_horizontalMovement + transform.forward * m_verticallMovement;

        m_controller.Move(m_move * m_speed * Time.deltaTime);
    }
}
