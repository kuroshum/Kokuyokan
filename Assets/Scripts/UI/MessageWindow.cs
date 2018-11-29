using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour {

    //　ポーズした時に表示するUI
    //[SerializeField]
    public GameObject pauseUI;
    Text myText;
    public bool MessageFlag { get; set; }
    public bool PauseFlag { get; set; }

    private GameObject Player;
    private PlayerParameter pp;

    private AudioSource sources;


    void Start() {
        sources = gameObject.GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pp = Player.GetComponent<PlayerParameter>();
    }

    // Update is called once per frame
    void Update() {
        if (MessageFlag) {
            sources.Play();
            //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            //　ポーズUIが表示されてる時は停止
            if (pauseUI.activeSelf) {
                //Time.timeScale = 0f;
                PauseFlag = true;
                //　ポーズUIが表示されてなければ通常通り進行
            } else {
                //Time.timeScale = 1f;
                PauseFlag = false;
            }
            MessageFlag = false;
        }

        if (PauseFlag && (Input.GetButtonDown("Circle"))) {
            MessageFlag = true;
            if(pp.ClearFlag) {
                SceneManager.LoadScene("Game Clear");
            }
        }
    }
}
