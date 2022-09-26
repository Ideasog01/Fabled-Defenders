using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneManager : MonoBehaviour
{

    public void TeleportAdvanced()
    {
            SceneManager.LoadScene("Future_Prefab");
    }

    public void TeleportMedieval()
    {
            SceneManager.LoadScene("Medieval_Prefab");
    }

    public void TeleportAncient()
    {
            SceneManager.LoadScene("Ancient_Prefab");
    }
}
