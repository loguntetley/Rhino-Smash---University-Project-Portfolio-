using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float velocity = 5f;
    [SerializeField] GameObject m_HostObject, m_Target;
    PhotonView photonView;

    void Start()
    {
        rigidbody = m_Target.GetComponent<Rigidbody>();
        photonView = m_HostObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!photonView.IsMine)
            return;

        Move(rigidbody, velocity);

    }

    public void MoveDirection(Rigidbody rigidbody, float velocity)
    {
        rigidbody.MovePosition(rigidbody.position + transform.forward * velocity * Time.deltaTime);
    }

    private void SetRotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, verticalInput));
    }

    public void Move(Rigidbody rigidbody, float velocity)
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            return;

        SetRotation();
        MoveDirection(rigidbody, velocity);
    }

}
