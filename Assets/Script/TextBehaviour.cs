using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;


public class TextBehaviour : PlayableBehaviour
{
    public string text; 
    public bool dialog = false;
    public float fullTimeOfText = 2f; 

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var textMesh = playerData as TextMeshProUGUI;
        textMesh.richText = true; 
        if(dialog)
        {
            // given the current time, determine how much of the string will be displayed
            var progress = (float) (playable.GetTime() / fullTimeOfText);
            int subStringLength = Mathf.RoundToInt(Mathf.Clamp01(progress) * text.Length);
            textMesh.text = text.Substring(0, subStringLength);
        }
        else
        {
            textMesh.text = text; 
            textMesh.color = new Color(1,1,1, info.weight);
        }
    }
}