using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveSlotDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private Image spriteRenderer;
    public Sprite emptySprite,savedSprite;
    public int saveEntry;
    void Start()
    {
        spriteRenderer=GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SaveDataHandler.GetInstance().SlotEmpty(saveEntry))
            spriteRenderer.sprite=emptySprite;
        else
            spriteRenderer.sprite=savedSprite;
    }
}
