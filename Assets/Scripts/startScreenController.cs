using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class startScreenController : MonoBehaviour
{
    public GameObject startScreenTools, introSceneTools, endSceneTools;

    public GameObject winText, loseText;

    private void Awake()
    {
        returnToStart();
    }

    public void introButtonPress()
    {
        startScreenTools.SetActive(false);

        endSceneTools.SetActive(false);

        introSceneTools.SetActive(true);
    }

    public void returnToStart()
    {
        if (sceneSwitcher.Instance.shouldShowEndScreen)
        {
            startScreenTools.SetActive(false);

            endSceneTools.SetActive(true);

            introSceneTools.SetActive(false);

            if (sceneSwitcher.Instance.gameWon)
            {
                winText.SetActive(true);
                loseText.SetActive(false);
            }
            else{
                winText.SetActive(false);
                loseText.SetActive(true);
            }
        }
        else
        {
            startScreenTools.SetActive(true);

            endSceneTools.SetActive(false);

            introSceneTools.SetActive(false);
        }
          
    }
}
