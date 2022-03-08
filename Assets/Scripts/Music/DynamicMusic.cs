using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DynamicMusic : MonoBehaviour
{

    [SerializeField] private ShotGun m_shotGun;
    [SerializeField] private TextMeshProUGUI m_rapidFireText;

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_clip;

    public int m_killCount = 0;
    public float m_timeSinceLastKill;

    private int m_playCount = 1;

    private bool dicrease = false;
    private float m_rapidFirefloat = 0;

    private void Start()
    {
        m_audioSource  = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }


        m_timeSinceLastKill += Time.deltaTime;

        if (m_timeSinceLastKill >= 1.2f && !m_audioSource.isPlaying)
        {
            m_killCount = 0;
            m_playCount = 1;
        }

        if (m_killCount == 0)
        {
            if (!m_audioSource.isPlaying && m_audioSource.clip != m_clip[0] && m_audioSource.clip != m_clip[1])
            {
                m_audioSource.clip = m_clip[0];
                m_audioSource.Play();
            }
            else if (!m_audioSource.isPlaying && m_audioSource.clip == m_clip[0])
            {
                m_audioSource.clip = m_clip[1];
                m_audioSource.Play();
            }
            else if (!m_audioSource.isPlaying && m_audioSource.clip == m_clip[1])
            {
                m_audioSource.clip = m_clip[1];
                m_audioSource.Play();
            }
        }

        if (m_killCount >= 1 && m_playCount <= 1)
        {
            if (m_audioSource.clip == m_clip[0])
            {
                if (!m_audioSource.isPlaying)
                {
                    m_audioSource.clip = m_clip[2];
                    m_audioSource.Play();
                    m_playCount += 2;
                }
            }
            else
            {
                m_audioSource.clip = m_clip[2];
                m_playCount += 2;
            }

            if (!m_audioSource.isPlaying)
            {
                m_audioSource.Play();
            }
        }

        if (m_playCount == 13)
        {
            m_audioSource.clip = m_clip[m_playCount];
            m_audioSource.PlayOneShot(m_audioSource.clip);
            m_playCount += 1;

            m_shotGun.m_rapidFire = true;
        }
        else if (m_playCount == 14)
        {
            m_audioSource.clip = m_clip[m_playCount];
            m_audioSource.Play();
            m_playCount += 1;

        }
        else if (m_playCount > 1 && !m_audioSource.isPlaying)
        {
            m_audioSource.clip = m_clip[m_playCount];
            m_audioSource.Play();
            m_playCount += 1;
        }

        if (m_playCount == 15)
        {
            m_rapidFireText.alpha = m_rapidFirefloat;


            if (!dicrease)
            {
                m_rapidFirefloat += 10 * Time.deltaTime;

                m_rapidFirefloat = Mathf.Clamp(m_rapidFirefloat, 0, 1);
            }
            else if (dicrease)
            {
                m_rapidFirefloat -= 10 * Time.deltaTime;

                m_rapidFirefloat = Mathf.Clamp(m_rapidFirefloat, 0, 1);
            }

            if (m_rapidFirefloat == 0)
            {
                dicrease = false;
            }
            else if (m_rapidFirefloat == 1.0)
            {
                dicrease = true;
            }
        }else
        {
            m_rapidFireText.alpha = 0;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
