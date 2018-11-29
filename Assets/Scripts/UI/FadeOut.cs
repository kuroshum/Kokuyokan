using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ

    Image fadeImage;                //透明度を変更するパネルのイメージ

    public int StageNum { get; set; }

    void Start() {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    void Update() {
        if (isFadeOut) {
            StartFadeOut();
        }
    }

    void StartFadeOut() {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1) {             // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
            switch (StageNum) {
                case 0:
                    SceneManager.LoadScene("Tutorial Full");
                    break;
                case 1:
                    SceneManager.LoadScene("Normal Full");
                    break;
                case 2:
                    SceneManager.LoadScene("Hard Full");
                    break;
            }
            
        }
        
    }

    void SetAlpha() {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
