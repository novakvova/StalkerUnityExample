using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Aspects
{
    AssaultRifle,
    Pistol,
    Grenade,
    Knife,
    Additional
};

public abstract class AbstractObject : MonoBehaviour
{
    public string name;
    public Aspects Aspect;
    public GameObject Prefab;
    [HideInInspector] public bool Equiped = false;
    public int BulletsCount, AllBulletsCount;

    public abstract void AddBullets(int b);
}
