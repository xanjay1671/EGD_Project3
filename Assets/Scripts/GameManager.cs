using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeElapsed = 0;
    public bool paused = false;

    public GameObject alertPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) timeElapsed += Time.deltaTime;
    }
}
