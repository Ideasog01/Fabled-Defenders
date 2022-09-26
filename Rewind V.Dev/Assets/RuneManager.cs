using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneManager : MonoBehaviour
{
    public string sentence;

    public Text[] runeText;
    public Image[] runeSymbol;
    public Sprite[] runeSprite;

    private Text inputText;

    public bool[] hasLetter;

    private int numberOfChars;

    private GameObject runeCanvasMinigame;

    public Sprite emptySprite;

    public string[] sentences;
    public int numberOfRunes;

    public GameObject currentRuneTablet;


    private void Start()
    {
        runeCanvasMinigame = GameObject.Find("RuneTabletPar");
        
        sentence = "the king lives";
        
        inputText = GameObject.Find("InputTextField").GetComponent<Text>();

        foreach (Image rune in runeSymbol)
        {
            rune.enabled = false;
        }
        foreach (Text letter in runeText)
        {
            letter.text = " ";
        }
        CloseRuneTablet();
    }

    public void ShowRuneTablet()
    {
        Cursor.visible = true;
        PlayerAttack.disableAttack = true;
        GameObject.Find("Player").GetComponent<PlayerMovement_R>().stunned = true;
        sentence = sentences[Random.Range(0, sentences.Length)];
        SetSentence();
        runeCanvasMinigame.transform.localScale = new Vector3(1, 1, 1);
        Time.timeScale = 0;
    }

    public void CloseRuneTablet()
    {
        Time.timeScale = 1;
        PlayerProperties.gold += Random.Range(1, 3);
        Cursor.visible = false;
        GameObject.Find("Player").GetComponent<PlayerMovement_R>().stunned = false;
        PlayerAttack.disableAttack = false;
        runeCanvasMinigame.transform.localScale = new Vector3(0, 0, 0);
    }

    public void RecieveRandomRune()
    {
        int randomRuneNum = Random.Range(0, hasLetter.Length);
        if(hasLetter[randomRuneNum] == true)
        {
            return;
        }
        else
        {
            hasLetter[randomRuneNum] = true;
            numberOfRunes += 1;
            GameObject.Find("CollectionsManager").GetComponent<CollectionsManager>().RunesCollected();
        }
    }

    

    public void EnterText()
    {
        if(inputText.text.ToString() == sentence)
        {
            Debug.Log("Success");
            GameObject.Find("AbilityManager").GetComponent<AbilityManager>().ChargeFocus(30);
            currentRuneTablet.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log(sentence);
            CloseRuneTablet();

        }
        else
        {
            Debug.Log("Fail");
        }
    }

    public void SetSentence()
    {
        numberOfChars = 0;

        foreach (Image rune in runeSymbol)
        {
            rune.enabled = false;
        }

        foreach (Text letter in runeText)
        {
            letter.text = " ";
        }
        foreach (char character in sentence.ToCharArray())
        {
            
            if(character.ToString() == "a")
            {
                if(hasLetter[0])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[0];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
               
            }
            if (character.ToString() == "b")
            {
                if(hasLetter[1])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[1];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "c")
            {
                if(hasLetter[2])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[2];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "d")
            {
                if(hasLetter[3])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[3];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "e")
            {
                if(hasLetter[4])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[4];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "f")
            {
                if(hasLetter[5])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[5];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "g")
            {
                if(hasLetter[6])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[6];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "h")
            {
                if(hasLetter[7])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[7];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "i")
            {
                if(hasLetter[8])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[8];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "j")
            {
                if(hasLetter[9])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[9];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "k")
            {
                if(hasLetter[10])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[10];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "l")
            {
                if(hasLetter[11])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[11];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "m")
            {
                if(hasLetter[12])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[12];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "n")
            {
                if(hasLetter[13])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[13];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "o")
            {
                if(hasLetter[14])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[14];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "p")
            {
                if(hasLetter[15])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[15];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "q")
            {
                if(hasLetter[16])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].sprite = runeSprite[16];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "r")
            {
                if(hasLetter[17])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[17];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "s")
            {
                if(hasLetter[18])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[18];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "t")
            {
                if(hasLetter[19])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[19];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "u")
            {
                if(hasLetter[20])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[20];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "v")
            {
                if(hasLetter[21])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[21];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == "w")
            {
                if (hasLetter[22])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[22];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "x")
            {
                if(hasLetter[23])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[23];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "y" && hasLetter[24] == true)
            {
                if(hasLetter[24])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[24];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
            }
            if (character.ToString() == "z")
            {
                if(hasLetter[25])
                {
                    runeText[numberOfChars].text = character.ToString();
                    numberOfChars += 1;
                }
                else
                {
                    runeSymbol[numberOfChars].enabled = true;
                    runeSymbol[numberOfChars].sprite = runeSprite[25];
                    runeText[numberOfChars].text = null;
                    numberOfChars += 1;
                }
                
            }
            if (character.ToString() == " ")
            {
                runeSymbol[numberOfChars].sprite = emptySprite;
                runeText[numberOfChars].text = " ";
                numberOfChars += 1;
            }
        }

        foreach (Image rune in runeSymbol)
        {
            if (rune.sprite == null)
            {
                rune.enabled = false;
            }
        }

        foreach (Text letter in runeText)
        {
            if (letter.text == null)
            {
                letter.text = " ";
            }
        }




    }






}
