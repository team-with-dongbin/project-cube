using UnityEngine;
using UnityEngine.Events;

public class StatusController : MonoBehaviour
{
    public static StatusController Instance;
    // 현재 HP
    public float Health { get; private set; }

    // 최대 HP, 아무런 버프가 없을 때의 Status
    public PlayerStatus PlayerNormalStatus
    {
        get
        {
            return PhysicalStatus + InventoryStatus;
        }
    }

    // 사용자가 임의로 올린 Stat이 반영되지 않은 Status
    [field: SerializeField]
    public PlayerStatus OriginalStauts { get; private set; }

    // 레벨업 등에서 사용자가 임의로 Stat을 올린 것들을 반영한, Player 자체적으로 가지고 있는 Status
    public PlayerStatus PhysicalStatus { get; private set; }

    // Inventory의 Item에서 statusChanging의 합
    public PlayerStatus InventoryStatus { get; private set; }
    public UnityEvent OnPlayerDie = new UnityEvent();

    private void Awake()
    {
        Instance = new StatusController();
        PhysicalStatus = new PlayerStatus(OriginalStauts);
        InventoryStatus = new PlayerStatus();
        InitiailizeHP();
    }

    private void Start()
    {
        Inventory.instance.OnInventoryStatusChanged.AddListener(SetInventoryStatus);
    }

    public void InitiailizeHP()
    {
        Health = PlayerNormalStatus.health;
    }

    public void SetPhysicalStatus(PlayerStatus physicalStatus)
    {
        Damage(physicalStatus.health - this.PhysicalStatus.health);
        this.PhysicalStatus = physicalStatus;
    }

    public void SetInventoryStatus(PlayerStatus inventoryStatus)
    {
        Damage(inventoryStatus.health - this.InventoryStatus.health);
        this.InventoryStatus = inventoryStatus;
    }

    public void Damage(float diff, bool CanDie = false)
    {
        float temp = Health;
        temp -= diff;

        if (CanDie == false)
        {
            temp = Mathf.Max(1f, temp);
        }

        Health = temp;

        if (Health <= 0f)
        {
            OnPlayerDie.Invoke();
        }
    }
}
