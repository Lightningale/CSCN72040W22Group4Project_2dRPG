using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
   /* public Slider healthbar;
    public IDamagable subject;
    bool display;
    // Start is called before the first frame update
    void Start()
    {
        healthbar=GetComponent<Slider>();
        healthbar.maxValue=GetComponentInParent<Enemy>().maxHP;
        healthbar.value=healthbar.maxValue;
        display=false;
        transform.localScale=new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth()
    {
        
        if(!display)
        {
            transform.localScale=new Vector3(1,1,1);
            display=true;
        }
        healthbar.value=GetComponentInParent<IDamagable>().health;
    }*/
}
