using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AudioClip BulletHitAudio;
    [SerializeField] private GameObject BulletHitVFX;
    public int damage = 5;
    private Rigidbody rigidbody;
    [HideInInspector] public Photon.Realtime.Player Owner;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void InitializeBullet(Vector3 originalDirection, Photon.Realtime.Player player)
    {
        transform.forward = originalDirection;
        rigidbody.velocity = transform.forward * 18f;
        Owner = player;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play3D(BulletHitAudio, transform.position);
        VFXManager.Instance.Play(BulletHitVFX, transform.position);
        Destroy(gameObject);
    }
}
