using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent m_agent;
    private Transform m_target;


    // Start is called before the first frame update
    void Start()
    {
        m_target = GameObject.FindWithTag("Player").transform;

        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(m_target.position, transform.position);

        m_agent.SetDestination(m_target.position);

        if (distance < m_agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (m_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
