using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;

public class DialogUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    DialogueRunner dialogueRunner;
    string speaker;
    string language;
    string en = "en";
    
    void Start()
    {
        dialogueRunner = GetComponent<DialogueRunner>();
        language = dialogueRunner.textLanguage;
        dialogueRunner.AddCommandHandler("SetSpeaker", SetSpeakerInfo);
    }

    
    void Update()
    {
        
    }

    public void AddSpeaker(SpeakerData speakerData)
    {
        speaker = speakerData.speakerName;
    }

    public void SetSpeakerInfo(string[] info)
    {
        if(language == en)
        {
            nameText.text = info[1];
        }
        else
        {
            nameText.text = info[0];
        }
        
    }
}
