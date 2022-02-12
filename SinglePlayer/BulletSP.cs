using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSP : MonoBehaviour
{
    [SerializeField] private AudioClip BulletHitAudio;
    [SerializeField] private GameObject BulletHitVFX;
    public int damage = 5;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void InitializeBullet(Vector3 originalDirection)
    {
        transform.forward = originalDirection;
        rigidbody.velocity = transform.forward * 18f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play3D(BulletHitAudio, transform.position);
        VFXManager.Instance.Play(BulletHitVFX, transform.position);
        Destroy(gameObject);
    }
}
