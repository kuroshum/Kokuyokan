using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerParameter : MonoBehaviour {
    public int Hp;
    public int MAXHP = 5;
    public int MINHP = 0;
    public int Key = 0;
    public int Bheal = 0;
    public int Sheal = 0;
    private int SwitchNum = 0;
    //public int WeponNum = 0;
    public string Item = null;
    public string ItemBuf = null;

    public bool ItemFlag;

    private StageSwitch ss;

    private SelectWepon sw;
    private MessageWindow mw;

    private Text myText;

    public bool ClearFlag = false;

    private AudioSource[] sources;

	// Use this for initialization
	void Start () {
        sources = gameObject.GetComponents<AudioSource>();
        GameObject canvas = GameObject.Find("Canvas");
        GameObject switch0 = GameObject.Find("Switch");
        sw = canvas.GetComponent<SelectWepon>();
        mw = canvas.GetComponent<MessageWindow>();
        if(SceneManager.GetActiveScene().name == "Hard Full") {
            ss = switch0.GetComponent<StageSwitch>();
        }
        myText = mw.pauseUI.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Hp <= 0) {
            SceneManager.LoadScene("GameOver");
        }
        if (ItemFlag) {
            switch (Item) {
                case null:
                    myText.text = "な に も 入 っ て な か っ た";
                    break;
                case "Key":
                    sources[5].Play();
                    Key = 1;
                    myText.text = "      鍵 を 手 に 入 れ た";
                    break;
                case "Big":
                    Bheal++;
                    myText.text = "回 復 薬 (大) を 手 に 入 れ た";
                    break;
                case "Small":
                    Sheal++;
                    myText.text = "回 復 薬 (小) を 手 に 入 れ た";
                    break;
                case "Wepon":
                    sw.WeponLevel++;
                    if(sw.WeponLevel == 2) myText.text = "ハ ン ド ガ ン を 手 に 入 れ た";
                    if(sw.WeponLevel == 3) myText.text = "フ ラ イ パ ン を 手 に 入 れ た\n\n敵 の 飛 び 道 具 は 効 か な く\nな っ た";
                    Debug.Log(sw.WeponLevel);
                    break;
                case "Switch":
                    SwitchNum++;
                    if (SwitchNum == 1) {
                        myText.text = "ス イ ッ チ が 押 さ れ た\n\n最 初 の 部 屋 の 左 の 壁\n           が 消 え た !";
                        ss.DeleteWall_Room1(SwitchNum);
                    }
                    if (SwitchNum == 2) {
                        myText.text = "ス イ ッ チ が 押 さ れ た\n\n最 初 の 部 屋 の 右 の 壁\n           が 消 え た !";
                        ss.DeleteWall_Room1(SwitchNum);
                    }
                    break;
            }
            ItemFlag = false;
            mw.MessageFlag = true;
            Item = null;
            ItemBuf = Item;
        }
    }

    private void OnTriggerEnter2D(Collider2D co) {
        if(co.gameObject.tag == "Exit") {
            if(Key > 0) {
                myText.text = "       鍵　を　使　っ　た\n\n           脱 出 成 功 ！！";
                mw.MessageFlag = true;
                ClearFlag = true;
            } else {
                myText.text = "鍵　を　持　っ　て　な　い\n\n　鍵　を　探　そ　う ！！";
                mw.MessageFlag = true;
            }
        }

        if(co.gameObject.tag == "Key") {
            if(Key == 0) {
                sources[5].Play();
                Key = 1;
                myText.text = "      鍵 を 手 に 入 れ た";
                mw.MessageFlag = true;
                co.gameObject.SetActive(false);
            }
        }
    }
}
