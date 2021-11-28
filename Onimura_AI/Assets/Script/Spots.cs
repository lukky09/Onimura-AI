using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spots : MonoBehaviour
{
    public int x, y;
    public GameObject currpion = null;
    public GameObject board;

    public void setpion(Pions p)
    {
        if (p.isKing)
        {
            currpion = Instantiate(Resources.Load<GameObject>("Master"), transform);
        }
        else
        {
            currpion = Instantiate(Resources.Load<GameObject>("murid"), transform);
        }
        if (p.isP1)
        {
            currpion.GetComponent<SpriteRenderer>().color = new Color(50f / 255f, 123f / 255f, 238f / 255f);
        }
        else
        {
            currpion.GetComponent<SpriteRenderer>().color = new Color(238f / 255f, 50f / 255f, 123f / 255f);
        }
    }

    private void OnMouseDown()
    {
        if(currpion!= null)
        {
            board.GetComponent<Board>().choosepion(x, y);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
