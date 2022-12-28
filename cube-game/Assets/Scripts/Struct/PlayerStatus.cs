[System.Serializable]
public class PlayerStatus
{
    public float health;
    public float attackPower;
    public float defense;
    public float movementSpeed;

    public PlayerStatus()
    {
        health = 0f;
        attackPower = 0f;
        defense = 0f;
        movementSpeed = 0f;
    }

    public PlayerStatus(PlayerStatus newStatus)
    {
        health = newStatus.health;
        attackPower = newStatus.attackPower;
        defense = newStatus.defense;
        movementSpeed = newStatus.movementSpeed;
    }

    public static PlayerStatus operator +(PlayerStatus a, PlayerStatus b)
    {
        PlayerStatus playerStatus = new PlayerStatus();
        playerStatus.health = a.health + b.health;
        playerStatus.attackPower = a.attackPower + b.attackPower;
        playerStatus.defense = a.defense + b.defense;
        playerStatus.movementSpeed = a.movementSpeed + b.movementSpeed;

        return playerStatus;
    }

    public static PlayerStatus operator -(PlayerStatus a, PlayerStatus b)
    {
        PlayerStatus playerStatus = new PlayerStatus();
        playerStatus.health = a.health - b.health;
        playerStatus.attackPower = a.attackPower - b.attackPower;
        playerStatus.defense = a.defense - b.defense;
        playerStatus.movementSpeed = a.movementSpeed - b.movementSpeed;

        return playerStatus;
    }

    public static PlayerStatus operator -(PlayerStatus a)
    {
        PlayerStatus playerStatus = new PlayerStatus();
        playerStatus.health = -a.health;
        playerStatus.attackPower = -a.attackPower;
        playerStatus.defense = -a.defense;
        playerStatus.movementSpeed = -a.movementSpeed;

        return playerStatus;
    }
}
