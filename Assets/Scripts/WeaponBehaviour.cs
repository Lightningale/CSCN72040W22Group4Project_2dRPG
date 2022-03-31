using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    //using abstract factory design pattern for weapons
    public interface Weapon
    {
        //weapon fields:

        //string name   (name of the weapon)
        //int attack    (damage the weapon does)
    }

    public abstract class ProductionLine
    {
        public abstract Weapon createWeapon();
    }

    public class SwordOne : Weapon
    {
        string name = "SwordOne";
        int attack = 5;
    }

    public class KnightSword : Weapon
    {
        string name = "Knight's Sword";
        int attack = 3;
    }

    public class WoodStaff : Weapon
    {
        string name = "Wooden Staff";
        int attack = 1;
    }

    public class SpikedStaff : Weapon
    {
        string name = "Spiked Staff";
        int attack = 4;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
