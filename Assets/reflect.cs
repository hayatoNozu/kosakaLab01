using UnityEngine;

public class reflect : MonoBehaviour
{
    public ParticleSystem particleSystem;  // �Ώۂ̃p�[�e�B�N���V�X�e��
    public GameObject reflectionPrefab;    // ���ˎ��ɏo���V�����p�[�e�B�N���̃v���n�u

    private void Start()
    {
        // �p�[�e�B�N���V�X�e���̏Փ˂�L���ɂ���
        var collisionModule = particleSystem.collision;
        collisionModule.enabled = true;
        collisionModule.collidesWith = LayerMask.GetMask("Default"); // �ՓˑΏۂ̃��C���[���w��
        collisionModule.type = ParticleSystemCollisionType.World;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����ʒu�Ɩ@�����g���Ĕ��˕������v�Z
        Vector3 hitPoint = collision.contacts[0].point;
        Vector3 hitNormal = collision.contacts[0].normal;

        // ���˕������v�Z
        Vector3 reflectionDirection = Vector3.Reflect(particleSystem.transform.forward, hitNormal);

        // �V�����p�[�e�B�N���𔽎˕����ɔ���������
        SpawnReflectedParticle(hitPoint, reflectionDirection);
    }

    private void SpawnReflectedParticle(Vector3 position, Vector3 direction)
    {
        // ���˕����Ƀp�[�e�B�N���𐶐�
        var reflectedParticle = Instantiate(reflectionPrefab, position, Quaternion.identity);
        var particleSystem = reflectedParticle.GetComponent<ParticleSystem>();

        // �p�[�e�B�N���̐i�s������ݒ�
        var mainModule = particleSystem.main;
        mainModule.startRotation = Quaternion.LookRotation(direction).eulerAngles.y;
        particleSystem.Play();
    }
}
