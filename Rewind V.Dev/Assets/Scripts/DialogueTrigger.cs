using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject DialogueManager;

    [SerializeField]
    private bool isTriggered;

    private GameObject player;

    private GameObject keyToQuarters;
    private GameObject questManager;
    private GameObject childOfNixia;
    private GameObject Zar;

    [SerializeField]
    private Quest tutorialQuest;

    private void Start()
    {
        questManager = GameObject.Find("QuestManager");
        DialogueManager = GameObject.Find("DialogueManager");
        player = GameObject.Find("Player");
        
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().sender = this.gameObject;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        DialogueManager.GetComponent<DialogueManager>().sender = this.gameObject;
    }

    private void Update()
    {

        if(Vector2.Distance(this.transform.position, player.transform.position) < 2.5f && !isTriggered)
        {
            TriggerDialogue();
            isTriggered = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            GameObject.Find("PortalToFarhaven").GetComponent<DoorTeleport>().TeleportPlayer();
        }

    
        
    }

    public void DialogueEndedBehav()
    {
        if(dialogue.eventName == "MariamBogD2")
        {
            questManager.GetComponent<QuestManager>().AddQuest(GameObject.Find("CavernsQuest").GetComponent<QuestProperties>().quest);
        }

        if(dialogue.eventName == "MariamBogD1")
        {
            this.GetComponent<EnemyBehav>().DialogueFinished();
            GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().Zar.SetActive(false);
        }

        if(dialogue.eventName == "MariamD1")
        {
            questManager.GetComponent<QuestManager>().AddQuest(GameObject.Find("BogQuest").GetComponent<QuestProperties>().quest);
            
        }
        if(dialogue.eventName == "DarkShadowD3")
        {
            GameObject.Find("PortalToFarhaven").GetComponent<DoorTeleport>().TeleportPlayer();
            Destroy(GameObject.Find("Nilerm"));
            Cursor.visible = false;
            GameObject.Find("NilermWatchManager").GetComponent<NilermWatchManager>().NilermDialogueFinished();
        }

        if (dialogue.eventName == "DarkShadowD1")
        {
            this.GetComponentInChildren<ParticleSystem>().Play();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("CorruptionSound");
            GameObject.Find("DarkShadow1").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DarkShadow1").transform.GetChild(0).gameObject.SetActive(false);
        }



        if (dialogue.eventName == "KnightDialogue")
        {
            GameObject.Find("NilermWatchManager").GetComponent<NilermWatchManager>().KnightDialogueFinished();
        }

        if(dialogue.eventName == "OthelDialogue")
        {
            GameObject.Find("CorruptionManager").GetComponent<CorruptionManager>().EncounterReady();
        }

        if (dialogue.eventName == "SarD1")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Save();
            GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().SpawnDragonBoss();
            this.gameObject.SetActive(false);
        }

        if(dialogue.eventName == "SarD2")
        {
            FindObjectOfType<PlayerProperties>().keyCodes.Add("4444");
        }

        if(dialogue.eventName == "TutorialD1")
        {
            questManager.GetComponent<QuestManager>().AddQuest(GameObject.Find("TutorialQuest").GetComponent<QuestProperties>().quest);
        }

        if(dialogue.eventName == "StrangerD1")
        {
            questManager.GetComponent<QuestManager>().AddQuest(GameObject.Find("StrangerQuest").GetComponent<QuestProperties>().quest);
        }
    }

    public void ActivateEvent()
    {
        if (DialogueManager.GetComponent<DialogueManager>().elementNumber == 4 && dialogue.eventName == "DarkShadowD3")
        {
            GameObject.Find("NilermVFX").GetComponent<ParticleSystem>().Play();
            GameObject.Find("DS2VFX").GetComponent<ParticleSystem>().Play();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("CorruptionSound");
            GameObject.Find("DarkShadow2").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("DarkShadow2").transform.GetChild(0).gameObject.SetActive(false);
        }

        if (DialogueManager.GetComponent<DialogueManager>().elementNumber == 3 && dialogue.eventName == "SarD1")
        {
            GameObject.Find("CameraPar").GetComponent<CameraShake>().Shake();
        }

        if (DialogueManager.GetComponent<DialogueManager>().elementNumber == 2 && dialogue.eventName == "MariamD1")
        {
            GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().Zar.SetActive(true);
        }

        if (DialogueManager.GetComponent<DialogueManager>().elementNumber == 4 && dialogue.eventName == "SarD1")
        {
            GameObject.Find("CameraPar").GetComponent<CameraShake>().Shake();
        }
      




    } 

    
}
