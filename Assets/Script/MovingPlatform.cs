using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public List<Vector3> targetVectors = new List<Vector3>();
    private int currentTaget = 0;
    private bool isBackward = false;
    public float waitSecond = 1F;

    void Start()
    {
        StartCoroutine(CoMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator CoMove()
    {
        // ���� ��ġ���� ����� ������ ġȯ�Ѵ�.
        transform.position = Vector3.MoveTowards(transform.position, targetVectors[currentTaget], moveSpeed * Time.deltaTime);

        // ��ǥ������ �������� ��
        Vector3 checkTarget = targetVectors[currentTaget];


        //1 ������ ���
        yield return null;

        if (Vector3.Distance(checkTarget, transform.position) < 0.1f)
        {
            //���ð��� ������
            yield return new WaitForSeconds(waitSecond);

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
}
