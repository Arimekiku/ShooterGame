using UnityEngine;

[System.Serializable]
public class PlayerWeaponInfo
{
    [SerializeField] private float WeaponAttackSpeed;
    [SerializeField] private int WeaponAttackDamage;

    public float AttackSpeed => WeaponAttackSpeed;
    public int AttackDamage => WeaponAttackDamage;

    public PlayerWeaponInfo(float attackSpeed, int attackDamage)
    {
        WeaponAttackSpeed = attackSpeed;
        WeaponAttackDamage = attackDamage;
    }
}