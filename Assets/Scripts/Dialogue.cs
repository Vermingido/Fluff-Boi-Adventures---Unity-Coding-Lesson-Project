using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public List<string> dialogueList;
    private TextMeshProUGUI text;
    private GameObject shop;
    private int currentDialogue = -1;
    
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void AdvanceDialogue()
    {
        currentDialogue+=1;
        if((byte)currentDialogue >= dialogueList.Count)
        {
            currentDialogue = -1;
            if (shop != null)
            {
                shop.gameObject.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            text.SetText(dialogueList[currentDialogue]);
        }
    }

    public void SetShop(GameObject shop)
    {
        Debug.Log("Opened Shop");
        this.shop = shop;
    }
}
