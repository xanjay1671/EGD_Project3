using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PopupManager : MonoBehaviour
{
    public Image popupSprite;
    public Button button1;
    public Button button2;
    public Button button3;

    public TMP_Text question, option1, option2, option3;

    public GameManager gm;

    public float scaleTime = .5f;


    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(() => respondWithOption(1));
        button2.onClick.AddListener(() => respondWithOption(2));
        button3.onClick.AddListener(() => respondWithOption(3));

        //transform.localScale = Vector3.zero;
        //Invoke("StartScaleup", 0f);
    }

    public void populateOptions(string[] dialogue)
    {
        question.text = dialogue[0];

        option1.text = dialogue[1];
        if (dialogue.Length <= 2)
        {
            button2.GetComponentInParent<GameObject>().SetActive(false);
        } else
        {
            option2.text = dialogue[2];
        }

        if (dialogue.Length <= 3)
        {
            button3.GetComponentInParent<GameObject>().SetActive(false);
        }
        else
        {
            option3.text = dialogue[3];
        }
    }

    void respondWithOption(int option)
    {
        gm.acceptResponse(option);
        Destroy(this.gameObject);
    }

    //public IEnumerator startScaleup()
    //{
    //    for (float i = 0; i < scaleTime; i += Time.deltaTime)
    //    {
    //        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, i / scaleTime);
    //        yield return null;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
