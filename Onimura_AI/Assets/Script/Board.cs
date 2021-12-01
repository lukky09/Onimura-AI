using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    public GameObject[] kartoes;
    public GameObject textobj;
    bool isFrozen = false;
    Spots[,] objarr; //x,y
    Pions[,] allpion; //0 = ai, 1 = player (kebalik krn papane kebalik)
    Color green = new Color(98f / 255f, 188f / 255f, 40f / 255f);
    Color grey = new Color(176f / 255f, 176f / 255f, 176f / 255f);

    Spots chosenpapan;
    Card chosencard;
    int indexkartu;

    int[] enemymove = new int[2];
    int enemycard, enemypion;

    // Start is called before the first frame update
    void Start()
    {
        textobj.GetComponent<Text>().color = new Color(98f / 255f, 188f / 255f, 40f / 255f, 0f / 255f);

        isFrozen = false;
        chosenpapan = null;
        chosencard = null;

        setkartupos();

        objarr = new Spots[5, 5];
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
                allpion[0, i] = new Pions(false, false, i, 0);
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
                objarr[j, i].GetComponent<Spots>().DestroyPion();
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (allpion[0, i] != null) objarr[allpion[0, i].xpos, allpion[0, i].ypos].GetComponent<Spots>().setpion(ref allpion[0, i]);
            if (allpion[1, i] != null) objarr[allpion[1, i].xpos, allpion[1, i].ypos].GetComponent<Spots>().setpion(ref allpion[1, i]);
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
        if (!isFrozen)
        {
            for (int j = 0; j < 5; j++)
            {
                if (kartoes[j].GetComponent<Card>() == c && j < 2)
                {
                    setkartupos();
                    indexkartu = j;
                    chosencard = c;
                    chosencard.transform.position = new Vector2(chosencard.transform.position.x, chosencard.transform.position.y + 1);
                    break;
                }
            }
            cekmoveavailable();
        }
    }

    void cekwin()
    {
        //AI win
        if (allpion[0, 2] != null && (allpion[1, 2] == null || (allpion[0, 2].xpos == 2 && allpion[0, 2].ypos == 4)))
        {
            textobj.GetComponent<Text>().color = Color.black;
            textobj.GetComponent<Text>().text = "AI Win";
            isFrozen = true;
        }//player win
        else if (allpion[1, 2] != null && (allpion[0, 2] == null || (allpion[1, 2].xpos == 2 && allpion[1, 2].ypos == 0)))
        {
            textobj.GetComponent<Text>().color = Color.black;
            textobj.GetComponent<Text>().text = "Player Win";
            isFrozen = true;
        }
    }

    public void pickspot(Spots s)
    {
        if (!isFrozen && s.GetComponent<SpriteRenderer>().color == green)
        {
            for (int i = 0; i < 5; i++)
            {
                if (allpion[1, i]!=null && chosenpapan.pion.xpos == allpion[1, i].xpos && chosenpapan.pion.ypos == allpion[1, i].ypos)
                {
                    s.setpion(ref allpion[1, i]);
                    break;
                }
            }
            //pindah
            chosenpapan.DestroyPion();
            kartoes = rotatecard(kartoes, indexkartu);
            setkartupos();
            newturn();
            cekwin();
        }

    }

    GameObject[] rotatecard(GameObject[] semuakartu, int i)
    {
        GameObject tempcard = semuakartu[i];
        semuakartu[i] = semuakartu[4]; // kartu rotation
        semuakartu[4] = tempcard;
        return semuakartu;
    }

    void newturn()
    {
        //ngebersihin
        chosenpapan = null;
        chosencard = null;
        //bersihin bg
        refreshpapan();
        Pions[,] peepee = new Pions[2, 5];
        //kalau langsung manggil allpion.clone nti isa berubah jadi murid masternya. Aku gatau gimana
        for (int i = 0; i < 5; i++)
        {
            if (allpion[0, i] != null) peepee[0, i] = new Pions(allpion[0, i].isKing, allpion[0, i].isP1, allpion[0, i].xpos, allpion[0, i].ypos);
            if (allpion[1, i] != null) peepee[1, i] = new Pions(allpion[1, i].isKing, allpion[1, i].isP1, allpion[1, i].xpos, allpion[1, i].ypos);
        }
        isFrozen = true;
        Debug.Log(minimaxmovement((GameObject[])kartoes.Clone(), (Pions[,])peepee.Clone(), 3, -100, 100, true));
        Debug.Log("Gerakan terbaik adalah pion " + enemypion + " dengan arah " + enemymove[0] + "x dan " + enemymove[1] + "y dengan kartu " + kartoes[enemycard].GetComponent<Card>().nama);
        //hancurkan pion secara kode
        for (int i = 0; i < 5; i++)
        {
            if (allpion[1, i]!=null && allpion[0, enemypion].xpos - enemymove[0] == allpion[1, i].xpos && allpion[0, enemypion].ypos + enemymove[1] == allpion[1, i].ypos)
            {
                //hapus pion secara visual
                objarr[allpion[1, i].xpos, allpion[1, i].ypos].DestroyPion(); 
                allpion[1, i] = null;
                break;
            }
        }
        objarr[allpion[0, enemypion].xpos, allpion[0, enemypion].ypos].GetComponent<Spots>().DestroyPion();
        allpion[0, enemypion].xpos = allpion[0, enemypion].xpos - enemymove[0];
        allpion[0, enemypion].ypos = allpion[0, enemypion].ypos + enemymove[1];
        objarr[allpion[0, enemypion].xpos, allpion[0, enemypion].ypos].setpion(ref allpion[0, enemypion]);
        kartoes = rotatecard(kartoes, enemycard);
        setkartupos();
        isFrozen = false;
        cekwin();
    }

    public void choosepion(int x, int y)
    {
        if (!isFrozen)
        {
            if (chosenpapan != null)
            {
                chosenpapan.GetComponent<SpriteRenderer>().color = grey;
            }
            chosenpapan = objarr[x, y];
            cekmoveavailable();
        }
    }

    public void eatpion(int x, int y)
    {
        if (chosenpapan != null && objarr[x, y].GetComponent<SpriteRenderer>().color == green && !isFrozen)
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
                if (allpion[0, i] != null && allpion[0, i].xpos == x && allpion[0, i].ypos == y)
                {
                    allpion[0, i] = null;
                    objarr[x, y].DestroyPion();
                    objarr[x, y].GetComponent<Spots>().setpion(ref allpion[1, index]);
                }
            }
            chosenpapan.DestroyPion();
            //muter kartu
            kartoes = rotatecard(kartoes, indexkartu);
            setkartupos();
            newturn();
            Debug.Log("dipilih papan di " + x + "," + y);
            refreshpapan();
            cekwin();
        }
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
                ytemp = y - arr[1]; ///krn + nti tambah ke bawah
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
        kartoes[0].transform.localScale = new Vector3(1f, 1f, 1f);
        kartoes[1].transform.position = new Vector2(4.5f, -2f);
        kartoes[1].transform.localScale = new Vector3(1f, 1f, 1f);
        kartoes[2].transform.position = new Vector2(-4.5f, 2f);
        kartoes[2].transform.localScale = new Vector3(-1f, -1f, 1f);
        kartoes[3].transform.position = new Vector2(4.5f, 2f);
        kartoes[3].transform.localScale = new Vector3(-1f, -1f, 1f);
        kartoes[4].transform.position = new Vector2(6.5f, 0);
        kartoes[4].transform.localScale = new Vector3(1f, 1f, 1f);
    }

    float minimaxmovement(GameObject[] kartus, Pions[,] semuapion, int dalam, float a, float b, bool maxingaiTurn)
    {
        if (dalam == 0)
        {
            //SBE nya
            float jarak = 0, jum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (semuapion[0, i] != null) jum++;
                if (semuapion[1, i] != null) jum--;
            }
            jum = Mathf.Pow(jum * 10, 2);
            if (semuapion[0, 2] != null && semuapion[1, 2] != null)
            {
                //Debug.Log(Mathf.Sqrt(Mathf.Pow(semuapion[0, 2].xpos - 2, 2) + Mathf.Pow(semuapion[0, 2].ypos - 4, 2))+" "+ Mathf.Sqrt(Mathf.Pow(semuapion[1, 2].xpos - 2, 2) + Mathf.Pow(semuapion[1, 2].ypos - 0, 2)));
                jarak = Mathf.Sqrt(Mathf.Pow(semuapion[0, 2].xpos - 2, 2) + Mathf.Pow(semuapion[0, 2].ypos - 4, 2));//ai
                jarak -= Mathf.Sqrt(Mathf.Pow(semuapion[1, 2].xpos - 2, 2) + Mathf.Pow(semuapion[1, 2].ypos - 0, 2)); //player
            }
            else
            {
                if (semuapion[1, 2] == null) jarak += 100;
                if (semuapion[0, 2] == null) jarak -= 100;
            }
            return jum + jarak;
        }

        //gerakan AI
        if (maxingaiTurn)
        {
            float eval, maxeval = -1000;
            bool prune = false;
            //untuk 5 pionnya
            for (int j = 0; j < 5; j++)
            {
                //kalau pion msh hidup
                if (semuapion[0, j] != null)
                {
                    Pions pion = semuapion[0, j];
                    //untuk semua gerakan di kartu pertama AI
                    for (int i = 2; i < 4; i++)
                    {
                        //Debug.Log("kartu ke - "+i);
                        foreach (int[] gerakan in kartus[i].GetComponent<Card>().gerakans)
                        {
                            //Debug.Log("gerakan x = " + gerakan[0]+ " y = " + gerakan[1]);
                            bool collide = false;
                            //cek collide ma temen
                            for (int e = 0; e < 5; e++)
                            {
                                if (semuapion[0, e] != null && pion.xpos - gerakan[0] == semuapion[0, e].xpos && pion.ypos + gerakan[1] == semuapion[0, e].ypos)
                                {
                                    collide = true;
                                    break;
                                }
                            }
                            if (pion.xpos - gerakan[0] >= 0 && pion.xpos - gerakan[0] < 5 && pion.ypos + gerakan[1] >= 0 && pion.ypos + gerakan[1] < 5 && !collide)
                            {
                                //setup papan
                                Pions[,] newsemuapion = new Pions[2, 5];
                                for (int c = 0; c < 5; c++)
                                {
                                    if (semuapion[0, c] != null) newsemuapion[0, c] = new Pions(semuapion[0, c].isKing, semuapion[0, c].isP1, semuapion[0, c].xpos, semuapion[0, c].ypos);
                                    if (semuapion[1, c] != null) newsemuapion[1, c] = new Pions(semuapion[1, c].isKing, semuapion[1, c].isP1, semuapion[1, c].xpos, semuapion[1, c].ypos);
                                }
                                //Debug.Log("Sebelum gerak : " + newsemuapion[0, j].xpos + "," + newsemuapion[0, j].ypos);
                                newsemuapion[0, j].xpos = pion.xpos - gerakan[0];
                                newsemuapion[0, j].ypos = pion.ypos + gerakan[1];
                                //Debug.Log("Hasil gerak : "+ newsemuapion[0, j].xpos+","+ newsemuapion[0, j].ypos);
                                //cek klo pion musuh kebunuh
                                for (int k = 0; k < 5; k++)
                                {
                                    if (newsemuapion[1, k] != null && newsemuapion[0, j].xpos == newsemuapion[1, k].xpos && newsemuapion[0, j].ypos == newsemuapion[1, k].ypos)
                                    {
                                        newsemuapion[1, k] = null;
                                        break;
                                    }
                                }
                                eval = minimaxmovement(rotatecard(kartus, i), newsemuapion, dalam - 1, a, b, false);
                                maxeval = Mathf.Max(eval, maxeval);
                                if (eval >= a)
                                {
                                    enemymove = gerakan;
                                    enemycard = i;
                                    enemypion = j;
                                }
                                a = Mathf.Max(eval, a);
                                //Debug.Log("Menggerakkan Pion AI ke - " + j + " dengan arah " + gerakan[0]*-1 + "x dan " + gerakan[1] + "y dengan kartu " + kartus[i].GetComponent<Card>().nama + " di depth " + dalam + " (a = " + a + ",b = " + b + ")");
                                if (b <= a)
                                {
                                    //Debug.Log("Ke prune?");
                                    prune = true;
                                    break;
                                }
                            }
                        }
                        if (prune)
                        {
                            //Debug.Log("Ke prunes");
                            break;
                        }
                    }
                }
                if (prune) break;
            }
            return maxeval;
        }
        else //gerakan Player
        {
            float eval, mineval = 1000;
            bool prune = false;
            //untuk 5 pionnya
            for (int j = 0; j < 5; j++)
            {
                //kalau pion msh hidup
                if (semuapion[1, j] != null)
                {
                    Pions pion = semuapion[1, j];
                    //untuk semua gerakan di kartu pertama AI
                    for (int i = 0; i < 2; i++)
                    {
                        //Debug.Log("kartu ke - "+i);
                        foreach (int[] gerakan in kartus[i].GetComponent<Card>().gerakans)
                        {
                            //Debug.Log("gerakan x = " + gerakan[0]+ " y = " + gerakan[1]);
                            bool collide = false;
                            //cek collide ma temen
                            for (int e = 0; e < 5; e++)
                            {
                                if (semuapion[1, e] != null && pion.xpos + gerakan[0] == semuapion[1, e].xpos && pion.ypos - gerakan[1] == semuapion[1, e].ypos)
                                {
                                    collide = true;
                                    break;
                                }
                            }
                            if (pion.xpos + gerakan[0] >= 0 && pion.xpos + gerakan[0] < 5 && pion.ypos - gerakan[1] >= 0 && pion.ypos - gerakan[1] < 5 && !collide)
                            {
                                //setup papan
                                Pions[,] newsemuapion = new Pions[2, 5];
                                for (int c = 0; c < 5; c++)
                                {
                                    if (semuapion[0, c] != null) newsemuapion[0, c] = new Pions(semuapion[0, c].isKing, semuapion[0, c].isP1, semuapion[0, c].xpos, semuapion[0, c].ypos);
                                    if (semuapion[1, c] != null) newsemuapion[1, c] = new Pions(semuapion[1, c].isKing, semuapion[1, c].isP1, semuapion[1, c].xpos, semuapion[1, c].ypos);
                                }
                                //Debug.Log("Sebelum gerak player : " + newsemuapion[1, j].xpos + "," + newsemuapion[1, j].ypos);
                                newsemuapion[1, j].xpos = pion.xpos + gerakan[0];
                                newsemuapion[1, j].ypos = pion.ypos - gerakan[1];
                                //Debug.Log("Hasil gerak player : " + newsemuapion[1, j].xpos + "," + newsemuapion[1, j].ypos);
                                //cek klo pion AI kebunuh
                                for (int k = 0; k < 5; k++)
                                {
                                    if (newsemuapion[0, k] != null && newsemuapion[1, j].xpos == newsemuapion[0, k].xpos && newsemuapion[1, j].ypos == newsemuapion[0, k].ypos)
                                    {
                                        newsemuapion[0, k] = null;
                                        break;
                                    }
                                }
                                eval = minimaxmovement(rotatecard(kartus, i), newsemuapion, dalam - 1, a, b, true);
                                mineval = Mathf.Min(eval, mineval);
                                b = Mathf.Min(eval, b);
                                //Debug.Log("Menggerakkan Pion Player ke - " + j + " dengan arah " + gerakan[0] * -1 + "x dan " + gerakan[1] + "y dengan kartu " + kartus[i].GetComponent<Card>().nama + " di depth " + dalam + " (a = " + a + ",b = " + b + ")");
                                if (b <= a)
                                {
                                    //Debug.Log("Ke prune player?");
                                    prune = true;
                                    break;
                                }
                            }
                        }
                        if (prune)
                        {
                            //Debug.Log("Ke prunes player");
                            break;
                        }
                    }
                }
                if (prune) break;
            }
            return mineval;
        }
    }
}
