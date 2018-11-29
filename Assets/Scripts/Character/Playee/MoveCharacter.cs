using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCharacter : MonoBehaviour {

    private int idX = Animator.StringToHash("x"), idY = Animator.StringToHash("y");
    private Animator animator = null;
    /*
     * 座標管理のスクリプト 
     */
    private CoordinateManage cm;
    /*
     * ダメージエフェクトのスクリプト
     */
    private Damage dmg;
    /*
     * プレイヤーの座標
     */
    private Vector3 PlayerPos;
    /*
     * プレイヤーのスプライト
     */
    private SpriteRenderer PlayerSprite;
    /*
     * プレイヤーの1フレームの移動
     */
    private float Frame = 2f;

    //private int Hp = 5;

    private bool SwitchCo; 
    private bool ItemCo;
    private string CoName;

    private samplestruct st;
    private NormalStruct ns;
    private HardStruct hs;
    private PlayerParameter pp;
    private GameObject cam;
    private MessageWindow mw;
    private StageWall sw;
    private StageSwitch ss;

    private int scene;

    private GameObject SwitchObj;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        dmg = GetComponent<Damage>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        cam = Camera.main.gameObject;
        //st = cam.GetComponent<samplestruct>();
        switch (SceneManager.GetActiveScene().name) {
            case "Tutorial Full":
                scene = 1;
                GameObject item0 = GameObject.Find("Items_Tutorial");
                st = item0.GetComponent<samplestruct>();
                break;
            case "Normal Full":
                scene = 2;
                Debug.Log("Normal Full");
                GameObject item1 = GameObject.Find("Items_Normal");
                ns = item1.GetComponent<NormalStruct>();
                break;
            case "Hard Full":
                scene = 3;
                GameObject item2 = GameObject.Find("Items_Hard");
                hs = item2.GetComponent<HardStruct>();
                sw = GameObject.Find("Room 2").GetComponent<StageWall>();
                ss = GameObject.Find("Switch").GetComponent<StageSwitch>();
                break;
        }
        /*
        GameObject item = GameObject.Find("Items");
        st = item.GetComponent<samplestruct>();
        */
        pp = GetComponent<PlayerParameter>();
        GameObject canvas = GameObject.Find("Canvas");
        mw = canvas.GetComponent<MessageWindow>();


        //Debug.Log(st.Items);
    }

    // Update is called once per frame
    void Update() {
        if (Mathf.Approximately(Time.timeScale, 0f)) {
            //return;
        }
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");
        //AnimatorStateInfo anim = animator.GetCurrentAnimatorStateInfo(1);
        if (dmg.KnockBackFlag) {
            //pp.Hp = dmg.PlayerKnockBack(pp.Hp);
            pp.Hp--;
            dmg.KnockBackFlag = false;

        }
        if (dmg.FlashFlag) {
            dmg.FlashFlag = dmg.DamageEffect(PlayerSprite);
        }

        Move(dx, dy);

        /*
         * スイッチを調べる
         */
        if (SwitchCo && Input.GetButtonDown("Triangle") && !mw.PauseFlag) {
            pp.ItemFlag = true;
            pp.Item = "Switch";
            if (CoName == "Switch0") {
                ss.DeleteBoard();
                SwitchObj.SetActive(false);
            }else if(CoName == "Switch1") {
                SwitchObj.SetActive(false);
            }
            SwitchCo = false;
        }
        /*
         * アイテムを調べる
         */
        if (ItemCo && Input.GetButtonDown("Triangle") && !mw.PauseFlag) {
            pp.ItemFlag = true;
            //Debug.Log(st.ItemObj.Length);
            switch (scene) {
                case 1:
                    for (int i = 0; i < st.ItemObj.Length; i++) {
                        if (st.Items[i].Pos == CoName) {
                            Debug.Log(st.Items[i].Name);
                            pp.Item = st.Items[i].Name;
                            st.Items[i].Name = null;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < ns.ItemObj.Length; i++) {
                        if (ns.Items[i].ObjName == CoName) {
                            Debug.Log(ns.Items[i].ItemName);
                            pp.Item = ns.Items[i].ItemName;
                            ns.Items[i].ItemName = null;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < hs.ItemObj.Length; i++) {
                        if (hs.Items[i].ObjName == CoName) {
                            if(12 <= i && i <= 19) {
                                sw.DeleteWall_Room2();
                            }
                            if(20 <= i & i <= 29) {
                                sw.DeleteWall_Room3();
                            }
                            Debug.Log(hs.Items[i].ItemName);
                            pp.Item = hs.Items[i].ItemName;
                            hs.Items[i].ItemName = null;
                        }
                    }
                    break;
            }
        }
    }

    /*
     * プレイヤーの移動処理
     */
    public void Move(float x, float y) {
        if (Mathf.FloorToInt(x) == 0 && Mathf.FloorToInt(y) == 0) {
            animator.speed = 0.0f;

        } else {
            animator.speed = 1.0f;
            animator.SetFloat(idX, x);
            animator.SetFloat(idY, y);
            // Debug.Log("dxbuf0 : " + x + " dyBuf0 : " + y);

            transform.localPosition += new Vector3(x, y) * Frame * Time.unscaledDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D co) {
        if (co.gameObject.tag == "Item") {
            ItemCo = true;
            CoName = co.gameObject.name;
        }

        if(co.gameObject.tag == "Switch") {
            SwitchCo = true;
            CoName = co.gameObject.name;
            SwitchObj = co.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D co) {
        if (co.gameObject.tag == "Item") {
            ItemCo = false;
        }

        if(co.gameObject.tag == "Switch") {
            SwitchCo = false;
            CoName = co.gameObject.name;
        }
    }
}
