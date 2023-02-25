using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    Arrow
}
public class BuyItem : MonoBehaviour
{
    public itemType type;
   public int quantity;
   public int price;

   public void buy()
   {
        Debug.Log("Bought " + quantity + " " + type + " for " + price);
   }

}
