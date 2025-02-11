using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    public SkinnedMeshRenderer springMeshRenderer;  // ������ �޽��� SkinnedMeshRenderer
    private int stretchedBlendShapeIndex;           // 'Stretched' BlendShape �ε���
    public float stretchSpeed = 20f;                 // �þ�� �ӵ�
    public float maxStretch = 100f;                 // �ִ� ��������� �� (���� �þ ����)
    public float recoverySpeed = 2f;                // ���� ���·� ���ƿ��� �ӵ�

    private bool playerOnSpring = false;            // �÷��̾ ������ ���� �ִ��� ����
    private float currentStretch = 0f;              // ���� ������ ��������� ��

    public float launchForce = 500f;                // �÷��̾ �߻��� ��
    private bool isLaunching = false;               // �ߺ� ���� ���� �÷���

    void Start()
    {
        springMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        stretchedBlendShapeIndex = springMeshRenderer.sharedMesh.GetBlendShapeIndex("Stretched");

        if (stretchedBlendShapeIndex == -1)
        {
            Debug.LogError("'Stretched'��� BlendShape�� ã�� �� �����ϴ�.");
        }
    }

    void Update()
    {
        if (playerOnSpring)
        {
            // �÷��̾ �ö��� �� ��������� ���� �ø���
            currentStretch = Mathf.Lerp(currentStretch, maxStretch, Time.deltaTime * stretchSpeed);
            if (currentStretch > 99)
            {
                playerOnSpring = false;
            }
        }
        else
        {
            // �÷��̾ �������� ������� ���ƿ���
            currentStretch = Mathf.Lerp(currentStretch, 0f, Time.deltaTime * recoverySpeed);
        }

        // ��������� �� ����
        springMeshRenderer.SetBlendShapeWeight(stretchedBlendShapeIndex, currentStretch);
    }

    // �÷��̾ ������ ���� �ö���� �� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            playerOnSpring = true;
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.up * launchForce);
        }
    }    
}
