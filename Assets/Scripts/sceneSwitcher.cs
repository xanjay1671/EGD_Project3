using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneSwitcher : MonoBehaviour
{

    //Scene Management
    private string startScene = "startScene";
    private string infamyScene = "InfamyGameScene";

    public static sceneSwitcher Instance;

    public bool shouldShowEndScreen,gameWon;

    private void Awake()
    {
        //shouldShowEndScreen = false;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void loadStart()
    {
        loadScene(startScene);

        shouldShowEndScreen = false;
    }

    public void loadInfamy()
    {
        loadScene(infamyScene);
    }

    public void turnOffEndScreen()
    {
        shouldShowEndScreen = false;
    }

    public void loadEnd()
    {
        loadScene(startScene);

        shouldShowEndScreen = true;
    }

    void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
