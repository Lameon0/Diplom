using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btntest : MonoBehaviour
{
    public GameObject PlayerObj;
    [SerializeField] public GameObject tpPoint;
    // Start is called before the first frame update
    public void Tp()
    {
        if (PlayerObj == null || tpPoint ==null)
        {
            return;
        }
        PlayerObj.transform.position = tpPoint.transform.position;
    }
}
