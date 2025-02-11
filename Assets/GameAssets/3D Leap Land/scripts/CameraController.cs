using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;          // ���� �÷��̾��� Transform
    public Vector3 offset = new Vector3(0f, 4f, -4f);  // �÷��̾���� �Ÿ� (ž�ٿ� ��)
    public float smoothSpeed = 0.125f;  // ī�޶� ���󰡴� �ӵ� (�ε巯�� ���� ����)

    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // ��ǥ ��ġ ��� (�÷��̾� ��ġ + ������)
        Vector3 targetPosition = player.transform.position + offset;

        // ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵� (Lerp ���)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // ī�޶� ��ġ ������Ʈ
        transform.position = smoothedPosition;

        // �÷��̾ �׻� �ٶ󺸵��� ���� (������ ������ �ּ� ó�� ����)
        transform.LookAt(player.transform);
    }
}