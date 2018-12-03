using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStruct : MonoBehaviour {
      
    /*
     * アイテムがあるか調べられるオブジェクト
     */
    public GameObject[] ItemObj;

    /*
     * アイテムの構造体
     * ObjName  : ItemObjの名前(GameObject(1)など)
     * ItemName : ItemObjに割り当てられているアイテムの名前(Weponなど) 
     */
    public struct Item {
        public string ObjName;
        public string ItemName;
    }

    /*
     * アイテムの構造体の配列
     * この配列にアイテムを割り当てていく
     */
    public Item[] Items { get; set; }

    /*
     * ItemObjの要素数と同じ長さの連番の配列(中身はランダムになっている)
     * 例：
     * 　要素数が5の場合：
     * 　　0, 1, 2, 3, 4, 5 → 3, 4, 2, 5, 1 のようになる
     */ 
    private int[] RandomNum;
    
    /*
     * 現在のRandomNumのインデックス
     */ 
    private int Pointer = 0;

    /*
     * 回復薬の種類と数
     */ 
    private string[] Heal = new string[] { "Small", "Small", "Small", "Small", "Small", "Big", "Big" };

    /*
     * 武器レベル
     *  1 : ハンドガンが拾える
     *  2 : ハンドガンとフライパンが拾える
     */ 
    private int Wepon = 1;   

    /*
     * アイテムのランダム配置のセットアップ
     */ 
    public void InitSet() {
        Items = new Item[ItemObj.Length];
        RandomNum = new int[ItemObj.Length];
        /*
         * RandomNumはすべて -1 に初期化
         * ItemsのObjNameに名前をセット
         */ 
        for (int i = 0; i < ItemObj.Length; i++) {
            RandomNum[i] = -1;
            Items[i].ObjName = ItemObj[i].transform.name;
        }

        KeySet();
        WeponSet();
        HealSet();

        /*
         * コンソールに表示
         */ 
        for (int i = 0; i < ItemObj.Length; i++) {
            Debug.Log("ObjName   : " + Items[i].ObjName + "\n" + "ItemName : " + Items[i].ItemName);
        }

        for (int i = 0; i < ItemObj.Length; i++) {
            Debug.Log(RandomNum[i]);
        }

    }

    /*
     *--------------------------------------------------------------------------------------------------------------
     * 鍵をセット
     * 
     * 今回はオブジェクトの11 ～ 19のどれかにセットしている
     * (バランス調整、ゲームスタート手前に鍵が配置されるとゲームバランスがおかしくなる)
     * 
     * 11 ～ 19 どれかにセットしたら、それをRandonNumに保存(武器や回復薬をセットする際に被らないようにするため)
     *--------------------------------------------------------------------------------------------------------------
     */
    public void KeySet() {
        int KeyNum = Random.Range(11, 19);
        RandomNum[Pointer++] = KeyNum;
        Items[KeyNum].ItemName = "Key";
    }

    /*
     *--------------------------------------------------------------------------------------------------------------
     * 武器をセット
     * 
     * WeponNum : 武器を何番目のItemObjにセットするか (int)
     * Check    : 今回生成した乱数(WeponNum)が今までに生成したことがあるかをチェックする (int)
     *            CheckがRundomNumの要素数と同じになったら今回生成した乱数は初めて生成されたもの
     *            
     * 5 ～ ItemObjの要素数 のどれかにセット
     * 5からにしているのはKeySet関数と同じ理由
     * 
     * while文ではRandomNumに登録されていない数字を生成するまでループし続ける
     * 登録されていない数字が生成されたら、それをRandonNumに保存し、その数字に対応するオブジェクトにアイテムをセットする
     *--------------------------------------------------------------------------------------------------------------
     */
    public void WeponSet() {
        int WeponNum;
        int Check;
        for(int j = 0; j < Wepon; j++) {
            while (true) {
                Check = 0;
                WeponNum = Random.Range(5, ItemObj.Length);
                for (int i = 0; i < RandomNum.Length; i++) {
                    if (RandomNum[i] == WeponNum) {
                        break;
                    } else {
                        Check++;
                    }
                }
                if (Check == RandomNum.Length) {
                    break;
                }
            }
            RandomNum[Pointer++] = WeponNum;
            Items[WeponNum].ItemName = "Wepon";
        }
        
    }
    /*
     *-----------------------------
     * 基本的にWeponSet関数と同じ
     *-----------------------------
     */ 
    public void HealSet() {
        int HealNum;
        int Check;
        for (int j = 0; j < Heal.Length; j++) {
            while (true) {
                Check = 0;
                HealNum = Random.Range(0, ItemObj.Length);
                for (int i = 0; i < RandomNum.Length; i++) {
                    if (RandomNum[i] == HealNum) {
                        break;
                    } else {
                        Check++;
                    }
                }
                if (Check == RandomNum.Length) {
                    break;
                }
            }
            RandomNum[Pointer++] = HealNum;
            Items[HealNum].ItemName = Heal[j];
        }
    }


    private void Start() {
        InitSet();
    }
}
