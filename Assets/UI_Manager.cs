using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] List<GameObject> Menus;


    public void SetObjectActive(GameObject ToActive)
    {
        foreach( GameObject g in Menus)
        {
            g.SetActive(false);
        }
        ToActive.SetActive(true);
    }
}
