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
}
