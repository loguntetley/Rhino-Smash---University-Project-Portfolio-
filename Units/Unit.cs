using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour, IPunObservable
{
    public Slider HealthBar;
    [HideInInspector] public int m_Health;
    protected PhotonView photonView;

    protected virtual void InitUnit() { }

    protected virtual void TakeDamage(Bullet p_Bullet) { }

    protected virtual void DestroyUnit() { }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(m_Health);
        }
        else
        {
            m_Health = (int)stream.ReceiveNext();
            HealthBar.value = m_Health;
        }
    }



}
