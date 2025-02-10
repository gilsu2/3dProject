using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;          // ���� �÷��̾��� Transform
    public Vector3 offset = new Vector3(0f, 10f, -10f);  // �÷��̾���� �Ÿ� (ž�ٿ� ��)
    public float smoothSpeed = 0.125f;  // ī�޶� ���󰡴� �ӵ� (�ε巯�� ���� ����)

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        // ��ǥ ��ġ ��� (�÷��̾� ��ġ + ������)
        Vector3 targetPosition = player.position + offset;

        // ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵� (Lerp ���)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // ī�޶� ��ġ ������Ʈ
        transform.position = smoothedPosition;

        // �÷��̾ �׻� �ٶ󺸵��� ���� (������ ������ �ּ� ó�� ����)
        transform.LookAt(player);
    }
}