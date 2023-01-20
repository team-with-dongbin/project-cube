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
            Debug.Log("Interaction !");

            var pictureCube = _target.GetComponent<PictureCube>();
            if (pictureCube)
            {
                var cube = Inventory.instance.FindAnyCubeByColor(pictureCube.color);
                if (cube)
                {
                    Picture.instance.fitCube(pictureCube.gameObject, cube.gameObject);
                    Inventory.instance.RemoveItem(cube);
                }
            }
        }
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
                var pictureCube = _target.GetComponent<PictureCube>();
                if (pictureCube)
                {
                    pictureCube.DrawOutline();
                }
            }
            else
            {
                var pictureCube = _target.GetComponent<PictureCube>();
                if (pictureCube)
                {
                    pictureCube.EraseOutline();
                }
            }
        }
    }
}
