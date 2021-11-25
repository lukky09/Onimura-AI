using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public ArrayList gerakans = new ArrayList();

    public Card(string nama)
    {
        gerakans.Add(new int[] { -1, 1 });
        gerakans.Add(new int[] { -1, -1 });
        gerakans.Add(new int[] { 1, 1 });
        gerakans.Add(new int[] { 1, -1 });
    }
}
