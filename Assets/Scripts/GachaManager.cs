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
    public PlayerData playerData;
    public Button pullButton;
    public AudioSource audioSource;
    public AudioClip rollSound;
    public AudioClip LegendarySound;

    private bool pulling = false;
    private float pt = 0;
    private float pc = 0;

    public List<GachaItem> commonItems;
    public List<GachaItem> uncommonItems;
    public List<GachaItem> rareItems;
    public List<GachaItem> ultimateItems;
    public List<GachaItem> legendaryItems;
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
                    if (item.Rarity == Rarity.Legendary)
                    {
                        audioSource.pitch = 1.0f;
                        audioSource.PlayOneShot(LegendarySound);

                    } 
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
        float rand = Random.Range(0f, 100f); // Generate a random number between 0-100

        List<GachaItem> selectedList = null;

        if (rand <= 3 && legendaryItems.Count > 0) // 3% chance for Legendary
        {
            selectedList = legendaryItems;
        }
        else if (rand <= 10 && ultimateItems.Count > 0) // 7% chance for Ultimate (10 - 3)
        {
            selectedList = ultimateItems;
        }
        else if (rand <= 30 && rareItems.Count > 0) // 20% chance for Rare (30 - 10)
        {
            selectedList = rareItems;
        }
        else if (rand <= 65 && uncommonItems.Count > 0) // 35% chance for Uncommon (65 - 30)
        {
            selectedList = uncommonItems;
        }
        else if (commonItems.Count > 0) // Remaining 35% chance for Common
        {
            selectedList = commonItems;
        }

        // Pick a random item from the selected rarity list
        GachaItem newItem = selectedList[Random.Range(0, selectedList.Count)];

        // Update UI
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
