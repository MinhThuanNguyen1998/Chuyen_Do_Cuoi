using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewChapterDialogue", menuName = "Game/Chapter Dialogue Data")]
public class DialogueDataSO : ScriptableObject
{
    [System.Serializable]
    public struct DialogueItem
    {
        public int stepIndex;                   
        public Sprite avatarSprite;                
        [TextArea(3, 5)] public string textContent; 
        public AudioClip voiceAudio;             
    }
    public int chapterNumber;                    
    public List<DialogueItem> dialogueList = new List<DialogueItem>();
}
