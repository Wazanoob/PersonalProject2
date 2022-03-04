using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_clip;

    public int m_killCount = 0;
    public float m_timeSinceLastKill;

    private int m_playCount = 1;


    private void Start()
    {
        m_audioSource  = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        m_timeSinceLastKill += Time.deltaTime;

        if (m_timeSinceLastKill >= 1.2f && !m_audioSource.isPlaying)
        {
            m_killCount = 0;
            m_playCount = 1;
        }

        if (m_killCount == 0)
        {
            if (!m_audioSource.isPlaying)
            {
                m_audioSource.clip = m_clip[0];
                m_audioSource.Play();
            }
        }

        if (m_killCount >= 1 && m_playCount <= 1)
        {
            m_audioSource.clip = m_clip[1];
            if (!m_audioSource.isPlaying)
            {
                m_audioSource.Play();
                m_playCount += 1;
            }
        }

        if (m_playCount >= 2 && !m_audioSource.isPlaying)
        {
            m_audioSource.clip = m_clip[m_playCount];
            m_audioSource.Play();
            m_playCount += 1;
        }
    }
}
