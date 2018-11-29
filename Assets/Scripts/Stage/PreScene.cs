using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreScene : MonoBehaviour {
    public static string SceneName;
	// Use this for initialization
	void Start () {
        SceneName = SceneManager.GetActiveScene().name;
        Debug.Log(SceneName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
