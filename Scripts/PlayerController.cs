using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Puyo puyo;

    void Start()
    {
        puyo = GetComponent<Puyo>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            puyo.MoveLeft();
        } else if(Input.GetKeyDown(KeyCode.RightArrow)){
            puyo.MoveRight();
        } else if(Input.GetKeyDown(KeyCode.DownArrow)){
            puyo.MoveDown();
        } else if(Input.GetKeyDown("z")){
            puyo.RotateLeft();
        } else if(Input.GetKeyDown("x")){
            puyo.RotateRight();
        }
    }
}
