using System;
using UnityEngine;
[Serializable]
public class PlayerStatusXP
{
    [Header("レベルと経験値のパラメータ")]
    [SerializeField] int _level;
    [SerializeField] int _experience;
    public int Level { get => _level;  set => _level = value;}
    public int Experience { get => _experience; set => _experience = value;}
}