using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<string> characterName;

    public Dialogue dialogue;

    private Text dialogueText;
    private Text characterNameText;
    public GameObject sender;

    private GameObject dialogueBox;
    
    private GameObject dialogueCharacterPort;
    private GameObject characterHeadSprite;

    public Sprite NilermHead;
    public Sprite ShadowHead;
    public Sprite LadyHead;
    public Sprite strangerHead;
    public Sprite sarHead;
    public Sprite mariamHead;
    public Sprite zarHead;

    public int elementNumber;

    
    

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        characterName = new Queue<string>();
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueCharacterPort = GameObject.Find("CharacterPortrait");
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        characterNameText = GameObject.Find("CharacterNameText").GetComponent<Text>();
        characterHeadSprite = GameObject.Find("CharacterHead");
        dialogueBox.SetActive(false);
        dialogueCharacterPort.SetActive(false);

    }

    private void Update()
    {

        if (dialogueText.text.ToString().Contains("$layer"))
        {
            dialogueText.text = dialogueText.text.ToString().Replace("$layer", GameManager.playerName);
        }
    }



    public void StartDialogue(Dialogue dialogue)
    {
        PlayerAttack.disableAttack = true;
        FindObjectOfType<PlayerMovement_R>().stunned = true;
        Cursor.visible = true;
        sentences.Clear();
        characterName.Clear();
        Debug.Log(dialogueText.text.ToString());
        dialogueBox.SetActive(true);
        dialogueCharacterPort.SetActive(true);
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialogue.characterName)
        {
            characterName.Enqueue(name);
        }
        elementNumber = -1; 

        DisplayNextSentence();

        


    }
    public void DisplayNextSentence()
    {
        elementNumber += 1;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        
        

        string sentence = sentences.Dequeue();
        string name = characterName.Dequeue();



        StopAllCoroutines();
        characterNameText.text = name;
        StartCoroutine(TypeSentence(sentence));

       


        if (name.ToString() == "Nilerm The Knowing")
        {
            characterHeadSprite.GetComponent<Image>().sprite = NilermHead;
        }

        if (name.ToString() == "Dark Shadow")
        {
            characterHeadSprite.GetComponent<Image>().sprite = ShadowHead;
        }

        if (name.ToString() == "Lady Dark")
        {
            characterHeadSprite.GetComponent<Image>().sprite = LadyHead;
        }

        if(name.ToString() == "Stranger")
        {
            characterHeadSprite.GetComponent<Image>().sprite = strangerHead;
        }

        if(name.ToString() == "Sar Tarlar")
        {
            characterHeadSprite.GetComponent<Image>().sprite = sarHead;
        }

        if (name.ToString() == "Mariam KnightSlayer")
        {
            characterHeadSprite.GetComponent<Image>().sprite = mariamHead;
        }

        if (name.ToString() == "Zar Varlar")
        {
            characterHeadSprite.GetComponent<Image>().sprite = zarHead;
        }

        sender.GetComponent<DialogueTrigger>().ActivateEvent();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        sender.GetComponent<DialogueTrigger>().DialogueEndedBehav();
        FindObjectOfType<PlayerMovement_R>().stunned = false;
        Debug.Log("End of Dialogue");
        PlayerAttack.disableAttack = false;
        Cursor.visible = false;
        dialogueBox.SetActive(false);
        dialogueCharacterPort.SetActive(false);

    }
}

