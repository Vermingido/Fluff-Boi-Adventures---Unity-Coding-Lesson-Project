using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    private Image barImage;
    
    private void Awake()
    {
        barImage = GetComponent<Image>();
    }
    private void Update()
    {
        barImage.fillAmount = (float)currentHP / maxHP;
    }
}
