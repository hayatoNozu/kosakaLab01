using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //InteractUI�{�^����������Ă�̂��𔻒肷�邽�߂�Iui�Ƃ����֐���SteamVR_Actions.default_InteractUI���Œ�
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    //���ʂ̊i�[�pBoolean�^�֐�interacrtui
    private Boolean interacrtui;

    public int maxReflections = 5;                   // �ő唽�ˉ�
    public float maxLaserDistance = 100f;            // ���[�U�[�̍ő勗��
    public LayerMask reflectLayerMask;               // ���ˑΏۂ̃��C���[
    public Material[] laserMaterials;                // �p�ӂ������[�U�[�p�̃}�e���A���z��

    private LineRenderer lineRenderer;

    void Start()
    {
        Application.targetFrameRate = 60;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;               // ���[�U�[���\���ɐݒ�

        if (laserMaterials.Length > 0)
        {
            lineRenderer.material = laserMaterials[0];  // �����}�e���A����ݒ�
        }
        else
        {
            Debug.LogWarning("���[�U�[�}�e���A�����ݒ肳��Ă��܂���B");
        }
    }

    void Update()
    {
        interacrtui = Iui.GetState(SteamVR_Input_Sources.RightHand);
        // �{�^����������Ă���ԁA���t���[���o�H���X�V
        if (interacrtui)
        {
            GenerateLaserPath();                    // ���[�U�[�̌o�H�𐶐�
            lineRenderer.enabled = true;            // ���[�U�[��\��
        }
        else
        {
            lineRenderer.enabled = false;           // �{�^���𗣂����烌�[�U�[���\��
        }
    }

    void GenerateLaserPath()
    {
        // ���[�U�[�̋N�_�ƕ���
        Vector3 startPosition = transform.position;
        Vector3 direction = transform.forward;

        List<Vector3> laserPoints = new List<Vector3>();
        laserPoints.Add(startPosition);

        int reflections = 0;
        while (reflections < maxReflections)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction, out hit, maxLaserDistance, reflectLayerMask))
            {
                laserPoints.Add(hit.point);

                // �ǂ̖@���Ɋ�Â��Ĕ��˕������v�Z
                Vector3 surfaceNormal = hit.normal;
                direction = Vector3.Reflect(direction, surfaceNormal);
                startPosition = hit.point;

                // ���ˉ񐔂ɉ����ă}�e���A����ύX�i��: ���˂��邽�тɈقȂ�F�ɕύX�j
                if (laserMaterials.Length > reflections)
                {
                    lineRenderer.material = laserMaterials[reflections]; // ���ˉ񐔂ɉ������}�e���A����ݒ�
                }

                reflections++;
            }
            else
            {
                // ���ˉ񐔂̏���ɒB������A�ő勗���܂ŐL�΂�
                laserPoints.Add(startPosition + direction * maxLaserDistance);
                break;
            }
        }

        // LineRenderer��ݒ�
        lineRenderer.positionCount = laserPoints.Count;
        lineRenderer.SetPositions(laserPoints.ToArray());
    }
}
