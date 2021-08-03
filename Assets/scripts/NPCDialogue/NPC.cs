using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject chat;
    [SerializeField] string yarnStartNode = "Start";
    [SerializeField] YarnProgram yarnDialogue;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] SpeakerData speakerData;
    [SerializeField] DialogUI dialog;
    bool start = true;
    void Start()
    {
        chat.SetActive(false);
        dialogueRunner.Add(yarnDialogue);
        //dialog.AddSpeaker(speakerData);
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && start)
            {
                dialogueRunner.StartDialogue(yarnStartNode);
                dialog.AddSpeaker(speakerData);
                start = false;
            }
        }
    }
}
