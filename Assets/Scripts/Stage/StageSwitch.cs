using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSwitch : MonoBehaviour {
    [SerializeField]
    private GameObject[] Wall;
    [SerializeField]
    private GameObject CardBoard;

    public void DeleteWall_Room1(int Swich) {
        if (Swich == 1) {
            for(int i = 0; i < 4; i++) {
                Wall[i].SetActive(false);
            }
        } else {
            for (int i = 4; i < 8; i++) {
                Wall[i].SetActive(false);
            }
        }
    }

    public void DeleteBoard() {
        CardBoard.SetActive(false);
    }
}
