using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samplestruct : MonoBehaviour {

    //[SerializeField]
    public GameObject[] ItemObj;
    private int[] ArrayItemValue;

    public struct Item {
        public string Pos;
        public string Name;
    }

    //private List<item> Items = new List<item>();
    public Item[] Items { get; set; }

    private int[] RandomNum;
    private int Pointer = 0;

    private string[] Heal = new string[] { "Small", "Small", "Small", "Big" };

    public void test() {
        Items = new Item[ItemObj.Length];
        RandomNum = new int[ItemObj.Length];
        for(int i = 0; i < ItemObj.Length; i++) {
            RandomNum[i] = -1;
            Items[i].Pos = ItemObj[i].transform.name;
        }

        KeySet();
        WeponSet();
        HealSet();

        for(int i = 0; i < ItemObj.Length; i++) {
            Debug.Log("Pos : " + Items[i].Pos + " Name : " + Items[i].Name);
        }

        for (int i = 0; i < ItemObj.Length; i++) {
            Debug.Log(RandomNum[i]);
        }

    }

    public void KeySet() {
        int KeyNum = Random.Range(10, ItemObj.Length);
        RandomNum[Pointer++] = KeyNum;
        //Pointer++;
        Items[KeyNum].Name = "Key";
    }

    public void WeponSet() {
        int WeponNum;
        int Check;
        while (true) {
            Check = 0;
            WeponNum = Random.Range(6, ItemObj.Length);
            for (int i = 0; i < RandomNum.Length; i++) {
                if (RandomNum[i] == WeponNum) {
                    break;
                } else {
                    Check++;
                }
            }
            if(Check == RandomNum.Length) {
                break;
            }
        }
        RandomNum[Pointer++] = WeponNum;
        //Pointer++;
        Items[WeponNum].Name = "Wepon";
    }

    public void HealSet() {
        int HealNum;
        int Check;
        for(int j = 0; j < Heal.Length; j++) {
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
            Items[HealNum].Name = Heal[j];
        }
    }


    private void Start() {
        test();
    }

}
