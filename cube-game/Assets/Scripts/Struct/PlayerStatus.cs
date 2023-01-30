using System;
using System.Text;

[System.Serializable]
public class PlayerStatus
{
    public float health;
    public float attackPower;
    public float defense;
    public float movementSpeed;

    public override string ToString()
    {
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendFormat("health : {0} ", health);
        strBuilder.AppendFormat("attackPower : {0} ", attackPower);
        strBuilder.AppendFormat("defense : {0} ", defense);
        strBuilder.AppendFormat("movementSpeed : {0} ", movementSpeed);
        return strBuilder.ToString();
    }

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

    public static bool operator ==(PlayerStatus a, PlayerStatus b)
    {
        if (ReferenceEquals(a, b))
            return true;
        if (ReferenceEquals(a, null))
            return false;
        if (ReferenceEquals(b, null))
            return false;

        if (a.health == b.health && a.attackPower == b.attackPower
            && a.defense == b.defense && a.movementSpeed == b.movementSpeed)
            return true;
        else return false;
    }

    public static bool operator !=(PlayerStatus a, PlayerStatus b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        return this == (obj as PlayerStatus);
    }

    public override int GetHashCode()
    {
        return health.GetHashCode() ^ attackPower.GetHashCode() ^ defense.GetHashCode() ^ movementSpeed.GetHashCode();
    }
}
