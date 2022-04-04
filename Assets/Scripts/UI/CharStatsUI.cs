using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
public class CharStatsUI : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
  
            health=player.currentHealth;
            maxHealth=player.maxHealth;
            level=player.level;
            mana=player.currentMana;
            maxMana=player.maxMana;
            //Avatar.GetComponent<RectTransform>().anchoredPosition=Vector3.MoveTowards(Avatar.GetComponent<RectTransform>().localPosition,AvatarPositions[i],400f*Time.deltaTime);
            levelText.text="LV/"+level.ToString();
            Healthbar.GetComponent<RectTransform>().anchoredPosition=new Vector3(-528f+1120f*((float)health/maxHealth),42,0);
            Energybar.GetComponent<RectTransform>().anchoredPosition=new Vector3(-882f+882f*((float)mana/(float)maxMana),0,0);
    }
    /*public void changeMainCharacter()
    {
        
        updateAvatarPos=true;
        var temp=Avatars[0];
        for(int i=0;i<Avatars.Count-1;i++)
        {
            Avatars[i]=Avatars[i+1];
            Avatars[i].GetComponent<SortingGroup>().sortingOrder=Avatars.Count-i;//Sorting order not working for reasons unknown. changing hierarchy order here instead
            Avatars[i].transform.SetSiblingIndex(Avatars.Count-i-1);
        }
        Avatars[Avatars.Count-1]=temp;
        Avatars[Avatars.Count-1].GetComponent<SortingGroup>().sortingOrder=0;
        Avatars[Avatars.Count-1].transform.SetSiblingIndex(0);
    }*/
}
