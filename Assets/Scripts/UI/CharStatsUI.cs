using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
public class CharStatsUI : MonoBehaviour,IPlayerStatListener
{
    // Start is called before the first frame update
    //public GlobalController globalController;
    //public Vector3 AvatarPosition;
   // public GameObject Avatar;
    public GameObject Healthbar;
    public GameObject Energybar;
    public TextMeshProUGUI levelText;
    private int maxHealth,maxExp,maxMana;
    private int health,exp,mana,level;
    private Player player;
    void Start()
    {
        player=FindObjectOfType<Player>() as Player;
        
    }
    void Update()
    {
  /*
            health=player.currentHealth;
            maxHealth=player.maxHealth;
            level=player.level;
            mana=player.currentMana;
            maxMana=player.maxMana;
            //Avatar.GetComponent<RectTransform>().anchoredPosition=Vector3.MoveTowards(Avatar.GetComponent<RectTransform>().localPosition,AvatarPositions[i],400f*Time.deltaTime);
            levelText.text="LV/"+level.ToString();
            Healthbar.GetComponent<RectTransform>().anchoredPosition=new Vector3(-528f+1120f*((float)health/maxHealth),42,0);
            Energybar.GetComponent<RectTransform>().anchoredPosition=new Vector3(-882f+882f*((float)mana/(float)maxMana),0,0);*/
    }
    public void UpdateDisplay()
    {
            levelText.text="LV/"+level.ToString();
            Healthbar.GetComponent<RectTransform>().anchoredPosition=new Vector3(-528f+1120f*((float)health/maxHealth),42,0);
            Energybar.GetComponent<RectTransform>().anchoredPosition=new Vector3(-882f+882f*((float)mana/(float)maxMana),0,0);
    }
    public void UpdatePlayerData(int health,int maxHealth,int mana,int maxMana,int level,int exp)
    {
        this.health=health;
        this.maxHealth=maxHealth;
        this.mana=mana;
        this.maxMana=maxMana;
        this.level=level;
        this.exp=exp;
        UpdateDisplay();
    }
   
}
