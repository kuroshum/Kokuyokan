using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcvPlayer : MonoBehaviour {
    private PlayerParameter pp;

    [SerializeField]
    private Stats health;

    private int i = 0;

    void Awake() {
        health.Initialize();
    }

    private AudioSource[] sources;

    // Use this for initialization
    void Start () {
        sources = gameObject.GetComponents<AudioSource>();
        pp = GetComponent<PlayerParameter>();
	}
	
	// Update is called once per frame
	void Update () {
        health.StatCurrentVal = pp.Hp;
        if (Input.GetButtonDown("Rectangle")) {
            sources[4].Play();
            BigHealing();
        }
        if (Input.GetButtonDown("Cross")) {
            sources[3].Play();
            SmallHealing();
            Debug.Log("i:" + i);
        }
	}

    public int Healing(int HealingValue, int Heal) {
        
        if (pp.MINHP < pp.Hp && pp.Hp < pp.MAXHP && Heal > 0) {
            pp.Hp += HealingValue;
            //health.StatCurrentVal += HealingValue;
            Debug.Log("HP : " + pp.Hp);
            if (pp.MAXHP < pp.Hp) {
                pp.Hp = pp.MAXHP;
            }
            else if(pp.MINHP > pp.Hp) {
                pp.Hp = pp.MINHP;
            }
            Heal--;
        }
        return Heal;
    }

    public void SmallHealing() {
        pp.Sheal = Healing(1, pp.Sheal);
        //pp.Sheal--;
    }

    public void BigHealing() {
        pp.Bheal = Healing(2, pp.Bheal);
        //pp.Bheal--;
    }
}
