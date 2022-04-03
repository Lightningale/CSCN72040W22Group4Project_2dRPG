using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class CharStatsUI : MonoBehaviour
{
    // Start is called before the first frame update
    //public GlobalController globalController;
    public List<Vector3> AvatarPositions;
    public List<GameObject> Avatars;
    public List<GameObject> Healthbars;
    public List<GameObject> Energybars;
    public Vector3 Avatar1Pos=new Vector3(0,0,0);
    public Vector3 Avatar2Pos=new Vector3(-38,-32,0);
    bool updateAvatarPos=false;
    void Start()
    {
        AvatarPositions.Add(new Vector3(0,0,0));
        AvatarPositions.Add(new Vector3(-38,-32,0));
        
    }

    // Update is called once per frame
    void Update()
    {
  
        for(int i=0;i<Avatars.Count;i++)
        {
            Avatars[i].GetComponent<RectTransform>().anchoredPosition=Vector3.MoveTowards(Avatars[i].GetComponent<RectTransform>().localPosition,AvatarPositions[i],400f*Time.deltaTime);
       //     Healthbars[i].GetComponent<RectTransform>().anchoredPosition=new Vector3(-528f+1120f*((float)globalController.characterList[i].health/(float)globalController.characterList[i].maxHealth),42,0);
       //     Energybars[i].GetComponent<RectTransform>().anchoredPosition=new Vector3(-882f+882f*((float)globalController.characterList[i].energy/(float)globalController.characterList[i].maxEnergy),0,0);
            
        }  
     
    }
    public void changeMainCharacter()
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
    }
}
