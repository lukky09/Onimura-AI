using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spots : MonoBehaviour
{
    public int x, y;
    public GameObject currpionobj = null;
    public Pions pion = null;
    public GameObject board;

    public void setpion(Pions p)
    {
        if (p.isKing)
        {
            currpionobj = Instantiate(Resources.Load<GameObject>("Master"), transform);
        }
        else
        {
            currpionobj = Instantiate(Resources.Load<GameObject>("murid"), transform);
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
    }

    public void DestroyPion()
    {
        Destroy(currpionobj);
        currpionobj = null;
        pion = null;
    }

    private void OnMouseDown()
    {
        if(currpionobj != null && pion.isP1)
        {
            board.GetComponent<Board>().choosepion(x, y);
        }else if (currpionobj == null)
        {
            board.GetComponent<Board>().pickspot(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
