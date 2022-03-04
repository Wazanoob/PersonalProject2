using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private DynamicMusic m_dynamicMusic;

    [SerializeField] float m_maxHealth = 100f;
    float m_currentHealth;

    public bool m_isAlive = true;

    [SerializeField] GameObject m_hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        m_dynamicMusic = GameObject.FindWithTag("GameManager").GetComponent<DynamicMusic>();
        m_currentHealth = m_maxHealth;
    }

    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        Instantiate(m_hitEffect, hitPos, Quaternion.LookRotation(hitNormal));

        m_currentHealth -= damage;

        if (m_currentHealth <= 0 && m_isAlive)
        {
            Die();
        }
    }

    private void Die()
    {
        m_dynamicMusic.m_killCount++;
        m_dynamicMusic.m_timeSinceLastKill = 0;
        m_isAlive = false;
        Destroy(gameObject);
    }
}
