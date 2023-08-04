using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> TutorialSteps;

    private void Awake()
    {
        if (TutorialSteps != null)
        {
            DisableAllSteps();
            
        }
    }
    public void SetStep(int StepNumber)
    {

        if(TutorialSteps!=null)
        {
            Debug.Log("Set Step Called : " + (StepNumber));
            DisableAllSteps();

            TutorialSteps[StepNumber - 1].SetActive(true);
            Debug.Log("Step Log: Enabling Step " + (StepNumber));

        }

        Debug.Log("Step3 is Currently : " + TutorialSteps[2].activeSelf);
    }

    public void DisableAllSteps()
    {
        foreach (GameObject g in TutorialSteps)
        {
            g.SetActive(false);
        }
    }



}
