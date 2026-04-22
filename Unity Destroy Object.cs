using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(XRGrabInteractable))]
public class DestroyOnContact : MonoBehaviour
{
    private XRGrabInteractable _grabInteractable;
    private bool _isHeld = false;

    public string targetTag = "";

    void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        _grabInteractable.selectEntered.AddListener(OnGrab);
        _grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        _grabInteractable.selectEntered.RemoveListener(OnGrab);
        _grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        _isHeld = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        _isHeld = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHeld) return;
        if (collision.gameObject == this.gameObject) return;
        if (!string.IsNullOrEmpty(targetTag) && !collision.gameObject.CompareTag(targetTag)) return;

        Destroy(collision.gameObject);
    }
}
