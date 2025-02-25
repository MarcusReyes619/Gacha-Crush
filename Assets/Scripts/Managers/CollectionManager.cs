
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    List<GachaItem> items = new List<GachaItem>();
    [SerializeField] GameObject gridLayout;
    [SerializeField] GameObject ItemBase;
    [SerializeField] PlayerData playerData;
    void Start()
    {
        fillFields();
    }

    void fillFields()
    {
        foreach (GachaItem item in playerData.OwnedItems)
        {
            GameObject go = Instantiate(ItemBase);
            go.transform.SetParent(gridLayout.transform);
            go.GetComponentsInChildren<Image>()[0].sprite = item.Border;
            go.GetComponentsInChildren<Image>()[2].sprite = item.GachaImg;
        }
    }
    public void OpenGacha()
    {
        SceneManager.LoadScene("Gacha_Scene");
    }
}
