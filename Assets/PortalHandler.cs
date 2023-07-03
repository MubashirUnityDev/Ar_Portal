using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] Posters;
    public GameObject IndicatorObject;

    public void SetPosters(bool b)
    {
        foreach(GameObject g in Posters)
        {
            g.SetActive(b);
        }
    }

    

}
