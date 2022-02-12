using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Unit
{
    [SerializeField] private GameObject m_Player;

    private void Start()
    {
        m_Health = 200;
        photonView = gameObject.GetComponentInParent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetKey(KeyCode.Z) && m_Player.active == false)
            photonView.RPC("Unmount", RpcTarget.AllViaServer);

        if (m_Health <= 0)
            photonView.RPC("DestroyUnit", RpcTarget.AllViaServer);
    }

    [PunRPC]
    private void Unmount()
    { 
        StartCoroutine(UnitUtilityFunctions.MountingCoolDown(m_Player, gameObject, 3.0f));
    }

    [PunRPC]
    protected override void DestroyUnit()
    {
            UnitUtilityFunctions.ActiveSwap(m_Player, gameObject);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            var bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet);
        }
    }


}
