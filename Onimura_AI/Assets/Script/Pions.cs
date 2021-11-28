using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pions
{
    public bool isP1,isKing;
    public int xpos, ypos;


    public Pions(bool isKing,bool isP1, int xpos, int ypos)
    {
        this.isKing = isKing;
        this.isP1 = isP1;
        this.xpos = xpos;
        this.ypos = ypos;
    }
}
