using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_Player, m_Mount;
    [SerializeField] private GameObject m_HostObject;
    PhotonView photonView;
    private Vector3 m_OffsetPosition;
    private Transform m_CameraTransform, m_Controller;


    private void Start()
    {
        //m_Controller = m_ViewPoint;
        photonView = m_HostObject.GetComponentInParent<PhotonView>();
        SwitchController();
        m_OffsetPosition = transform.position - m_Controller.position;
        m_CameraTransform = gameObject.transform;
    }

    private void Update()
    {
        SwitchController();
        if (!photonView.IsMine)
            return;
        transform.position = m_Controller.position + m_OffsetPosition;
        TurnCameraLeft();
        TurnCameraRight();
    }

    private void TurnCameraLeft() 
    {
        if (Input.GetKey(KeyCode.Q))
        {
            m_CameraTransform.RotateAround(m_Controller.transform.position, new Vector3(0, 1, 0), -0.5f);
        }
    }

    private void TurnCameraRight() 
    {
        if (Input.GetKey(KeyCode.E))
        {
            m_CameraTransform.RotateAround(m_Controller.transform.position, new Vector3(0, 1, 0), 0.5f);
        }
    }

    private void SwitchController()
    {
        if (m_Player.gameObject.active == true)
            m_Controller = m_Player;
        else
            m_Controller = m_Mount;
    }

}
