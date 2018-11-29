using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TtileCotrol : MonoBehaviour {
    private bool GameStartFlag;
    [SerializeField]
    private Sprite[] Image; 
    private Transform House;
    private Image HouseImage;
    [SerializeField]
    private GameObject Player;
    private MoveCharacter mc;
    [SerializeField]
    private GameObject Text;
    private TextHighlight th;
    TextMeshProUGUI tmPro;
    Material material;
    private RectTransform TextRect;
    private float num = Mathf.PI * 1 / 3;
    private int[] One = new int[2] { 0,0};

    [SerializeField]
    private GameObject Title;

    private GameObject eventSystem;

    /*
      *サウンド代入する変数
      */
    private AudioSource[] sources;

    // Use this for initialization
    void Start () {
        Title.transform.position = new Vector3(0.2f, 6f, 0);
        GameStartFlag = false;
        House = gameObject.transform.Find("HouseImage");
        HouseImage = House.GetComponent<Image>();
        mc = Player.GetComponent<MoveCharacter>();
        mc.enabled = false;
        tmPro = Text.GetComponent<TextMeshProUGUI>();
        material = tmPro.fontMaterial;
        th = Text.GetComponent<TextHighlight>();
        TextRect = Text.GetComponent<RectTransform>();
        sources = gameObject.GetComponents<AudioSource>();

        //Debug.Log("STart");
    }

    // Update is called once per frame
    void Update () {
        Title.transform.position = Vector3.MoveTowards(Title.transform.position, new Vector3(0.2f, 2.7f, 0), Time.deltaTime);
        //Debug.Log(GameStartFlag);
        if (Input.anyKeyDown) {
            GameStartFlag = true;
            sources[0].Play();
        }
        /*
        if (Player.activeSelf == false) {
            GameStartFlag = false;
            HouseImage.sprite = Image[0];
        }
        */

        if (GameStartFlag) {
            sources[4].Stop();
            th.enabled = false;
            material.SetFloat("_OutlineWidth", 0.4f);
            
            if (TextRect.localScale.x > 0.1) {
                TextRect.localScale = new Vector2(Mathf.Abs(1.1f * Mathf.Sin(num)), Mathf.Abs(1.1f * Mathf.Sin(num)));
                num += Time.unscaledDeltaTime * 1.4f;
                //Debug.Log("gamestartdayo");
                Debug.Log(num);
            } else {
                Text.SetActive(false);
            }
            StartCoroutine("MoveCoroutine");
            
        }
        
	}

    private IEnumerator MoveCoroutine() {
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log(One[0]);
        if (One[0] == 0) {
            sources[1].Play();
            One[0]++;
        }
        mc.enabled = true;
        mc.Move(0, 1);
        HouseImage.sprite = Image[1];
        StartCoroutine("CloseDoorCoroutine");
    }

    private IEnumerator CloseDoorCoroutine() {
        yield return new WaitForSecondsRealtime(2f);
        if (One[1] == 0) {
            sources[2].Play();
            One[1]++;
        }
        GameStartFlag = false;
        Image[1] = Image[0];
        HouseImage.sprite = Image[0];
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("SelectStage");
    }

    private void Sound(int num) {
        int i = 0;
        if (i == 0) {
            sources[num].Play();
            i++;
        }
    }
}
