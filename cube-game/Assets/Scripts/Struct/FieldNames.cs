public partial class ItemData
{
    public enum FieldNames
    {
        // ItemData
        id,
        itemName,
        prefab,
        icon,
        itemType,

        // CubeData
        color,
        statusChanging,
        initialHp,
        strikeSound,
        destroySound,

        // EquipmentData
        partDefense,
        durability,
        equipmentType,

        // GunData
        shotClip,
        reloadClip,
        bullet,
        bulletSpeed,
        initialAmmoRemain,
        magCapacity,
        timeBetFire,
        reloadTime,

        // KnifeData
        swingClip,
        attackRange,

        // WeaponData
        damage
    }
}
