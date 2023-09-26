using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DateUiController : MonoBehaviour
{
    public Button pauseButton, speedButton;

    public bool paused = true;
    public bool sped = false;

    public Sprite pauseSprite, playSprite, fastfowardSprite;

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(PausePlayTask);
        speedButton.onClick.AddListener(FastFowardTask);
    }

    void PausePlayTask()
    {
        if (paused)
        {
            paused = false;
            pauseButton.image.sprite = pauseSprite;
        } else
        {
            paused = true;
            pauseButton.image.sprite = playSprite;
        }
        gm.paused = paused;
    }

    void FastFowardTask()
    {
        if (sped)
        {
            sped = false;
            speedButton.image.color = Color.white;
            gm.timeScale = 1f;
        }
        else
        {
            sped = true;
            speedButton.image.color = Color.gray;
            gm.timeScale = 3f;

        }
    }

}
