using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour {
    [SerializeField]
    private GameObject[] Door;
    [SerializeField]
    private GameObject[] OpenDoor;
    [SerializeField]
    private GameObject[] Text = new GameObject[3];

    private RectTransform[] TextRect = new RectTransform[3];
    private GameObject Canvas;
    private GameObject Camera;
    private Camera mainCamera;
    private GameObject[] StageText;
    private float num = Mathf.PI * 1 / 3;

    private FadeOut fo;
    private Transform Panel;
    private RectTransform pos;

    private bool DeleteFlag;

    private AudioSource sources;

    void Start() {
        //StageText = new GameObject[3];
        Canvas = GameObject.Find("Canvas");
        Panel = Canvas.transform.Find("Panel");
        fo = Panel.GetComponent<FadeOut>();
        pos = GetComponent<RectTransform>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera = Camera.GetComponent<Camera>();
        for(int i = 0; i < Text.Length; i++) {
            TextRect[i] = Text[i].GetComponent<RectTransform>();
            //Debug.Log(TextRect[i]);
        }
        sources = gameObject.GetComponent<AudioSource>();
    }

    void Update() {
        if (DeleteFlag) {
            DeleteText();
        }
    }

    public void OnEasyClick() {
        DoorManage(0);
        DeleteFlag = true;
        sources.Play();
    }

    public void OnNormalClick() {
        DoorManage(1);
        DeleteFlag = true;
        sources.Play();
    }

    public void OnHardClick() {
        DoorManage(2);
        DeleteFlag = true;
        sources.Play();
    }

    public void DeleteText() {
        for(int i = 0; i < Text.Length; i++) {
            if (TextRect[i].localScale.x > 0.1) {
                if(mainCamera.orthographicSize >= 0) {
                    mainCamera.orthographicSize -= Time.unscaledDeltaTime * 1.5f;
                    if( pos.localPosition.x > 0) {
                        Camera.transform.position = new Vector3(Camera.transform.position.x + Time.unscaledDeltaTime * 2.5f, Camera.transform.position.y, Camera.transform.position.z);
                    } else if(pos.localPosition.x < 0){
                        Camera.transform.position = new Vector3(Camera.transform.position.x - Time.unscaledDeltaTime * 2.5f, Camera.transform.position.y, Camera.transform.position.z);
                    }
                    
                }
                TextRect[i].localScale = new Vector2(Mathf.Abs(1.1f * Mathf.Sin(num)), Mathf.Abs(1.1f * Mathf.Sin(num)));
                num += Time.unscaledDeltaTime * 1f;
            } else {
                //Text[i].SetActive(false);
                int j = 0;
                foreach (Transform child in Canvas.transform) {
                    if (j > 2) continue;
                    Text[j] = child.gameObject;
                    Text[j].SetActive(false);
                    j++;
                }
            }
        }
        fo.isFadeOut = true;
        
    }

    public void DoorManage(int dNum) {
        fo.StageNum = dNum;
        for (int i = 0; i < Door.Length; i++) {
            if (i == dNum) {
                Door[dNum].SetActive(false);
                OpenDoor[dNum].SetActive(true);
            } else {
                Door[i].SetActive(true);
                OpenDoor[i].SetActive(false);
            }
        }
    }

    
}
