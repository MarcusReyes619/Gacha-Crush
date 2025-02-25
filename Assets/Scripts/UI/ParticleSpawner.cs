using UnityEngine;
using System;
using System.Collections.Generic;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particle;

    public void SpawnPartcle()
    {
        Instantiate(particle[0]);
    }
    public void SpawnPartcle(GachaItem item) 
    {
        switch (item.Rarity)
        {
            case Rarity.Common:
                break;
            case Rarity.Uncommon:
                break;
            case Rarity.Rare:
                break;
            case Rarity.Ultimate:
                break;
            case Rarity.Legendary:
                break;
        }
    }
}