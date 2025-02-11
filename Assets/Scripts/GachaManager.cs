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
    public Button pullButton;
    private bool pulling = false;
    private float pt = 0;
    private float pc = 0;

    private void Update()
    {
        if (pulling)
        {
            if (pt < 0)
            {
                Pull();
                pc--;
                if (pc < 0)
                {
                    pulling = false;
                    OwnedItems.Add(Pull());
                    pullButton.interactable = true;
                }
                else
                {
                    pt = 0.1f;
                }
            }
            pt -= Time.deltaTime;
        }
    }
    public void PullButton()
    {
        pulling = true;
        pullButton.interactable = false;
        pt = 0.1f;
        pc = 10;
    }
    public GachaItem Pull()
    {

        GachaItem newItem = ScriptableObject.CreateInstance<GachaItem>();
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
        return newItem;

    }
}
