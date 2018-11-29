using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateManage : MonoBehaviour {
    /*
     * 敵とプレイヤーの距離のx座標
     */ 
    private float SideX;
    /*
     * 敵とプレイヤーの距離のy座標
     */
    private float SideY;
    /*
     * 敵とプレイヤーの角度(12時の方向から時計回りに角度が増えていく)
     */
    private float Ang;

    /*
     * 敵がプレイヤーから見てどの方角にいるかを計算
     */ 
    public int CalcCoordinate(Vector3 GivePos, Vector3 RecivePos) {
        SideX = GivePos.x - RecivePos.x;
        SideY = GivePos.y - RecivePos.y;
        /*
         * ラジアンを弧度法に変換
         */ 
        Ang = Mathf.Rad2Deg * Mathf.Atan2(SideX, SideY);
        /*
         * -180 ～ 180 を 0 ～ 360に変換
         */ 
        if(Ang < 0) {Ang += 360;}
        
        if(315 <= Ang || Ang < 45) {
            return 0;
        }else if(45 <= Ang && Ang < 135) {
            return 1;
        }else if(135 <= Ang && Ang < 225) {
            return 2;
        } else {
            return 3;
        }
    }
}
