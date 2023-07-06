using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject Step1;
    public GameObject Step2;

    public void aStep1()
    {
        Step1.SetActive(false);
        Step2.SetActive(true);

    }

    
}
