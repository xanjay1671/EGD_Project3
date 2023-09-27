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
    public bool currentPopup = false;

    public int latestPopup;

    private float nextMessageTime;

    public string[] newDayMessages;
    public string[] MmocMessages;
    public string[] GurleyMessages;
    public string[] OtherMessages;

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
        scroller.transform.Translate(Vector2.left * Time.deltaTime * timeScale * scrollspeed);
    }

    void spawnPopup()
    {
        string[] test = { "bob", "ross", "bing" };

        GameObject child = Instantiate(alertPrefab, new Vector3(-200, 0, 0), transform.rotation);
        PopupManager popscript = child.GetComponent<PopupManager>();
        popscript.gm = this;
        popscript.populateOptions(test);

        child.transform.SetParent(gameObject.transform, false);
    }

    public void acceptResponse(int op)
    {
        currentPopup = false;
    }

    void tryRandomMessage()
    {
        if (timeElapsed > nextMessageTime)
        {
            if (Random.Range(0, 100) < 20)
            {
                sendNewScrollingMessage(OtherMessages[Random.Range(0, OtherMessages.Length - 1)]);
                nextMessageTime = timeElapsed + 40;
            }
            else
            {
                nextMessageTime = timeElapsed + 10;
            }
        }
    }

    void tryPopup()
    {
        if (!currentPopup && timeElapsed > 3f)
        {
            spawnPopup();
            currentPopup = true;
        }
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

            tryRandomMessage();
            tryPopup();

        }

    }
}
