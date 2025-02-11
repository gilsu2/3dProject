using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer; // Skinned Mesh Renderer�� �Ҵ��մϴ�.
    private int flyBlendShapeIndex; // Fly ��������� �ε��� ����
    public float animationSpeed = 200f; // ��������� �ִϸ��̼� �ӵ� ����

    private float currentWeight = 0f;
    private bool increasing = true;

    public float moveDistance = 5f;   // ���� �̵��� �Ÿ�
    public float moveSpeed = 2f;      // �̵� �ӵ�
    private Vector3 startPosition;    // ���� ��ġ ����
    private Vector3 targetPosition;   // ��ǥ ��ġ ����
    private bool movingForward = true; // �̵� ����

    void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        flyBlendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("Fly");

        if (flyBlendShapeIndex == -1)
        {
            Debug.LogError("'Fly'��� BlendShape�� ã�� �� �����ϴ�.");
        }

        startPosition = transform.position;
        targetPosition = startPosition + transform.forward * moveDistance;
    }

    void Update()
    {
        AnimateBlendShape(); // �ִϸ��̼� ��Ʈ��
        MoveBee(); // �̵� ��Ʈ��
    }

    void AnimateBlendShape()
    {
        if (increasing)
        {
            currentWeight += animationSpeed * Time.deltaTime;
            if (currentWeight >= 100f)
            {
                currentWeight = 100f;
                increasing = false;
            }
        }
        else
        {
            currentWeight -= animationSpeed * Time.deltaTime;
            if (currentWeight <= 0f)
            {
                currentWeight = 0f;
                increasing = true;
            }
        }

        skinnedMeshRenderer.SetBlendShapeWeight(flyBlendShapeIndex, currentWeight);
    }

    void MoveBee()
    {
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                movingForward = false;
                // ���� ���Ƽ� �� ���� ��ȯ (180�� ȸ��)
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                movingForward = true;
                // �ٽ� ���Ƽ� �� ���� ��ȯ (���� �������� ȸ��)
                transform.Rotate(0f, 180f, 0f);
            }
        }
    }
}
