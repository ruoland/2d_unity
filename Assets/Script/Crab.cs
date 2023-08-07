using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Crab : MonoBehaviour
{
    private int currentHP = 100;
    public int maxHP = 100;

    public int damage = 1;

    public float moveSpeed = 1.5f;
    public List<Vector3> targetVectors = new List<Vector3>();
    private int currentTaget = 0;
    private bool isBackward = false;
    private bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isWalk", true);
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit == false)
        {
            Move();
        }
    }

    private void Move()
    {
        // ��ǥ�������� �����ϴ� �ڵ�
        Vector3 dir = new Vector3();
        // ��ǥ�������� ��ġ�� �����ϴ� �ڵ� X�ุ
        dir.x = Vector3.MoveTowards(transform.position, targetVectors[currentTaget], moveSpeed * Time.deltaTime).x;
        // Y���� ���� ��ġ �״��
        dir.y = transform.position.y;

        // ���� ��ġ���� ����� ������ ġȯ�Ѵ�.
        transform.position = dir;

        // ��ǥ������ �������� ��
        Vector3 checkTarget = targetVectors[currentTaget];
        checkTarget.y = transform.position.y;
        if (Vector3.Distance(checkTarget, transform.position) < 0.1f)
        {
            if (isBackward == true)
            {
                // ó���������� �����ϸ� ������ ����
                if (currentTaget == 0)
                {
                    isBackward = false;
                    currentTaget = currentTaget + 1;
                }
                // ó���������� �������� �ʾҴٸ� �� ��������
                else
                {
                    currentTaget = currentTaget - 1;
                }
            }
            else
            {
                // ������������ �����ϸ� �ڷ� ���ư���
                if (currentTaget == (targetVectors.Count - 1))
                {
                    isBackward = true;
                    currentTaget = currentTaget - 1;
                }
                // ������������ �������� �ʾҴٸ� ���� ��������
                else
                {
                    currentTaget = currentTaget + 1;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� �浹�� ������Ʈ�� �Ѿ����� �˻��Ѵ�
        if (collision.gameObject.tag == "Shot")
        {
            isHit = true;
            // �Ѿ˿��� �������� �� ���� HP���� -�Ѵ�.
            currentHP = currentHP - collision.gameObject.GetComponent<Shot>().damage;
            //GetComponent<Animator>().SetBool("isWalk", false);
            if (currentHP <= 0)
            {
                // �״´�.
                // �� ������Ʈ�� �����Ѵ�.
                GetComponent<Animator>().SetBool("isDeath", true);
                //GameObject.Destroy(gameObject);
                Invoke("Delete", 0.6f);
            }
            else
            {
                // ���� ������
                GetComponent<Animator>().SetTrigger("isHit");
                Invoke("OffHit", 0.4f);
            }
        }
    }

    private void OffHit()
    {
        isHit = false;
    }

    private void Delete()
    {
        GameObject.Destroy(gameObject);
    }
}
