using UnityEngine;
using Valve.VR.InteractionSystem;

public class Handle : MonoBehaviour
{
    private float oldZRotation; // ��]��Z���݂̂�ۑ�
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
    private Interactable interactable;

    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            // Z����]��ۑ�
            oldZRotation = transform.localEulerAngles.z;

            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);

            hand.HoverUnlock(interactable);

            // ��]��Z���̂݌��ɖ߂�
            Vector3 currentRotation = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, oldZRotation);
        }
    }
}