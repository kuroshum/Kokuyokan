using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitDamage : MonoBehaviour {
    /*
     * ダメージエフェクトのスクリプト
     */
    private Damage dmg;
    /*
     * プレイヤーのスプライト
     */
    private SpriteRenderer EnemySprite;
    private int Hp = 1;
    private float FadeTime = 1f;

    public void Death() {
        if(Hp <= 0) {
            if(FadeTime >= 1) {
                switch (this.gameObject.tag) {
                    case "Rabbit":
                        GetComponent<RabbitEnemy>().enabled = false;
                        break;
                    case "Snake":
                        GetComponent<WingSnakeEnemy>().enabled = false;
                        break;
                    case "Human":
                        GetComponent<HumanEnemy>().enabled = false;
                        break;
                    case "Tutorial":
                        GetComponent<TutorialEnemy>().enabled = false;
                        break;
                }
            }
            
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
        switch (this.transform.gameObject.tag) {
            case "Rabbit":
                Hp = 2;
                break;
            case "Snake":
                Hp = 2;
                break;
            case "Human":
                Hp = 2;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        if (dmg.KnockBackFlag) {
            //Hp = dmg.PlayerKnockBack(Hp);
            if (dmg.FlypanFlag) {
                Hp -= 2;
                dmg.FlypanFlag = false;
            } else {
                Hp--;
            }
            dmg.KnockBackFlag = false;
        }
        if (dmg.FlashFlag) {
            dmg.FlashFlag = dmg.DamageEffect(EnemySprite);
        }

        Death();
        //if (Hp <= 0) Destroy(this.gameObject);

    }

    


}
