using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    public Transform handleTransform;
    private float previousAngle = 0f;
    public float rotationEnergyMultiplier = 1f;
    public float generatedEnergy = 0f;

    void Update()
    {
        float currentAngle = handleTransform.localEulerAngles.z; // �n���h���̉�]�p�x���擾
        float deltaAngle = Mathf.DeltaAngle(previousAngle, currentAngle); // ��]�p�̕ω���
        generatedEnergy += Mathf.Abs(deltaAngle) * rotationEnergyMultiplier; // �G�l���M�[�����Z
        previousAngle = currentAngle;
    }
}