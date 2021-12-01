using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite[] gambars;
    public GameObject papan;
    public ArrayList gerakans = new ArrayList();
    Sprite dissprite;
    public string nama;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = dissprite;
    }

    public void setcard(int i)
    {
        string name="";
        switch (i)
        {
            case 0:
                name = "boar";
                gerakans.Add(new int[] { -1, 0 });
                gerakans.Add(new int[] { 1, 0 });
                gerakans.Add(new int[] { 0, 1 });
                break;
            case 1:
                name = "cobra";
                gerakans.Add(new int[] { -1, 0 });
                gerakans.Add(new int[] { 1, 1 });
                gerakans.Add(new int[] { 1, -1 });
                break;
            case 2:
                name = "crab";
                gerakans.Add(new int[] { -2, 0 });
                gerakans.Add(new int[] { 2, 0 });
                gerakans.Add(new int[] { 0, 1 });
                break;
            case 3:
                name = "crane";
                gerakans.Add(new int[] { 1, -1 });
                gerakans.Add(new int[] { -1, -1 });
                gerakans.Add(new int[] { 0, 1 });
                break;
            case 4:
                name = "dragon";
                gerakans.Add(new int[] { -2, 1 });
                gerakans.Add(new int[] { -1, -1 });
                gerakans.Add(new int[] { 1, -1 });
                gerakans.Add(new int[] { 2, 1 });
                break;
            case 5:
                name = "eel";
                gerakans.Add(new int[] { -1, 1 });
                gerakans.Add(new int[] { -1, -1 });
                gerakans.Add(new int[] { 1, 0 });
                break;
            case 6:
                name = "elephant";
                gerakans.Add(new int[] { -1, 0 });
                gerakans.Add(new int[] { -1, 1 });
                gerakans.Add(new int[] { 1, 0 });
                gerakans.Add(new int[] { 1, 1 });
                break;
            case 7:
                name = "frog";
                gerakans.Add(new int[] { -2, 0 });
                gerakans.Add(new int[] { -1, 1 });
                gerakans.Add(new int[] { 1, -1 });
                break;
            case 8:
                name = "goose";
                gerakans.Add(new int[] { -1, 1 });
                gerakans.Add(new int[] { -1, 0 });
                gerakans.Add(new int[] { 1, 0 });
                gerakans.Add(new int[] { 1, -1 });
                break;
            case 9:
                name = "horse";
                gerakans.Add(new int[] { -1, 0 });
                gerakans.Add(new int[] { 0, -1 });
                gerakans.Add(new int[] { 0, 1 });
                break;
            case 10:
                name = "mantis";
                gerakans.Add(new int[] { -1, 1 });
                gerakans.Add(new int[] { 1, 1 });
                gerakans.Add(new int[] { 0, -1 });
                break;
            case 11:
                name = "monkeh";
                gerakans.Add(new int[] { -1, 1 });
                gerakans.Add(new int[] { -1, -1 });
                gerakans.Add(new int[] { 1, 1 });
                gerakans.Add(new int[] { 1, -1 });
                break;
            case 12:
                name = "ox";
                gerakans.Add(new int[] { 1, 0 });
                gerakans.Add(new int[] { 0, 1 });
                gerakans.Add(new int[] { 0, -1 });
                break;
            case 13:
                name = "rabbit";
                gerakans.Add(new int[] { -1, -1 });
                gerakans.Add(new int[] { 1, 1 });
                gerakans.Add(new int[] { 2, 0 });
                break;
            case 14:
                name = "rooster";
                gerakans.Add(new int[] { -1, -1 });
                gerakans.Add(new int[] { -1, 0 });
                gerakans.Add(new int[] { 1, 0 });
                gerakans.Add(new int[] { 1, 1 });
                break;
            case 15:
                name = "tiger";
                gerakans.Add(new int[] { 0, -1 });
                gerakans.Add(new int[] { 0, 2 });
                break;
        }
        nama = name;
        dissprite = gambars[i];
        this.GetComponent<SpriteRenderer>().sprite = dissprite;
    }

    private void OnMouseDown()
    {
        papan.GetComponent<Board>().changekartu(this);
    }

}
