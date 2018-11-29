using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {
    [SerializeField] private GameObject window;
    private MessageWindow mw;
    private Text myText;

    private string[] text = {
        "洋館に入ったら閉じ込められてしまった！",
        "鍵を探して脱出しよう！",
        "棚を探してみよう\n△ボタン",
        "移動方法:左スティック",
        "調べる:△ボタン"
    };

    private int textCounter;

    // Use this for initialization
    void Start () {
        mw = window.GetComponent<MessageWindow>();
        myText = mw.pauseUI.GetComponentInChildren<Text>();
        textCounter = 0;
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (textCounter < 5)
        {
            if (!window.activeSelf)
            {
                myText.text = text[textCounter];
                mw.MessageFlag = true;
                textCounter++;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
