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
    public void SpawnPartcle(GachaItem rarity) 
    {
        
    }
}
