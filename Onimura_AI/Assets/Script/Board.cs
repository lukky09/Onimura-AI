using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{

    public GameObject[] kartoes;
    bool isAIturn;
    Spots[,] objarr; //x,y
    Pions[,] allpion; //0 = player, 1 = ai

    Spots chosenpapan;
    Card chosencard;

    // Start is called before the first frame update
    void Start()
    {
        isAIturn = false;
        chosenpapan = null;
        chosencard = null;

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
        initializekartu();

        allpion = new Pions[2, 5];
        for (int i = 0; i < 5; i++)
        {
            if (i != 2)
            {
                allpion[0, i] = new Pions(false,false,i,0);
                objarr[allpion[0, i].xpos, allpion[0, i].ypos].setpion(allpion[0, i]);
                allpion[1, i] = new Pions(false, true, i, 4);
                objarr[allpion[1, i].xpos, allpion[1, i].ypos].setpion(allpion[1, i]);
            }
            else
            {
                allpion[0, i] = new Pions(true, false, i, 0);
                objarr[allpion[0, i].xpos, allpion[0, i].ypos].setpion(allpion[0, i]);
                allpion[1, i] = new Pions(true, true, i, 4);
                objarr[allpion[1, i].xpos, allpion[1, i].ypos].setpion(allpion[1, i]);
            }
        }

    }

    void initializekartu()
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
            if (kartoes[j].GetComponent<Card>() == c && j < 2)
            {
                i = j;
                chosencard = c;
                Debug.Log("Pilih kartu ke-" + (i+1));
                break;
            }
        }
        GameObject tempcard = kartoes[i];
        kartoes[i] = kartoes[4]; // kartu rotation
        kartoes[4] = tempcard;
        setkartupos();
    }

    public void choosepion(int x, int y)
    {
        chosenpapan = objarr[x, y];
        Debug.Log("dipilih papan di " + x + "," + y);
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
