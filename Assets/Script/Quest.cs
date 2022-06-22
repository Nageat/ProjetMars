using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public Image questItem;
    public Color CompletedColor;
    public Color ActiveColor;
    public Color currentColor;

    public QuestArrow arrow;

    public Quest[] allQuests;

    public void Start()
    {
        allQuests = FindObjectsOfType<Quest>();
        currentColor = questItem.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FinishQuest();
            Destroy(gameObject);
        }
    }

    void FinishQuest()
    {
        questItem.GetComponent<Button>().interactable = false;
        currentColor = CompletedColor;
        questItem.color = CompletedColor;
        arrow.gameObject.SetActive(false);
    }

    public void OnQuestClick()
    {
        arrow.gameObject.SetActive(true);
        arrow.target = this.transform;
        foreach(Quest quest in allQuests)
        {
            quest.questItem.color = quest.currentColor;
        }
        questItem.color = ActiveColor;
    }

}
