using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

// Interaction
public class InteractionController : MonoBehaviour
{
    [field: SerializeField]
    public float laycastRange { get; private set; } = 2.0f;

    [SerializeField]
    List<string> targetLayerName = new List<string>();

    [SerializeField]
    PlayerInput playerInput;

    int layerMask = 0;
    GameObject _target;
    InputAction interactionAction;

    void Awake()
    {
        foreach (var layerName in targetLayerName)
        {
            layerMask |= 1 << LayerMask.NameToLayer(layerName);
        }
        // Debug.Log(layerMask);
    }

    void Start()
    {
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
        interactionAction = playerInput.actions["InteractionInput"];
    }

    void Update()
    {
        UpdateTarget();
        if (interactionAction.triggered)
        {
            Interaction();
        }
    }

    void Interaction()
    {
        if (_target)
        {

            // Picture.
        }
        // target에는 Picture Cube가 있고,
        // 전역에서, picure를 불러오고.
        // inventory에서 같은 색의 큐브를 불러온다.
    }

    void UnsetTarget()
    {
        SetHighlightTarget(false);
        _target = null;
    }

    void SetTarget(GameObject target)
    {
        if (target != _target)
        {
            UnsetTarget();
            _target = target;
            SetHighlightTarget(true);
        }
    }

    void UpdateTarget()
    {
        RaycastHit hit;
        var camera = CameraController.instance.firstViewCamera.transform;
        int isSet = 0;

        if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, laycastRange, layerMask))
        {
            var target = hit.collider.gameObject;
            if (target.layer == LayerMask.NameToLayer("Picture"))
            {
                SetTarget(target);
                isSet = 1;
            }
        }

        if (isSet == 0) UnsetTarget();
    }

    void SetHighlightTarget(bool isHighlight)
    {
        if (_target)
        {
            if (isHighlight)
            {
                Picture.instance.DrawOutline(_target);
            }
            else
            {
                Picture.instance.EraseOutline(_target);
            }
        }
    }
}
