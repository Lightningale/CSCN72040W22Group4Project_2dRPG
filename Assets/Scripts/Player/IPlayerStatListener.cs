using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStatListener
{
    // Start is called before the first frame update
    void UpdatePlayerData(int health,int maxHealth,int mana,int maxMana,int level,int exp);
}
