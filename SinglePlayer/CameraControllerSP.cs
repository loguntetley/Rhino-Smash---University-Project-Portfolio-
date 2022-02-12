using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerSP : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.position;
    }

    void Update()
    {
        transform.position = Player.position + offset;
    }
}
