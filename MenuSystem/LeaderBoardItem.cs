using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardItem : MonoBehaviour
{
    public Text m_Order;
    public Text m_Username;
    public Text m_Score;

    public void SetValues(int p_Order, string p_Username, int p_Score)
    {
        m_Order.text = p_Order.ToString() + ")";
        m_Username.text = p_Username;
        m_Score.text = p_Score.ToString();
    }
}
