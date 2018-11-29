using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDamage : MonoBehaviour {
    /*
     * ダメージエフェクトのスクリプト
     */
    private Damage dmg;
    /*
     * プレイヤーのスプライト
     */
    private SpriteRenderer EnemySprite;
    private int Hp = 2;
    private float FadeTime = 1f;

    public void Death() {
        if (Hp <= 0) {
            FadeTime -= Time.deltaTime;
            EnemySprite.color = new Color(1f, 1f, 1f, FadeTime);
            if (FadeTime <= 0f) {
                Destroy(this.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start() {
        dmg = GetComponent<Damage>();
        EnemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (dmg.KnockBackFlag) {
            Hp = dmg.PlayerKnockBack(Hp);
        }
        if (dmg.FlashFlag) {
            dmg.FlashFlag = dmg.DamageEffect(EnemySprite);
        }

        Death();
        //if (Hp <= 0) Destroy(this.gameObject);
    }
}
