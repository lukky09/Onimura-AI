using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
    bool isAIturn;
    Spots[,] objarr; //x,y
    Card[] kartus; //k1p,k2p,k1m,k2m,kp

    // Start is called before the first frame update
    void Start()
    {
        kartus = new Card[5];
        isAIturn = false;
        objarr = new Spots[5,5];
        int tempx, tempy;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            tempx = i % 5;
            tempy = i / 5;
            gameObject.transform.GetChild(i).gameObject.AddComponent<Spots>();
            objarr[tempx, tempy] = gameObject.transform.GetChild(i).GetComponent<Spots>();
            objarr[tempx, tempy].board = gameObject;
            objarr[tempx, tempy].x = tempx;
            objarr[tempx, tempy].y = tempy;
        }
    }

    public void henshin(int x,int y)
    {
        objarr[x, y].GetComponent<SpriteRenderer>().color = Color.green;
    }

}
