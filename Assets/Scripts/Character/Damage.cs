using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    /*
     * 攻撃を受けた際のプレイヤーの座標
     */
    private Vector3 FirstCharacterPos;
    /*
     * 座標管理のスクリプト 
     */ 
    private CoordinateManage cm;

    /*
     * 攻撃を受けた時の敵の方角
     */ 
    private int Axis;
    /*
     * ノックバックの距離
     */ 
    private int Distance = 5;
    /*
     * ノックバッグするかのフラグ
     */ 
    public bool KnockBackFlag { get; set; }
    /*
     * 攻撃を受けた後に点滅する(無敵)かのフラグ
     */ 
    public bool FlashFlag { set; get; }
    /*
     * 無敵時間
     */ 
    private float Invincible = 2f;
    private float Count = 0;

    private RabitDamage rd;
    public bool FlypanFlag { get; set; }

    private SelectWepon sw;

    // Use this for initialization
    void Start () {
        FlypanFlag = false;
        GameObject canvas = GameObject.Find("Canvas");
        cm = canvas.GetComponent<CoordinateManage>();
        rd = GetComponent<RabitDamage>();
        sw = canvas.GetComponent<SelectWepon>();
    }
	
    /*
     *-----------------------------------------------------
     * プレイヤーが攻撃を受けたら逆方向に移動させる
     * 敵の方向はcaseの値が 
     *      0 : 上方向
     *      1 : 右方向
     *      2 : 下方向
     *      3 : 左方向
     * となり、その逆方向にプレイヤーを移動させている
     *-----------------------------------------------------
     */ 
    public int PlayerKnockBack(int HP) {
        /*
         * プレイヤーが1フレームにノックバックする距離と向き
         */ 
        float frame = 0;
        switch (Axis) {
            case 0:
                frame = -0.1f;
                //transform.position += new Vector3(0f, frame, 0f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, FirstCharacterPos.y + (frame * Distance), transform.position.z),Time.deltaTime*3);
                if (transform.position.y <= FirstCharacterPos.y + (frame * Distance)) {
                    HP--;
                    KnockBackFlag = false;
                }
                break;
            case 1:
                frame = -0.1f;
                //transform.position += new Vector3(frame, 0f, 0f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(FirstCharacterPos.x + (frame * Distance), transform.position.y, transform.position.z),Time.deltaTime*3);
                if (transform.position.x <= FirstCharacterPos.x + (frame * Distance)) {
                    HP--;
                    KnockBackFlag = false;
                }
                break;
            case 2:
                frame = 0.1f;
                //transform.position += new Vector3(0f, frame, 0);
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, FirstCharacterPos.y + (frame * Distance), transform.position.z),Time.deltaTime*3);
                if (transform.position.y >= FirstCharacterPos.y + (frame * Distance)) {
                    HP--;
                    KnockBackFlag = false;
                }
                break;
            case 3:
                frame = 0.1f;
                //transform.position += new Vector3(frame, 0f, 0f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(FirstCharacterPos.x + (frame * Distance), transform.position.y, transform.position.z),Time.deltaTime*3);
                if (transform.position.x >= FirstCharacterPos.x + (frame * Distance)) {
                    HP--;
                    KnockBackFlag = false;
                }
                break;
        }
        return HP;
    }
    /*
     * 無敵時間中の点滅処理
     */ 
    public void Flashing(SpriteRenderer Character) {
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
        Character.color = new Color(1f, 1f, 1f, level);
    }

    /*
     * 無敵時間のカウント(ここで点滅処理を呼び出す)
     * 無敵時間が終わったらfalseを返す
     */ 
    public bool DamageEffect(SpriteRenderer CharacterSprite) {
        Flashing(CharacterSprite);
        Count += Time.deltaTime;
        if(Count >= Invincible) {
            CharacterSprite.color = new Color(1f, 1f, 1f, 255);
            Count = 0;
            return false;
        } else {
            return true;
        }
    }

    void OnTriggerEnter2D(Collider2D co) {
        /*
         * タイトル画面で、ドアに当たったら描画をやめる
         */ 
        if (co.gameObject.tag == "Door") {
            gameObject.SetActive(false);
        }

        /*
         * 敵のひっかき攻撃を受けた場合の処理
         * CoordinateManageで計算した敵の方角をAxisに代入する
         */
        if ((co.gameObject.tag == "EnemyScrabble" || co.gameObject.tag == "EnemyPoison" || co.gameObject.tag == "EnemyWave") && FlashFlag == false) {
            if(co.gameObject.tag == "EnemyPoison" && sw.FlyPanEquipFlag) {
                return;
            }
            Debug.Log("攻撃");
            FirstCharacterPos = this.transform.position;
            KnockBackFlag = true;
            FlashFlag = true;
            //Vector3 hitPos = co.bounds.ClosestPoint(this.transform.position);
            switch (cm.CalcCoordinate(co.transform.position, this.transform.position)) {
                case 0:
                    Debug.Log("上からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
                case 1:
                    Debug.Log("右からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
                case 2:
                    Debug.Log("下からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
                case 3:
                    Debug.Log("左からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
            }
        }

        
        if ((co.gameObject.tag == "Knife" || co.gameObject.tag == "Bullet" || co.gameObject.tag == "Flypan") && FlashFlag == false) {
            Debug.Log("攻撃");
            if (co.gameObject.tag == "Flypan") FlypanFlag = true;
            FirstCharacterPos = this.transform.position;
            KnockBackFlag = true;
            FlashFlag = true;
            //Vector3 hitPos = co.bounds.ClosestPoint(this.transform.position);
            switch (cm.CalcCoordinate(co.transform.position, this.transform.position)) {
                case 0:
                    Debug.Log("上からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
                case 1:
                    Debug.Log("右からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
                case 2:
                    Debug.Log("下からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
                case 3:
                    Debug.Log("左からの攻撃");
                    Axis = cm.CalcCoordinate(co.transform.position, this.transform.position);
                    break;
            }
        }
        
    }
}
