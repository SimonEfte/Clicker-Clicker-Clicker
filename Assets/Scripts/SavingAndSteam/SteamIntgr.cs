using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamIntgr : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool noSteamInt;

    public void Awake()
    {
        noSteamInt = false;
    }
    
    void Start()
    {
        if (noSteamInt == false)
        {
            try
            {
                if(DemoScript.isDemo == false) { Steamworks.SteamClient.Init(3187730); }
                else { Steamworks.SteamClient.Init(3270750); }
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    private void Update()
    {
        if (noSteamInt == false)
        {
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    private void OnApplicationQuit()
    {
        if (noSteamInt == false)
        {
            Steamworks.SteamClient.Shutdown();
        }
    }

}
