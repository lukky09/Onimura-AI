using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{

    public GameObject[] kartoes;
    bool isAIturn;
    Spots[,] objarr; //x,y
    Spots chosenpapan;

    // Start is called before the first frame update
    void Start()
    {
        isAIturn = false;

        setkartupos();

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
        settingkartu();

    }

    void settingkartu()
    {
        ArrayList indexes = new ArrayList();
        int r; bool ada;
        for (int i = 0; i < 5; i++)
        {
            do
            {
                r = Random.Range(0, 16);
                ada = false;
                foreach (int a in indexes)
                {
                    if (a == r)
                    {
                        ada = true;
                    }
                }
            } while (ada);
            indexes.Add(r);
        }

        for (int i = 0; i < 5; i++)
        {
            kartoes[i].GetComponent<Card>().setcard((int)indexes[i]);
        }
    }

    public void changekartu(Card c)
    {
        int i = -1;
        for (int j = 0; j < 5; j++)
        {
            if (kartoes[j].GetComponent<Card>() == c)
            {
                i = j;
                Debug.Log(i);
                break;
            }
        }
        GameObject tempcard = kartoes[i];
        kartoes[i] = kartoes[4]; // kartu rotation
        kartoes[4] = tempcard;
        setkartupos();
    }

    void setkartupos()
    {
        kartoes[0].transform.position = new Vector2(-4.5f, -2f);
        kartoes[1].transform.position = new Vector2(4.5f, -2f);
        kartoes[2].transform.position = new Vector2(-4.5f, 2f);
        kartoes[3].transform.position = new Vector2(4.5f, 2f);
        kartoes[4].transform.position = new Vector2(6.5f, 0);
    }

    public void henshin(int x,int y)
    {
        objarr[x, y].GetComponent<SpriteRenderer>().color = Color.green;
    }

}
