using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public List<Image> AllImages;
    public List<GachaItem> OwnedItems;

    public GachaItem Pull()
    { 
        GachaItem newItem = new GachaItem();
        newItem.GachaImg = AllImages[Random.Range(0, AllImages.Count-1)];
        
        float rand = Random.Range(0, 100);
        if (rand <= 3)
        {
            newItem.Rarity = Rarity.Legendary;
        }
        else if (rand <= 10)
        {
            newItem.Rarity = Rarity.Ultimate;
        }
        else if (rand <= 30)
        {
            newItem.Rarity = Rarity.Rare;
        }
        else if (rand <= 65)
        {
            newItem.Rarity = Rarity.Uncommon;
        }
        else
        {
            newItem.Rarity = Rarity.Common;
        }

        return newItem;

    }
}
