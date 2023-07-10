using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Menu_Manager : MonoBehaviour
{
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

     string GetRateUsLink()
    {
        string platformName = Application.platform.ToString();
        string rateUsLink = null;

        if (platformName == "iOS")
        {
            rateUsLink = "itms-apps://itunes.apple.com/app/id<YOUR_APP_ID>?action=write-review";
        }
        else if (platformName == "Android")
        {
            rateUsLink = "https://play.google.com/store/apps/details?id=<YOUR_APP_ID>&review";
        }

        return rateUsLink;
    }

    public void RateUsURL()
    {
        if(GetRateUsLink()!=null)
        {
            OpenUrl(GetRateUsLink());
        }
    }

    
}
