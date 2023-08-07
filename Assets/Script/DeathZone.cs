using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public int damage = 2;
    public GameObject startPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // 1. �÷��̾��� HP�� ����Ʈ����
            player.OnHit(damage);
            // 2. �÷��̾ ���� �������� �ǵ�����
            collision.transform.position = startPoint.transform.position;

        }
    }
   
}
