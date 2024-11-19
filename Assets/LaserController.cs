using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float laserSpeed = 5f; // ���[�U�[�̊g�呬�x
    public GameObject ancar;

    private float maxScaleY = 10f; // �ő�X�P�[��Y�i���[�U�[���ǂꂾ�������Ȃ邩�j
    private bool isColliding = false;

    void Update()
    {
        if (!isColliding)
        {
            // ���[�U�[���g�傷��
            float newScaleY = ancar.transform.localScale.y + laserSpeed * Time.deltaTime;

            // �ő�X�P�[���𒴂��Ȃ��悤�ɒ���
            if (newScaleY > maxScaleY)
                newScaleY = maxScaleY;

            ancar.transform.localScale = new Vector3(ancar.transform.localScale.x, newScaleY, ancar.transform.localScale.z);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }
}
