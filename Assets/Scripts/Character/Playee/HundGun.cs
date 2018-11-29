using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HundGun : MonoBehaviour {
    [SerializeField]
    private GameObject BulletPrefab;
    private Knife knife;
    [SerializeField]
    private GameObject[] MuzzleFlashEffect = new GameObject[4];
    private float Count = 0.0f;
    private float Interval = 0.3f;
    private bool TimeFlag;
    private float dxBuf1 = 0;
    private float dyBuf1 = -1;
    private float dxBuf0 = 0;
    private float dyBuf0 = -1;

    private AtkPlayer atk;
    // Use this for initialization
    void Awake() {
        knife = GetComponent<Knife>();
        PlayerBullet.SetOriginal(BulletPrefab);
        atk = GetComponent<AtkPlayer>();
    }

    public float Step(float input, float output) {
        if (input > 0) output = 1;
        else if (input < 0) output = -1;

        return output;
    }

    public void Attack(float dx, float dy, float dxbuf, float dybuf, AudioSource GunSound) {

        /*
        if (dx != 0) {
            if (dx > 0) dxBuf = 1;
            else if (dx < 0) dxBuf = -1;
            dyBuf = 0;
        }
        if (dy != 0) {
            if (dy > 0) dyBuf = 1;
            else if (dy < 0) dyBuf = -1;
            dxBuf = 0;
        }
        */

        //this.dxBuf1 = dxbuf;
        this.dxBuf0 = dxbuf;
        //this.dyBuf1 = dybuf;
        this.dyBuf0 = dybuf;

        /*
        if (dxbuf > 0) dxBuf1 = 1;
        else if (dxbuf < 0) dxBuf1 = -1;

        if (dybuf > 0) dyBuf1 = 1;
        else if (dybuf < 0) dyBuf1 = -1;
        */
        dxBuf1 = Step(dxbuf, dxBuf1);
        dyBuf1 = Step(dybuf, dyBuf1);

        if ((dx != 0 && (dx <= 0 || dx >= 1)) || dy != 0 && (dy <= 0 || dy >= 1)) {
            dxBuf0 = dx;
            dyBuf0 = dy;

            /*
            if (dx > 0) dxBuf1 = 1;
            else if (dx < 0) dxBuf1 = -1;

            if (dy > 0) dyBuf1 = 1;
            else if (dy < 0) dyBuf1 = -1;
            */
            dxBuf1 = Step(dx, dxBuf1);
            dyBuf1 = Step(dy, dyBuf1);

        }

        //Debug.Log("dxbuf : " + dxBuf1 + " dyBuf : " + dyBuf1);

        
        if (Mathf.Abs(dxBuf0) >= Mathf.Abs(dyBuf0)) {
            dyBuf1 = 0;
        } else {
            dxBuf1 = 0;
        }
        

        //if (Input.GetKey(KeyCode.Y)) {
        if (Input.GetButton("Circle")) {
            TimeFlag = true;
            KeyDownShoot(GunSound);
        //} else if (Input.GetKeyDown(KeyCode.Y)) {
        } else if (Input.GetButtonDown("Circle")) {
            KeyDownShoot(GunSound);
        }

        //Debug.Log("dxBuf : " + dxBuf + " dyBuf : " + dyBuf);
        CountTime();

        atk.dxBuf = dxBuf0;
        atk.dyBuf = dyBuf0;
    }

    public void Shoot(AudioSource GunSound) {
        PlayerBullet bullet = PlayerBullet.Create();
        float speed = 1.0f;
        Vector3 PlayerDir = new Vector3(transform.position.x + (int)dxBuf1 * 6, transform.position.y + (int)dyBuf1 * 6, transform.position.z);
        Vector3 BulletDir = new Vector3(transform.position.x + (dxBuf1 * 3/4), transform.position.y + (dyBuf1 * 3/4), transform.position.z);
        bullet.Shoot(BulletDir, PlayerDir);
        GunSound.Play();
        if (Count > Interval) Count = 0;
        MuzzleFlash();
    }

    public void KeyDownShoot(AudioSource GunSound) {
        if(Count == 0 || Count >= Interval) {
            Shoot(GunSound);
        }
    }

    public void MuzzleFlash() {
        if (dxBuf1 > 0) {
            StartCoroutine("SetMuzzleFlashCoroutine", 1);
        } else if (dxBuf1 < 0) {
            StartCoroutine("SetMuzzleFlashCoroutine", 2);
        }
        if (dyBuf1 > 0) {
            StartCoroutine("SetMuzzleFlashCoroutine", 0);
        } else if (dyBuf1 < 0) {
            StartCoroutine("SetMuzzleFlashCoroutine", 3);
        }
    }

    public void CountTime() {
        if (TimeFlag) {
            Count += Time.deltaTime;
            if (Count >= Interval) {
                TimeFlag = false;
            }
        }
    }

    /*
     *----------------------------------------- -----------------------------
     * num : プレイヤーの向き情報
     * numの方向のナイフエフェクトを表示・それ以外のナイフエフェクトを非表示
     *----------------------------------------------------------------------- 
     */
    void SetMuzzleEffect(int num) {
        for (int i = 0; i < MuzzleFlashEffect.Length; i++) {
            if (i == num) {
                MuzzleFlashEffect[i].SetActive(true);
            } else {
                MuzzleFlashEffect[i].SetActive(false);
            }
        }
    }

    /*
     * ナイフエフェクトを0.1秒だけ表示させて非表示にする
     */
    IEnumerator SetMuzzleFlashCoroutine(int num) {
        SetMuzzleEffect(num);
        yield return new WaitForSeconds(0.1f);
        MuzzleFlashEffect[num].SetActive(false);
    }
}
