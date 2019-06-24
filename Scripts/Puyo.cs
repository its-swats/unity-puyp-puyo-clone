using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puyo : MonoBehaviour
{
    public GameObject[] unitArray = new GameObject[2];

    public float fallSpeed = 1;
    public float interval = 0;

    private Vector3 left = Vector3.left;
    private Vector3 right = Vector3.right;
    private Vector3 down = Vector3.down;
    private Vector3 up = Vector3.up;

    private bool puyoUnitDropsFinished = false;

    void Start()
    {
        unitArray[0] = Instantiate((GameObject)Resources.Load("PuyoUnit"), transform.position, Quaternion.identity);
        unitArray[1] = Instantiate((GameObject)Resources.Load("PuyoUnit"), new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
        unitArray[0].transform.parent = gameObject.transform;
        unitArray[1].transform.parent = gameObject.transform;
        UpdateGameBoard();
    }

    void Update()
    {
        AutoDrop();
        // GameBoard.DebugBoard();           
    }

    void AutoDrop(){
        if(interval > fallSpeed){
            MoveDown();
            interval = 0; 
        } else {
            interval += Time.deltaTime;
        }
    }

    //////////////
    // Movement //
    //////////////

    public void MoveLeft(){
        if(ValidMove(left)){
            Move(left, transform);
        }
    }

    public void MoveRight(){
        if(ValidMove(right)){
            Move(right, transform);
        }
    }

    public void MoveDown(){
        if(ValidMove(down)){
            Move(down, transform);
        } else {
            DisableSelf();
        }
    }

    public void RotateLeft(){
        Vector3 vect = GetClockwiseRotationVector();
        if(ValidRotate(vect)){
            Move(vect, unitArray[1].transform);
        }
    }

    public void RotateRight(){
        Vector3 vect = GetCounterClockwiseRotationVector();
        if(ValidRotate(vect)){
            Move(vect, unitArray[1].transform);
        }
    }

    void Move(Vector3 vector, Transform target){
        ClearCurrentGameboardPosition();
        target.position += vector;
        UpdateGameBoard();
    }

    void ClearCurrentGameboardPosition(){
        foreach(Transform puyoUnit in transform){
            GameBoard.Clear(puyoUnit.transform.position.x, puyoUnit.transform.position.y);
        }
    }

    void UpdateGameBoard(){
        foreach(Transform puyoUnit in transform){
            GameBoard.Add(puyoUnit.position.x, puyoUnit.position.y, puyoUnit);
        }
    }

    Vector3 GetClockwiseRotationVector(){
        Vector3 puyoUnitPos = RoundVector(unitArray[1].transform.position);

        if(Vector3.Distance(puyoUnitPos + left, transform.position) == 0){
            return new Vector3(-1, -1);
        } else if(Vector3.Distance(puyoUnitPos + up, transform.position) == 0){
            return new Vector3(-1, +1);
        } else if(Vector3.Distance(puyoUnitPos + right, transform.position) == 0){
            return new Vector3(+1, +1);
        } else if(Vector3.Distance(puyoUnitPos + down, transform.position) == 0){
            return new Vector3(+1, -1);
        }
        
        return new Vector3(0, 0);
    }

    Vector3 GetCounterClockwiseRotationVector(){
        Vector3 puyoUnitPos = RoundVector(unitArray[1].transform.position);

        if(Vector3.Distance(puyoUnitPos + left, transform.position) == 0){
            return new Vector3(-1, +1);
        } else if(Vector3.Distance(puyoUnitPos + up, transform.position) == 0){
            return new Vector3(+1, +1);
        } else if(Vector3.Distance(puyoUnitPos + right, transform.position) == 0){
            return new Vector3(+1, -1);
        } else if(Vector3.Distance(puyoUnitPos + down, transform.position) == 0){
            return new Vector3(-1, -1);
        }
        
        return new Vector3(0, 0);
    }

    bool ActivelyFalling(){
        return unitArray[0].GetComponent<PuyoUnit>().activelyFalling ||
            unitArray[1].GetComponent<PuyoUnit>().activelyFalling;
    }

    ///////////////////////////
    // Movement Constraints //
    /////////////////////////

    bool ValidMove(Vector3 direction){
        foreach(Transform puyo in transform){
            Vector3 newPosition = new Vector3(puyo.position.x + direction.x, puyo.position.y + direction.y, 0);

            if(!GameBoard.FreeSpace(newPosition, transform)){
                return false;
            }
        }
        return true;
    }

    bool ValidRotate(Vector3 direction){
        Vector3 puyoPos = unitArray[1].transform.position;
        Vector3 newPosition = new Vector3(puyoPos.x + direction.x, puyoPos.y + direction.y);
        return GameBoard.FreeSpace(newPosition, transform);
    }
    
    ////////////////
    // PuyoUnits //
    ///////////////

    private void DropPuyoUnits(){
        foreach(Transform puyoUnit in transform){    
            StartCoroutine(puyoUnit.gameObject.GetComponent<PuyoUnit>().DropToFloor());
        }
    }

    ////////////////
    // Utilities //
    ///////////////

    public Vector3 RoundVector(Vector3 vect){
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    void DisableSelf(){
        gameObject.GetComponent<PlayerController>().enabled = false;
        DropPuyoUnits();
        enabled = false;
        StartCoroutine(SpawnNextBlock());
    }

    IEnumerator SpawnNextBlock(){
        yield return new WaitUntil(() => !ActivelyFalling());

        GameObject.Find("PuyoSpawner").GetComponent<PuyoSpawner>().SpawnPuyo();
    }
}
