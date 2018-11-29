using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealText : MonoBehaviour {
    [SerializeField]
    private GameObject Big;
    [SerializeField]
    private GameObject Small;
    private Text Bigtex;
    private Text Smalltex;

    private int BigvalBuf;
    private int SmallvalBuf;

    private PlayerParameter pp;
    private GameObject Player;

    // Use this for initialization
    void Start () {
        Bigtex = Big.GetComponent<Text>();
        Bigtex.text = "x 0";
        Smalltex = Small.GetComponent<Text>();
        Smalltex.text = "x 0";
        
        Player = GameObject.FindGameObjectWithTag("Player");
        pp = Player.GetComponent<PlayerParameter>();
        BigvalBuf = pp.Bheal;
        SmallvalBuf = pp.Sheal;
    }
	
	// Update is called once per frame
	void Update () {
        if(BigvalBuf != pp.Bheal) {
            Bigtex.text = "x " + pp.Bheal.ToString();
            BigvalBuf = pp.Bheal;
        }
        if(SmallvalBuf != pp.Sheal) {
            Smalltex.text = "x " + pp.Sheal.ToString();
            SmallvalBuf = pp.Sheal;
        }
    }
}
