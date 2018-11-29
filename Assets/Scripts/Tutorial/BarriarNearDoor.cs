using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarriarNearDoor : MonoBehaviour {
    [SerializeField] private GameObject window;
    [SerializeField] private string[] text = new string[2];
    private MessageWindow mw;
    private Text myText;

    [SerializeField] private GameObject box;
    [SerializeField] private GameObject enemyBarriar;

    public static int shelfCounter;

    // Use this for initialization
    void Start()
    {
        mw = window.GetComponent<MessageWindow>();
        myText = mw.pauseUI.GetComponentInChildren<Text>();
        shelfCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (shelfCounter < 2)
            {
                myText.text = text[0];
                mw.MessageFlag = true;
            }
            else
            {
                myText.text = text[1];
                mw.MessageFlag = true;
                enemyBarriar.SetActive(false);
                box.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }
}
