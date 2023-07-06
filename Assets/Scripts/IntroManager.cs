using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> ScreenList;
    

    private int CurrentScreenIndex;


    private void Start()
    {
        CurrentScreenIndex = -1;
        NextScreen();
    }
    public void NextScreen()
    {
        foreach ( GameObject g in ScreenList)
        {
            g.SetActive(false);
        }
        
        CurrentScreenIndex += 1;
        ScreenList[CurrentScreenIndex].SetActive(true);
    }
    
}
