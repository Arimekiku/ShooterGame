[System.Serializable]
public class PlayerWeaponInfo
{
    public float AttackSpeed { get; private set; }
    public int AttackDamage { get; private set; }

    public PlayerWeaponInfo(float attackSpeed, int attackDamage)
    {
        AttackSpeed = attackSpeed;
        AttackDamage = attackDamage;
    }
}