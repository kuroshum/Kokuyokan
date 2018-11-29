using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barriar : MonoBehaviour {
    [SerializeField] private GameObject window;
    [SerializeField] private string text;
    private MessageWindow mw;
    private Text myText;

    // Use this for initialization
    void Start () {
        mw = window.GetComponent<MessageWindow>();
        myText = mw.pauseUI.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            myText.text = text;
            mw.MessageFlag = true;
        }
    }
}
