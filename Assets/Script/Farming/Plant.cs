
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject childPrefab;//�ڽ�
    public int childCount;//�ڽ� ������ ����

    public void hit()
    {
        for (int i = 0; i < childCount; i++)
        {
            // �ڽ� �������� ����
            GameObject currentBranch = Instantiate(childPrefab, transform.position + new Vector3(0, 1), Quaternion.identity);

            // ȸ������ �����Ͽ� ���������� Ư�� ������ ȸ���ǵ��� ��
            Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
            currentBranch.transform.rotation = rotation;

            //������ ���������� Rigidbody �� �߷� ����
            Rigidbody branchRigidbody = currentBranch.GetComponent<Rigidbody>();

            branchRigidbody.AddForce(currentBranch.transform.forward*10);

        }

        Destroy(gameObject);
    }
}