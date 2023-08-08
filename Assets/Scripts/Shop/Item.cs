using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        HealthPotion,
        FireBullet,
        WaterBullet,
        AcidBullet
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:
                return 100;
            case ItemType.FireBullet:
                return 500;
            case ItemType.WaterBullet:
                return 1000;
            case ItemType.AcidBullet:
                return 1500;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:
                return GameAssets.i.healthPotion;
            case ItemType.FireBullet:
                return GameAssets.i.fireBullet;
            case ItemType.WaterBullet:
                return GameAssets.i.waterBullet;
            case ItemType.AcidBullet:
                return GameAssets.i.acidBullet;
        }
    }
}
