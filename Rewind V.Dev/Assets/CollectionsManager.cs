using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionsManager : MonoBehaviour
{
    private GameObject collections;
    private GameObject questScroll;
    private GameObject runeScroll;
    private GameObject miracleScroll;


    public Text[] RuneTranslation;
    public Text numberOfRunesCollected;

    private void Start()
    {
        collections = GameObject.Find("CollectionsBackground");
        questScroll = GameObject.Find("QuestsScroll");
        runeScroll = GameObject.Find("RunesScroll");
        miracleScroll = GameObject.Find("MiraclesScroll");
        questScroll.transform.localScale = new Vector3(0, 0, 0);
        runeScroll.transform.localScale = new Vector3(0, 0, 0);
        miracleScroll.transform.localScale = new Vector3(0, 0, 0);
        CloseCollections();
        RunesCollected();
    }
    public void CloseCollections()
    {
        collections.transform.localScale = new Vector3(1, 1, 1);
        collections.transform.localScale = new Vector3(0, 0, 0);
        FindObjectOfType<PlayerMovement_R>().stunned = false;
        PlayerAttack.disableAttack = false;
        Cursor.visible = false;
    }

    public void OpenCollections()
    {
        collections.transform.localScale = new Vector3(0, 0, 0);
        collections.transform.localScale = new Vector3(1, 1, 1);
        questScroll.transform.localScale = new Vector3(0, 0, 0);
        runeScroll.transform.localScale = new Vector3(0, 0, 0);
        miracleScroll.transform.localScale = new Vector3(0, 0, 0);
        FindObjectOfType<PlayerMovement_R>().stunned = true;
        PlayerAttack.disableAttack = true;
        Cursor.visible = true;
    }

    

    public void ShowQuests()
    {
        questScroll.transform.localScale = new Vector3(1, 1, 1);
        runeScroll.transform.localScale = new Vector3(0, 0, 0);
        miracleScroll.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowLetters()
    {
        questScroll.transform.localScale = new Vector3(0, 0, 0);
        runeScroll.transform.localScale = new Vector3(0, 0, 0);
        miracleScroll.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowRunes()
    {
        questScroll.transform.localScale = new Vector3(0, 0, 0);
        runeScroll.transform.localScale = new Vector3(1, 1, 1);
        miracleScroll.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowMiracles()
    {
        questScroll.transform.localScale = new Vector3(0, 0, 0);
        runeScroll.transform.localScale = new Vector3(0, 0, 0);
        miracleScroll.transform.localScale = new Vector3(1, 1, 1);
    }


    public void RunesCollected()
    {
        numberOfRunesCollected.text = GameObject.Find("RuneManager").GetComponent<RuneManager>().numberOfRunes.ToString() + "/26";
        foreach(Text rune in RuneTranslation)
        {
            if(rune == RuneTranslation[0] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[0])
            {
                rune.text = "a";
            }
            if (rune == RuneTranslation[1] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[1])
            {
                rune.text = "b";
            }
            if (rune == RuneTranslation[2] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[2])
            {
                rune.text = "c";
            }
            if (rune == RuneTranslation[3] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[3])
            {
                rune.text = "d";
            }
            if (rune == RuneTranslation[4] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[4])
            {
                rune.text = "e";
            }
            if (rune == RuneTranslation[5] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[5])
            {
                rune.text = "f";
            }
            if (rune == RuneTranslation[6] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[6])
            {
                rune.text = "g";
            }
            if (rune == RuneTranslation[7] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[7])
            {
                rune.text = "h";
            }
            if (rune == RuneTranslation[8] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[8])
            {
                rune.text = "i";
            }
            if (rune == RuneTranslation[9] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[9])
            {
                rune.text = "j";
            }
            if (rune == RuneTranslation[10] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[10])
            {
                rune.text = "k";
            }
            if (rune == RuneTranslation[11] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[11])
            {
                rune.text = "l";
            }
            if (rune == RuneTranslation[12] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[12])
            {
                rune.text = "m";
            }
            if (rune == RuneTranslation[13] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[13])
            {
                rune.text = "n";
            }
            if (rune == RuneTranslation[14] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[14])
            {
                rune.text = "o";
            }
            if (rune == RuneTranslation[15] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[15])
            {
                rune.text = "p";
            }
            if (rune == RuneTranslation[16] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[16])
            {
                rune.text = "q";
            }
            if (rune == RuneTranslation[17] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[17])
            {
                rune.text = "r";
            }
            if (rune == RuneTranslation[18] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[18])
            {
                rune.text = "s";
            }
            if (rune == RuneTranslation[19] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[19])
            {
                rune.text = "t";
            }
            if (rune == RuneTranslation[20] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[20])
            {
                rune.text = "u";
            }
            if (rune == RuneTranslation[21] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[21])
            {
                rune.text = "v";
            }
            if (rune == RuneTranslation[22] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[22])
            {
                rune.text = "w";
            }
            if (rune == RuneTranslation[23] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[23])
            {
                rune.text = "x";
            }
            if (rune == RuneTranslation[24] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[24])
            {
                rune.text = "y";
            }
            if (rune == RuneTranslation[25] && GameObject.Find("RuneManager").GetComponent<RuneManager>().hasLetter[25])
            {
                rune.text = "z";
            }
        }
    }
}
