using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWall : MonoBehaviour {
    [SerializeField]
    private GameObject[] Wall2;
    [SerializeField]
    private GameObject[] Wall3;

    private int[] RandomWallNum2;
    private int[] RandomWallNum3;
    private int Check;
    private int rand;
    private int DeletePointer2 = 3;
    private int DeletePointer3 = 3;

    public void DeleteWall_Room2() {
        for(int i = 0; i < DeletePointer2; i++) {
            Wall2[RandomWallNum2[i]].SetActive(false);
        }
        DeletePointer2 += 2;
    }
    public void DeleteWall_Room3() {
        for (int i = 0; i < DeletePointer3; i++) {
            Wall3[RandomWallNum3[i]].SetActive(false);
        }
        DeletePointer3 += 2;
    }

    public void CreateWallArray_Room2() {
        int Pointer = 0;
        RandomWallNum2 = new int[Wall2.Length];
        for (int j = 0; j < Wall2.Length; j++) {
            while (true) {
                Check = 0;
                rand = Random.Range(0, Wall2.Length);
                for (int i = 0; i < j; i++) {
                    if (RandomWallNum2[i] == rand) {
                        break;
                    } else {
                        Check++;
                    }
                }
                if (Check == j) {
                    break;
                }
            }
            RandomWallNum2[Pointer++] = rand;
            Debug.Log("ite : " + RandomWallNum2[j]);
        }
    }

    public void CreateWallArray_Room3() {
        int Pointer = 0;
        RandomWallNum3 = new int[Wall3.Length];
        for (int j = 0; j < Wall3.Length; j++) {
            while (true) {
                Check = 0;
                rand = Random.Range(0, Wall3.Length);
                for (int i = 0; i < j; i++) {
                    if (RandomWallNum3[i] == rand) {
                        break;
                    } else {
                        Check++;
                    }
                }
                if (Check == j) {
                    break;
                }
            }
            RandomWallNum3[Pointer++] = rand;
            Debug.Log("ite : " + RandomWallNum3[j]);
        }
    }

    // Use this for initialization
    void Start () {
        CreateWallArray_Room2();
        CreateWallArray_Room3();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
