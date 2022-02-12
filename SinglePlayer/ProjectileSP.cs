using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSP : MonoBehaviour
{
    [SerializeField] private float fireRate, nextFire = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectilePosition;
    [SerializeField] private AudioClip ShootingAudio;
    [SerializeField] private GameObject ShootingEffect;


    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(projectilePrefab, projectilePosition.position, Quaternion.identity);
            bullet.GetComponent<BulletSP>().InitializeBullet(transform.rotation * Vector3.forward);

            PlayProjectileEffects();
        }
    }

    private void PlayProjectileEffects()
    {
        AudioManager.Instance.Play3D(ShootingAudio, projectilePosition.position);
        VFXManager.Instance.Play(ShootingEffect.gameObject, projectilePosition.position);
    }
}
