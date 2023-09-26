using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeElapsed = 0;
    public bool paused = false;
    public float inf;
    public float auth;

    public GameObject infBar;
    public GameObject authBar;

    public GameObject alertPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void setBars()
    {
        infBar.GetComponent<Slider>().value = inf;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) timeElapsed += Time.deltaTime;
        inf = timeElapsed / 50;

        setBars();
    }
}
