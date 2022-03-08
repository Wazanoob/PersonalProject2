using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;

    [SerializeField] private Transform[] m_spawnArea;

    private Collider[] m_collider;

    private int m_count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 20)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 m_randomPosition = RandomPosition();

        if (m_randomPosition != Vector3.zero)
        {
            Instantiate(m_enemyPrefab, m_randomPosition, Quaternion.identity);
        }else
        {
            SpawnEnemy();
        }
    }
    private Vector3 RandomPosition()
    {
        Transform m_randomArea = m_spawnArea[Random.Range(0, 4)];

        float m_areaScaleX = m_randomArea.lossyScale.x / 2;
        float m_areaScaleZ = m_randomArea.lossyScale.z / 2;

        Vector3 m_randomPos;
        m_randomPos.x = Random.Range(m_randomArea.position.x - m_areaScaleX, m_randomArea.position.x + m_areaScaleX);
        m_randomPos.z = Random.Range(m_randomArea.position.z - m_areaScaleZ, m_randomArea.position.z + m_areaScaleZ);
        m_randomPos.y = 0.5f;

        m_collider = Physics.OverlapSphere(m_randomPos, 0.4f);
        if (m_collider.Length == 0)
        {
            m_randomPos.y = 0f;
            return m_randomPos;
        }else
        {
            return Vector3.zero;
        }
    }
}
