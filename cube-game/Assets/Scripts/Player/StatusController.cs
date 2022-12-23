using UnityEngine;

public class StatusController : MonoBehaviour
{
    public PlayerStatus nowStatus { get; private set; }
    public PlayerStatus originalStatus { get; private set; }

    [field: SerializeField]
    public PlayerStatus basicStatus { get; private set; }
    public PlayerStatus inventoryStatus { get; private set; }

    private void Awake()
    {
        originalStatus = new PlayerStatus(basicStatus);
        RecoverStatus();
    }

    public void RecoverStatus()
    {
        nowStatus = new PlayerStatus(originalStatus);
    }

    public void SetBasicStatus(PlayerStatus basicStatus)
    {
        ApplyStatusDiff(this.basicStatus, basicStatus);

        this.basicStatus = basicStatus;
        this.originalStatus = this.inventoryStatus + this.basicStatus;
    }

    public void SetInventoryStatus(PlayerStatus inventoryStatus)
    {
        ApplyStatusDiff(this.inventoryStatus, inventoryStatus);

        this.inventoryStatus = inventoryStatus;
        this.originalStatus = this.inventoryStatus + this.basicStatus;
    }

    private void ApplyStatusDiff(PlayerStatus before, PlayerStatus after)
    {
        var diffOriginalStatus = after - before;
        nowStatus += diffOriginalStatus;

        nowStatus.health = Mathf.Max(nowStatus.health, 1f);
        nowStatus.movementSpeed = Mathf.Max(nowStatus.movementSpeed, 0f);
    }
}
