using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] m_Cameras;
    //private static Camera[] sm_Cameras;
    private static int m_CurrentCamera = 0; 
    private float secondsLeft = 0;
    private bool m_Input = false;

    private void Start()
    {
        //for (int i = 0; i < m_Cameras.Length; i++)
           // sm_Cameras[i] = m_Cameras[i];

    }

    void Update()
    {
        cameraSwitch();
    }

    void cameraSwitch()
    {
        if (Input.GetKey(KeyCode.Q) && m_Input == false)
        {
            m_Input = true;
            if (m_CurrentCamera == 0)
                m_CurrentCamera = 5;
            if (m_CurrentCamera != 0)
                m_CurrentCamera -= 1;
            StartCoroutine(DelayedCamerasSwitch(0.5f));

        }
        if (Input.GetKey(KeyCode.E) && m_Input == false)
        {
            m_Input = true;
            if (m_CurrentCamera == 5)
                m_CurrentCamera = 0;
            if (m_CurrentCamera != 5)
                m_CurrentCamera += 1;
            StartCoroutine(DelayedCamerasSwitch(0.5f));
        }
    }

    private void CameraEnabler()
    {
        for (int i = 0; i < m_Cameras.Length; i++)
        {
            if (i != m_CurrentCamera)
                m_Cameras[i].gameObject.SetActive(false);
            if (i == m_CurrentCamera)
                m_Cameras[i].gameObject.SetActive(true);
        }
    }

    public IEnumerator DelayedCamerasSwitch(float p_TimeDelay)
    {
        
        secondsLeft = p_TimeDelay;
        do { yield return new WaitForSeconds(1); }
        while (--secondsLeft > 0);
        CameraEnabler();
        m_Input = false;
    }


}
