using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

public class TextClip : PlayableAsset
{
    [TextArea]
    public string text; 
    public bool dialog;
    public float secondsForText; 


    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextBehaviour>.Create(graph);

        TextBehaviour textBehaviour = playable.GetBehaviour(); 
        textBehaviour.text = text;
        textBehaviour.dialog = dialog;
        textBehaviour.fullTimeOfText = secondsForText;

        return playable;
    }
}
