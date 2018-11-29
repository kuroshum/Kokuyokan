using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

    /*
     * -------------------------------------- 
     * KnifeEffect
     *      0 : Up
     *      1 : RIght
     *      2 : Left
     *      3 : Down
     *--------------------------------------
     */
    [SerializeField]
    private GameObject[] KnifeEffect = new GameObject[4];
    private float dxBuf = 0;
    private float dyBuf = -1;

    private AtkPlayer atk;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < KnifeEffect.Length; i++) {
            KnifeEffect[i].SetActive(false);
        }
        atk = GetComponent<AtkPlayer>();
    }

    /*
    *-----------------------------------------------------------------------
    * ナイフ攻撃-
    *      dx       : プレイヤーの向き(左右)
    *      dy       : プレイヤーの向き(上下)
    *      dxbuf   : xの値を保存(xの値は何も押していないときは0になるため)
    *      dybuf   : yの値を保存(yの値は何も押していないときは0になるため)
    *-----------------------------------------------------------------------
    */
    public void Attack(float dx, float dy, float dxbuf, float dybuf, AudioSource KnifeSound) {

        /*
        if (dx != 0) {
            dxBuf = dx;
            //dyBuf = 0;
        }
        if (dy != 0) {
            //dxBuf = 0;
            dyBuf = dy;
        }
        //Debug.Log("dxbuf0 : " + dxBuf + " dyBuf0 : " + dyBuf);

        if (Mathf.Abs(dxBuf) >= Mathf.Abs(dyBuf)) {
            dyBuf = 0;
        } else {
            dxBuf = 0;
        }
        */

        this.dxBuf = dxbuf;
        this.dyBuf = dybuf;

        if((dx != 0 && (dx <= 0 || dx >= 1)) || dy != 0 && (dy <= 0 || dy >= 1)) {
            dxBuf = dx;
            dyBuf = dy;
        }

        //Debug.Log("dxbuf : " + dxBuf + " dyBuf : " + dyBuf);

        if (Mathf.Abs(dxBuf) >= Mathf.Abs(dyBuf)) {
            dyBuf = 0;
        } else {
            dxBuf = 0;
        }

        //Debug.Log("dxbuf : " + dxBuf + " dyBuf : " + dyBuf);
        //Debug.Log("dxbuf : " + dx + " dyBuf : " + dy);

        /*
         * Tキーを押したら向いている方向にナイフ攻撃エフェクトを一瞬だけ表示
         */
        //if (Input.GetKeyDown(KeyCode.Y)) {
        if (Input.GetButtonDown("Circle")) {
            //Debug.Log("dxBuf : " + dxBuf + " dyBuff : " + dyBuf);
            if (dxBuf > 0) {
                StartCoroutine("SetKnifeEffectCoroutine", 1);
            } else if (dxBuf < 0) {
                StartCoroutine("SetKnifeEffectCoroutine", 2);
            }
            if (dyBuf > 0) {
                StartCoroutine("SetKnifeEffectCoroutine", 0);
            } else if (dyBuf < 0) {
                StartCoroutine("SetKnifeEffectCoroutine", 3);
            }
            KnifeSound.Play();
        }

        atk.dxBuf = this.dxBuf;
        atk.dyBuf = this.dyBuf;
    }
    /*
     *----------------------------------------- -----------------------------
     * num : プレイヤーの向き情報
     * numの方向のナイフエフェクトを表示・それ以外のナイフエフェクトを非表示
     *----------------------------------------------------------------------- 
     */
    void SetKnifeEffect(int num) {
        for (int i = 0; i < KnifeEffect.Length; i++) {
            if (i == num) {
                KnifeEffect[i].SetActive(true);
            } else {
                KnifeEffect[i].SetActive(false);
            }
        }
    }
    /*
     * ナイフエフェクトを0.1秒だけ表示させて非表示にする
     */
    IEnumerator SetKnifeEffectCoroutine(int num) {
        SetKnifeEffect(num);
        yield return new WaitForSeconds(0.1f);
        KnifeEffect[num].SetActive(false);
    }
}
