using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pions : MonoBehaviour
{
    bool isP1;
    int xpos, ypos;


    public Pions(bool isP1, int xpos, int ypos)
    {
        this.isP1 = isP1;
        this.xpos = xpos;
        this.ypos = ypos;
    }
}
