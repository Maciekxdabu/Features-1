using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientParts", menuName = "Scriptable Objects/ClientParts", order = 1)]
public class ClientParts : ScriptableObject
{
    [Header("Male parts")]
    public Sprite []MHairs;
    public Sprite []MHeads;
    public Sprite []MBodys;

    [Header("Female parts")]
    public Sprite []FHairs;
    public Sprite []FHeads;
    public Sprite []FBodys;
}
