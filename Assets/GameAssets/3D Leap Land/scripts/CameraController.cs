using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;                         // ���� �÷��̾��� Transform
    public Vector3 offset = new Vector3(0f, 4f, -4f); // �÷��̾���� �Ÿ�
    public float smoothSpeed = 0.125f;               // ī�޶� ���󰡴� �ӵ�
    public float movementThreshold = 0.05f;          // �̼��� �����ӿ� ���� �Ӱ谪

    private Vector3 lastTargetPosition;              // ���� �÷��̾� ��ġ ����
    private float initialXRotation = 0f;                  // �ʱ� X�� ȸ�� �� ����

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        // �ʱ� �÷��̾� ��ġ ����
        lastTargetPosition = player.position;

        // �ʱ� X�� ȸ�� �� ����
        initialXRotation = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        // ��ǥ ��ġ ��� (�÷��̾� ��ġ + ������)
        Vector3 targetPosition = player.position + offset;
        float distanceMoved = Vector3.Distance(lastTargetPosition, targetPosition);

        if (distanceMoved > movementThreshold)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // ������ ��ġ ������Ʈ
            lastTargetPosition = player.position;
        }

        // �÷��̾ �ٶ󺸵� X�� ȸ���� ����
        Vector3 currentRotation = transform.eulerAngles;
        transform.LookAt(player);
        if (initialXRotation == 0f)
        {
            initialXRotation = transform.eulerAngles.x;
        }

        // X�� ȸ���� �ʱⰪ���� ����
        transform.rotation = Quaternion.Euler(initialXRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
