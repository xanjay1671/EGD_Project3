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

    public TMP_Text date;
    public int day = 0;

    public GameObject alertPrefab;
    public bool currentPopup = false;
    public GameObject myCanvas;

    public int latestPopup;
    private float nextMessageTime = 10f;

    private string[][] events;
    private float[][] infEventRewards;
    private float[][] authEventRewards;

    public float eventInfScaling;
    public float eventAuthScaling;

    public float passiveInfScaling;
    public float passiveAuthScaling;

    private float nextPopupTime;

    public string[] newDayMessages;
    public string[] MmocMessages;
    public string[] GurleyMessages;
    public string[] OtherMessages;

    public GameObject mask;
    public List<GameObject> activeMaskers;
    private int maskCount = 0;
    public float targetMasked = 1000f;
    public float maskTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //hardcoding shit
        events = new string[12][];
        infEventRewards = new float[12][];
        authEventRewards = new float[12][];
        events[0] = new string[] { "You’ve discovered one of your rivals secretly feeds cats during his days off.", "Blackmail him into dropping out of the competition", "Spread rumors about it among the student body" };
        infEventRewards[0] = new float[] { 5f, 6f};
        authEventRewards[0] = new float[] { 0f, 3f};
        events[1] = new string[] { "A freshman has asked you for directions.", "Lie and lead him around until they are late for class", "Ignore him and walk away", "Show him to his class and bribe him to spread bad rumors" };
        infEventRewards[1] = new float[] { 3f, 2f, 8f };
        authEventRewards[1] = new float[] { 1f, 0f, 1f };
        events[2] = new string[] { "RPI’s Hockey team is in the middle of a neck and neck game", "Cheer for the opposite team", "Steal the water coolers" };
        infEventRewards[2] = new float[] { 8f, 16f };
        authEventRewards[2] = new float[] { 1f, 10f};
        events[3] = new string[] { "Class has been canceled", "Take the time to spread your infamy", "Take the time to commit evil deeds", "Lay Low" };
        infEventRewards[3] = new float[] { 3f, 5f, 0f };
        authEventRewards[3] = new float[] { 0f, 3f, -5f };

        events[4] = new string[] { "You have consecutive upcoming exams this week!", "Time for some last minute late night cramming", "Well, I was going to fail them anyway…" };
        infEventRewards[4] = new float[] { -2f, 5f };
        authEventRewards[4] = new float[] { -1f, 5f };
        events[5] = new string[] { "You were caught by the professor while planning out your campaign!", "Abandon ship and re-strategize", "Confess to your crimes", "It's all or nothing!" };
        infEventRewards[5] = new float[] { -3f, -2f, 5f };
        authEventRewards[5] = new float[] { 2f, 0f, 10f };
        events[6] = new string[] { "Gurley has been snooping around lately!", "I’m innocent!", "I won't let her stop me!" };
        infEventRewards[6] = new float[] { 0f, 10f };
        authEventRewards[6] = new float[] { -10f, 10f };
        events[7] = new string[] { "Someone has been pulling down the posters you hung around campus!", "Catch the culprit", "Ignore them, they are just jealous", "I’ll just put up more than they take down" };
        infEventRewards[7] = new float[] { 0f, -3f, 2f};
        authEventRewards[7] = new float[] { -4f, 0f, 4f };

        events[8] = new string[] { "You decide to take a break and relax", "Hang out with friends", "Turn off your alarm and sleep in" };
        infEventRewards[8] = new float[] { 0f, 0f };
        authEventRewards[8] = new float[] { -1f, -1f };
        events[9] = new string[] { "A dog walks by", "Pet the dog", "Aggressively pet the dog", "Aggressively pet the dog until the sun sets" };
        infEventRewards[9] = new float[] { -2f, -5f -8f };
        authEventRewards[9] = new float[] { -2f, -5f, - 8f };

        events[10] = new string[] { "Classes seem to be getting longer and more boring", "Let's take a nap" };
        infEventRewards[10] = new float[] { 0f };
        authEventRewards[10] = new float[] { 0f };
        events[11] = new string[] { "Surprise visit from your counselor", "Just what are you doing with your life?"};
        infEventRewards[11] = new float[] { 0f };
        authEventRewards[11] = new float[] { 0f };

        inf = 0f;
        auth = 0f;
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

    void spawnRandomMask()
    {
        Vector3 newpos = new Vector3(Random.Range(-9.6f, 9.6f), Random.Range(-5.4f, 5.4f), 0);
        GameObject child = Instantiate(mask, newpos, transform.rotation);
        activeMaskers.Add(child);
    }

    void destroyRandomMask()
    {
        activeMaskers.RemoveAt(Random.Range(0, activeMaskers.Count - 1));
    }

    void maskify()
    {
        //if (timeElapsed > maskTime) {
        //    maskTime = timeElapsed + 1f;
        if (maskCount + 1 < targetMasked * inf)
        {
            spawnRandomMask();
            maskCount++;
        }
        if (maskCount - 1 > targetMasked * inf)
        {
            destroyRandomMask();
            maskCount--;
        }
        //}
    }

    void spawnPopup()
    {
        int Choice = Random.Range(0, events.Length - 1);
        string[] displayOptions = events[Choice];
        latestPopup = Choice;

        GameObject child = Instantiate(alertPrefab, new Vector3(-200, 0, 0), transform.rotation);
        child.transform.SetParent(myCanvas.transform, false);
        PopupManager popscript = child.GetComponent<PopupManager>();
        popscript.gm = this;
        popscript.populateOptions(displayOptions);

    }

    public void acceptResponse(int op)
    {
        inf += infEventRewards[latestPopup][op-1] * eventInfScaling;
        auth += authEventRewards[latestPopup][op-1] * eventAuthScaling;
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
        if (!currentPopup && timeElapsed > nextPopupTime)
        {
            if (Random.Range(0, 100) < 20)
            {
                spawnPopup();
                currentPopup = true;
                nextPopupTime = timeElapsed + 40;
            }
            else
            {
                nextPopupTime = timeElapsed + 10;
            }
        }
    }

    void passiveIncrease()
    {
        if (inf < 0) inf = 0;
        if (auth < 0) auth = 0;
        inf += .01f * passiveInfScaling;
        auth += .05f * inf * passiveAuthScaling;
    }

    void refreshDate()
    {
        if ((int)timeElapsed / 100 > day) {
            day++;
            string calcDate = "5/2" + day;
            date.text = calcDate;
        }
        if (timeElapsed > 1000) endGame();
    }

    void endGame()
    {
        if (auth > inf) //lose
        {

        } else //win
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            setBars();

            if (!currentPopup)
            {
                timeElapsed += Time.deltaTime * timeScale;
                passiveIncrease();
                scroll();
                //auth = timeElapsed / 50; //debug

                tryRandomMessage();
                tryPopup();
                maskify();
                refreshDate();
            }
        }

    }
}
