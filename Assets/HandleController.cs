using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandleController : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction; // �͂ރA�N�V����
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.LeftHand; // ������w��
    public SteamVR_Behaviour_Pose controllerPose; // �R���g���[���[�̈ʒu�E��]���擾����R���|�[�l���g

    private bool isGrabbing = false; // �͂�ł��邩
    private float initialAngle; // �͂񂾂Ƃ��̏����p�x
    private Vector3 initialControllerDirection; // �͂񂾂Ƃ��̃R���g���[���[�̏�������

    void Update()
    {
        // �R���g���[���[���ݒ肳��Ă��Ȃ��ꍇ�̓G���[��h��
        if (controllerPose == null) return;

        // �͂ރ{�^���������ꂽ
        if (grabAction.GetStateDown(handType))
        {
            StartGrabbing();
        }
        // �͂ރ{�^���������ꂽ
        else if (grabAction.GetStateUp(handType))
        {
            StopGrabbing();
        }

        // ��]����
        if (isGrabbing)
        {
            RotateValve();
        }
    }

    private void StartGrabbing()
    {
        isGrabbing = true;

        // �o���u��Z������ɂ��������p�x���L�^
        initialAngle = transform.eulerAngles.z;

        // �͂񂾂Ƃ��̃R���g���[���[����o���u���S�ւ̕������L�^
        initialControllerDirection = controllerPose.transform.position - transform.position;
    }

    private void StopGrabbing()
    {
        isGrabbing = false;
    }

    private void RotateValve()
    {
        // ���݂̃R���g���[���[����o���u���S�ւ̕������擾
        Vector3 currentControllerDirection = controllerPose.transform.position - transform.position;

        // ���������ƌ��ݕ����̊p�x�����v�Z�iZ����]�Ɍ���j
        float angleDifference = Vector3.SignedAngle(initialControllerDirection, currentControllerDirection, Vector3.forward);

        // �o���u�̉�]���X�V
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            initialAngle + angleDifference
        );
    }

}