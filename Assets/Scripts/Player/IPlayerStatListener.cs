using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStatListener
{
    void UpdatePlayerData(int health,int maxHealth,int mana,int maxMana,int level,int exp);
}
