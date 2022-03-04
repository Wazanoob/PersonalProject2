using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{

    private Transform m_cam;

    [Header("General Stats")]
    [SerializeField] float m_range = 50f;
    [SerializeField] float m_damage = 30f;
    [SerializeField] float m_timeBetweenShoot = 0.7f;
    [SerializeField] float m_reloadTime;
    [SerializeField] float m_inaccuracyDistance = 6f;
    [SerializeField] int m_maxAmmo = 60;
    [SerializeField] int m_bulletPerShot = 6;


    [Header("Rapide Fire")]
    [SerializeField] bool m_rapidFire = false;

    private bool m_readyToShoot = true;
    private int m_currentAmmo;



    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main.transform;

        m_currentAmmo = m_maxAmmo;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && m_readyToShoot && CanShoot())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        m_readyToShoot = false;

        for (int i = 0; i < m_bulletPerShot; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(m_cam.position, GetShootingDirection(), out hit, m_range))
            {
                Damageable m_damageable = hit.collider.GetComponent<Damageable>();
                if (m_damageable != null)
                {
                    m_damageable.TakeDamage(m_damage, hit.point, hit.normal);
                }
            }
        }

        Invoke("ResetShot", m_timeBetweenShoot);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(m_reloadTime);
        m_currentAmmo = m_maxAmmo;
    }

    private void ResetShot()
    {
        m_readyToShoot = true;
    }

    bool CanShoot()
    {
        bool m_enoughAmmo = m_currentAmmo > 0;
        return m_enoughAmmo;
    }

    Vector3 GetShootingDirection()
    {
        Vector3 m_targetPos = m_cam.position + m_cam.forward * m_range;
        m_targetPos = new Vector3(
            m_targetPos.x + Random.Range(-m_inaccuracyDistance, m_inaccuracyDistance),
            m_targetPos.y + Random.Range(-m_inaccuracyDistance, m_inaccuracyDistance),
            m_targetPos.z + Random.Range(-m_inaccuracyDistance, m_inaccuracyDistance)
            );

        Vector3 m_direction = m_targetPos - m_cam.position;
        return m_direction.normalized;
    }
}
