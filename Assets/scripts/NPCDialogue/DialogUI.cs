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
    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = GetComponent<DialogueRunner>();
        dialogueRunner.AddCommandHandler("SetSpeaker", SetSpeakerInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpeaker(SpeakerData speakerData)
    {
        speaker = speakerData.speakerName;
    }

    public void SetSpeakerInfo(string[] info)
    {
        nameText.text = info[0];
    }
}
