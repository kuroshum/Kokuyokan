using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardStruct : MonoBehaviour {
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
     * 
     */ 
    private int[] RandomNum;
    private int Pointer = 0;

    private string[] Heal = new string[] { "Small", "Small", "Small", "Small", "Small", "Big", "Big", "Big" };
    private int Wepon = 2;

    public void InitSet() {
        Items = new Item[ItemObj.Length];
        RandomNum = new int[ItemObj.Length];
        for (int i = 0; i < ItemObj.Length; i++) {
            RandomNum[i] = -1;
            Items[i].ObjName = ItemObj[i].transform.name;
        }

        KeySet();
        WeponSet();
        HealSet();

        for (int i = 0; i < ItemObj.Length; i++) {
            Debug.Log("ObjName   : " + Items[i].ObjName + "\n" + "ItemName : " + Items[i].ItemName);
        }
        for (int i = 0; i < ItemObj.Length; i++) {
            Debug.Log(RandomNum[i]);
        }

    }

    public void KeySet() {
        int KeyNum = Random.Range(8, ItemObj.Length);
        RandomNum[Pointer++] = KeyNum;
        //Pointer++;
        Items[KeyNum].ItemName = "Key";
    }

    public void WeponSet() {
        int WeponNum;
        int Check;
        for (int j = 0; j < Wepon; j++) {
            while (true) {
                Check = 0;
                if(j == 0) {
                    WeponNum = Random.Range(0, 3);
                } else {
                    WeponNum = Random.Range(5, ItemObj.Length);
                }
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
            //Pointer++;
            Items[WeponNum].ItemName = "Wepon";
        }

    }

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
            //Pointer++;
            Items[HealNum].ItemName = Heal[j];
        }
    }


    private void Start() {
        InitSet();
    }
}
