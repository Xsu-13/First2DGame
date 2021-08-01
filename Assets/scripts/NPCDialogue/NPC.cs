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
        if(collision.CompareTag("Player"))
        {
            dialogueRunner.StartDialogue(yarnStartNode);
            dialog.AddSpeaker(speakerData);
        }
    }
}
