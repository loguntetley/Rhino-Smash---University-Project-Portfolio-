using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    [SerializeField] GameObject m_PlayerMount;
    Projectile m_Projectile;

    private void Start()
    {
        photonView = gameObject.GetComponentInParent<PhotonView>();
        m_Projectile = gameObject.GetComponent<Projectile>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetKey(KeyCode.Space))
            photonView.RPC("FireProjectile", RpcTarget.AllViaServer);

        if (Input.GetKey(KeyCode.Z))
            photonView.RPC("Mount", RpcTarget.AllViaServer);
    }

    [PunRPC]
    private void Mount()
    {

            StartCoroutine(UnitUtilityFunctions.MountingCoolDown(m_PlayerMount, gameObject, 3.0f));
    }

    [PunRPC]
    public void FireProjectile()
    {
        
            m_Projectile.Fire();
    }

    protected override void TakeDamage(Bullet p_Bullet)
    {
        m_Health -= p_Bullet.damage;
        HealthBar.value = m_Health;
        if (m_Health <= 0)
        {
            DestroyUnit();
            p_Bullet.Owner.AddScore(1);
        }
    }

    protected override void DestroyUnit()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            var bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet);
        }
    }

}
