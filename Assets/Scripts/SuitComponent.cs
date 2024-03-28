using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SuitComponent
{
    public SuitSlot slot;
    public Mesh mesh;
    public bool isUnlocked;
}

public enum SuitSlot
{
    Head,
    Chest,
    Emblem,
    RightArm,
    LeftArm,
    RightLeg,
    LeftLeg
}

public struct SuitSet
{
    public SuitComponent head, chest, emblem, rightarm, leftarm, rightleg, leftleg;
    public string name;
}
