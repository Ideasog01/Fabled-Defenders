using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public HingeJoint2D hook;
    public GameObject linkPrefab;
    public int linksInt = 7;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
    }

    public void GenerateRope()
    {
        for(int i = 0; i < linksInt; i++)
        {
            Instantiate(linkPrefab, transform);
        }
    }
}
