using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spots : MonoBehaviour
{
    public int x, y;
    public GameObject currpionobj = null;
    public Pions pion = null;
    public GameObject board;
    public GameObject[] murid;

    public void setpion(ref Pions p)
    {
        if (p.isKing)
        {
            currpionobj = Instantiate(murid[1], transform);
        }
        else
        {
            currpionobj = Instantiate(murid[0], transform);
        }
        if (p.isP1)
        {
            currpionobj.GetComponent<SpriteRenderer>().color = new Color(50f / 255f, 123f / 255f, 238f / 255f);
        }
        else
        {
            currpionobj.GetComponent<SpriteRenderer>().color = new Color(238f / 255f, 50f / 255f, 123f / 255f);
        }
        pion = p;
        pion.xpos = x;
        pion.ypos = y;
    }

    public void DestroyPion()
    {
        if (currpionobj != null)
        {
            Destroy(currpionobj);
        }
        currpionobj = null;
        pion = null;
    }

    private void OnMouseDown()
    { 
        if (currpionobj != null && pion.isP1)
        {
            board.GetComponent<Board>().choosepion(x, y);
        }
        else if (currpionobj != null && !pion.isP1)
        {
            board.GetComponent<Board>().eatpion(x, y);
        }
        else if (currpionobj == null)
        {
            board.GetComponent<Board>().pickspot(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

}
