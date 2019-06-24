using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private Canvas canvas;

    void Awake(){
        canvas = GameObject.Find("GameStartCanvas").GetComponent<Canvas>();
    }

    public void StartGame(){
        GameObject.Find("PuyoSpawner").GetComponent<PuyoSpawner>().enabled = true;
        canvas.gameObject.SetActive(false);
    }
}
