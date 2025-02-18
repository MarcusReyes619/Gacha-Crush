using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public Image border;
    public Image gachaImage;
    public TMP_Text rarityText;
    public List<Sprite> AllImages = new List<Sprite>();
    public List<Sprite> AllBorders = new List<Sprite>();
    public PlayerData playerData;
    public Button pullButton;
    public AudioSource audioSource;
    public AudioClip rollSound;
    public AudioClip LegendarySound;

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
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(rollSound);
                
                if (pc < 0)
                {
                    pulling = false;
                    GachaItem item = Pull();
                   
                    playerData.OwnedItems.Add(item);
                    if (item.Rarity == Rarity.Legendary) audioSource.PlayOneShot(LegendarySound);
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
        newItem.GachaImg = AllImages[Random.Range(0, AllImages.Count)];

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
        rarityText.text = newItem.Rarity.ToString();
        return newItem;
    }

    public void OpenCollection()
    {
        SceneManager.LoadScene("Collection_Scene");
    }
}
