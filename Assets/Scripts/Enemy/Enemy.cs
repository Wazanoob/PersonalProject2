using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent m_agent;

    private Vector3 m_randomPosition;

    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
        m_agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(m_randomPosition, transform.position);

        m_agent.SetDestination(m_randomPosition);

        if (distance < m_agent.stoppingDistance)
        {
            RandomPosition();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (m_randomPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void RandomPosition()
    {
        float x = Random.Range(-28, 28);
        float z = Random.Range(-28, 28);

        m_randomPosition = new Vector3(x, 0, z);
    }
}
