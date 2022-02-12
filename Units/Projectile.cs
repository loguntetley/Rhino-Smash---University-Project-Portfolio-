using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float fireRate, nextFire = 1f;
    public GameObject projectilePrefab;
    [SerializeField] private Transform projectilePosition;
    [SerializeField] private AudioClip ShootingAudio;
    [SerializeField] private GameObject ShootingEffect, m_HostObject;
    photonView photonView;

    private void Start()
    {
        photonView = m_HostObject.GetComponent<photonView>();
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(projectilePrefab, projectilePosition.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().InitializeBullet(transform.rotation * Vector3.forward, m_HostObject.GetComponent<PhotonView>().Owner);

            PlayProjectileEffects();
        }
    }

    private void PlayProjectileEffects()
    {
        AudioManager.Instance.Play3D(ShootingAudio, projectilePosition.position);
        VFXManager.Instance.Play(ShootingEffect.gameObject, projectilePosition.position);
    }
}
