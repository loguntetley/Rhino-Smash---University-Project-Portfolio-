using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSystem : MonoBehaviour
{
    public Transform[] m_Panels;

    public enum e_Panels : int
    {
        LoginPanel = 0,
        SelectionPanel = 1,
        CreateRoomPanel = 2,
        InsideRoomPanel = 3,
        ListRoomPanel = 4,
        ChatPanel = 5
    }

    public void ActivatePanel(string p_PanelName)
    {

        DisablePanels();

        int arrayIndex = -1;
        foreach (var panel in m_Panels)
        {
            arrayIndex++;
            if (p_PanelName == m_Panels[arrayIndex].name)
            {
                m_Panels[arrayIndex].gameObject.SetActive(true);
                break;
            }
        }
    }

    private void DisablePanels()
    {
        foreach (var panel in m_Panels)
            panel.gameObject.SetActive(false);
    }

}
