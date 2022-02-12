using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUtilityFunctions : MonoBehaviour
{
    private static float secondsLeft = 0;
    public static void ActiveSwap(GameObject p_ActivatedObject, GameObject p_DeactivateObject)
    {
        p_DeactivateObject.SetActive(false);
        p_ActivatedObject.SetActive(true);
    }
    public static IEnumerator MountingCoolDown(GameObject p_PrimaryObject, GameObject p_SecondaryObject, float p_TimeDelay)
    {
        secondsLeft = p_TimeDelay;
        do { yield return new WaitForSeconds(1); }
        while (--secondsLeft > 0);
        ActiveSwap(p_PrimaryObject, p_SecondaryObject);
        PositionSync(p_SecondaryObject, p_PrimaryObject);

    }

    public static void PositionSync(GameObject p_HostObject, GameObject p_SubjectObject)
    {
        p_SubjectObject.transform.position = p_HostObject.transform.position; 
    }



}
