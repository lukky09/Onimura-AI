using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{

    public GameObject[] kartoes;
    bool isAIturn;
    Spots[,] objarr; //x,y
    Pions[,] allpion; //0 = ai, 1 = player (kebalik krn papane kebalik)
    Color green = new Color(98f / 255f, 188f / 255f, 40f / 255f);
    Color grey = new Color(176f / 255f, 176f / 255f, 176f / 255f);

    Spots chosenpapan;
    Card chosencard;
    int indexkartu;

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
                allpion[1, i] = new Pions(false, true, i, 4);
                objarr[allpion[0, i].xpos, allpion[0, i].ypos].setpion(ref allpion[0, i]);
                objarr[allpion[1, i].xpos, allpion[1, i].ypos].setpion(ref allpion[1, i]);
            }
            else
            {
                allpion[0, i] = new Pions(true, false, i, 0);
                allpion[1, i] = new Pions(true, true, i, 4);
                objarr[allpion[0, i].xpos, allpion[0, i].ypos].setpion(ref allpion[0, i]);
                objarr[allpion[1, i].xpos, allpion[1, i].ypos].setpion(ref allpion[1, i]);
            }
        }

    }

   void refreshpapan()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                objarr[j, i].GetComponent<SpriteRenderer>().color = grey;
            }
        }
        if (chosenpapan != null)
        {
            chosenpapan.GetComponent<SpriteRenderer>().color = green;
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
        for (int j = 0; j < 5; j++)
        {
            if (kartoes[j].GetComponent<Card>() == c && j < 2)
            {
                setkartupos();
                indexkartu = j;
                chosencard = c;
                chosencard.transform.position = new Vector2(chosencard.transform.position.x, chosencard.transform.position.y + 1);
                Debug.Log("Pilih kartu ke-" + (indexkartu+1));
                break;
            }
        }
        cekmoveavailable();
    }

    public void pickspot(Spots s)
    {
        if (s.GetComponent<SpriteRenderer>().color == green)
        {
            int index = -1;
            for (int i = 0; i < 5; i++)
            {
                if (chosenpapan.pion.xpos == allpion[1, i].xpos && chosenpapan.pion.ypos == allpion[1, i].ypos)
                {
                    index = i;
                    break;
                }
            }
            //pindah
            s.setpion(ref allpion[1,index]);
            chosenpapan.DestroyPion();
            //muter kartu
            GameObject tempcard = kartoes[indexkartu];
            kartoes[indexkartu] = kartoes[4]; // kartu rotation
            kartoes[4] = tempcard;
            setkartupos();
            //ngebersihin
            chosenpapan = null;
            chosencard = null;
            //bersihin bg
            refreshpapan();
        }

    }

    public void choosepion(int x, int y)
    {
        if (chosenpapan != null)
        {
            chosenpapan.GetComponent<SpriteRenderer>().color = grey;
        }
        chosenpapan = objarr[x, y];
        Debug.Log("dipilih papan di " + x + "," + y);
        cekmoveavailable();
    }

    public void eatpion(int x, int y)
    {
        if (chosenpapan != null)
        {
            chosenpapan.GetComponent<SpriteRenderer>().color = grey;
            int index = -1;
            //cari pion player
            for (int i = 0; i < 5; i++)
            {
                if (chosenpapan.pion.xpos == allpion[1, i].xpos && chosenpapan.pion.ypos == allpion[1, i].ypos)
                {
                    index = i;
                    break;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if(allpion[0,i]!=null && allpion[0,i].xpos == x && allpion[0, i].ypos == y)
                {
                    allpion[0, i] = null;
                    objarr[x, y].DestroyPion();
                    objarr[x, y].GetComponent<Spots>().setpion(ref allpion[1, index]);
                }
            }
        }
        chosenpapan.DestroyPion();
        //muter kartu
        GameObject tempcard = kartoes[indexkartu];
        kartoes[indexkartu] = kartoes[4]; // kartu rotation
        kartoes[4] = tempcard;
        setkartupos();
        //ngebersihin
        chosenpapan = null;
        chosencard = null;
        //bersihin bg
        refreshpapan();
        Debug.Log("dipilih papan di " + x + "," + y);
        cekmoveavailable();
    }

    void cekmoveavailable()
    {
        refreshpapan();
        if (chosencard != null && chosenpapan != null)
        {
            int x = chosenpapan.GetComponent<Spots>().x;
            int y = chosenpapan.GetComponent<Spots>().y;
            int xtemp, ytemp;
            foreach (int[] arr in chosencard.gerakans)
            {
                xtemp = arr[0] + x;
                ytemp =  y - arr[1] ; ///krn + nti tambah ke bawah
                if (xtemp >= 0 && xtemp < 5 && ytemp >= 0 && ytemp < 5)
                {
                    objarr[xtemp, ytemp].GetComponent<SpriteRenderer>().color = green;
                }
            }
        }
    }

    void setkartupos()
    {
        kartoes[0].transform.position = new Vector2(-4.5f, -2f);
        kartoes[1].transform.position = new Vector2(4.5f, -2f);
        kartoes[2].transform.position = new Vector2(-4.5f, 2f);
        kartoes[3].transform.position = new Vector2(4.5f, 2f);
        kartoes[4].transform.position = new Vector2(6.5f, 0);
    }

}
