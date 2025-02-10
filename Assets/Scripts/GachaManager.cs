using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public Image border;
    public Image gachaImage;
    public List<Sprite> AllImages = new List<Sprite>();
    public List<Sprite> AllBorders = new List<Sprite>();
    public List<GachaItem> OwnedItems = new List<GachaItem>();

    public void Pull()
    {
        Debug.Log("a");

        GachaItem newItem = new GachaItem();
        newItem.GachaImg = AllImages[Random.Range(0, AllImages.Count-1)];
        
        float rand = Random.Range(0, 100);
        if (rand <= 3)
        {
            newItem.Rarity = Rarity.Legendary;
            newItem.Border = AllBorders[4];
        }
        else if (rand <= 10)
        {
            newItem.Rarity = Rarity.Ultimate;
            newItem.Border = AllBorders[3];
        }
        else if (rand <= 30)
        {
            newItem.Rarity = Rarity.Rare;
            newItem.Border = AllBorders[2];
        }
        else if (rand <= 65)
        {
            newItem.Rarity = Rarity.Uncommon;
            newItem.Border = AllBorders[1];
        }
        else
        {
            newItem.Rarity = Rarity.Common;
            newItem.Border = AllBorders[0];
        }

        border.sprite = newItem.Border;
        gachaImage.sprite = newItem.GachaImg;
        OwnedItems.Add(newItem);


    }
}
