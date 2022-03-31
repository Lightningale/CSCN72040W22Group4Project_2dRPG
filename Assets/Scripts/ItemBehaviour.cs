using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //using abstract factory design pattern for weapons
    public interface Item
    {
        //item fields:

        //string name   (name of item)
        //int cost      (price of item)
        //string effect (description of what the item does)
    }

    public abstract class ProductionLine
    {
        public abstract Item createItem();
    }

    public class Java : Item
    {
        string name = "Java";
        int cost = 20;
        string effect = "A cup of Java - recover 20 mana";
    }

    public class HotJava : Item
    {
        string name = "Hot Java";
        int cost = 35;
        string effect = "A cup of hot Java - recover 40 mana";
    }

    public class Slides : Item
    {
        string name = "Lesson Slides";
        int cost = 10;
        string effect = "A copy of the lesson slides - improve GPA by 10";
    }

    public class Notes : Item
    {
        string name = "Lesson Notes";
        int cost = 20;
        string effect = "Handwritten lesson notes - improve GPA by 25";
    }
}
