using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 *--------------------------------------------------------
 * プレイヤーの攻撃
 *      ナイフ攻撃                : Knife    → knife
 *      ハンドガン攻撃            : HundGun  → hundGun 
 *--------------------------------------------------------
 */
public class AtkPlayer : MonoBehaviour {
    private HundGun hundGun;
    private Knife knife;
    private FlyPan flypan;
    //サウンド代入する変数
    private AudioSource[] sources;
    private SelectWepon sw;
    public float dxBuf { set; private get; }
    public float dyBuf { set; private get; }


    void Start() {
        dxBuf = 0;
        dyBuf = -1;

        knife = GetComponent<Knife>();
        hundGun = GetComponent<HundGun>();
        flypan = GetComponent<FlyPan>(); 
        sources = gameObject.GetComponents<AudioSource>();
        GameObject canvas = GameObject.Find("Canvas");
        sw = canvas.GetComponent<SelectWepon>();
    }

    // Update is called once per frame
    void Update() {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        //Debug.Log("向いている方向：" + Vector3.forward);

        /*
         * 武器選択UIで選択した武器で攻撃する
         */ 
        if(SceneManager.GetActiveScene().name != "GameTitle") {
            switch (sw.WeponType) {
                case 0:
                    knife.Attack(dx, dy, dxBuf, dyBuf, sources[0]);
                    break;
                case 1:
                    hundGun.Attack(dx, dy, dxBuf, dyBuf, sources[1]);
                    break;
                case 2:
                    flypan.Attack(dx, dy, dxBuf, dyBuf, sources[2]);
                    break;
            }
        }
        
        

        
    }
}
