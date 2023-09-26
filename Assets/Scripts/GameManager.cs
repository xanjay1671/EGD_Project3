using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeElapsed = 0;
    public bool paused = false;
    public float timeScale = 1f;

    public float inf;
    public float auth;

    public GameObject infBar;
    public GameObject authBar;

    public GameObject scroller;
    public TMP_Text scrollingContent;
    public Vector3 scrollerHome;
    public float scrollspeed;

    public GameObject alertPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void setBars()
    {
        infBar.GetComponent<Slider>().value = inf;
        authBar.GetComponent<Slider>().value = auth;
    }

    void sendNewScrollingMessage(string message)
    {
        scrollingContent.text = message;
        scroller.transform.position = scrollerHome;
    }

    // Update is called once per frame
    void scroll()
    {
        scroller.transform.Translate(Vector2.left * Time.deltaTime * scrollspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timeElapsed += Time.deltaTime * timeScale;
            setBars();
            scroll();
            auth = timeElapsed / 50; //debug
        }

    }
}
