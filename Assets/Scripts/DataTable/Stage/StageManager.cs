using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public Transform[] monsterGeneratePosition = new Transform[5]; // ���� ������
    public SpriteRenderer[] stageBackgroundMap; // �������� �� ����̹���
    public StageDataTable[] stageDataTables; // �������� ���������̺� ����

    private MonsterDataTable[] currentMonsterData = new MonsterDataTable[5]; // ���� ������ ������

    private int stageLevel = 0; // �������� ����
    private int mapLevel = 0; // �� ����

    public TextMeshProUGUI stageLevelText;
    public Transform mainMap;

    void Start()
    {
        for (int i = 0; i < monsterGeneratePosition.Length; i++)
        {
            // monsterGeneratePosition�� Transform������Ʈ �Ҵ�
            monsterGeneratePosition[i] = GetComponent<Transform>();
        }

        SetStage(); // �������� ����
    }

    public void SetStage()
    {
        // �������� ����
        DisplayStageLevelText();
        SetMonster();
    }
    public void MovingNextStage()
    {
        // ���� ���������� �̵�
        
    }
    private void DisplayStageLevelText()
    {
        // ���������� �´� �ؽ�Ʈ�� ǥ��
        stageLevelText.text = "STAGE "+stageDataTables[stageLevel].mainStageNumber.ToString() + "-" + stageDataTables[stageLevel].subStageNumber.ToString();
    }

    private void SetMonster()
    {
        // ���������� �´� ���͸� ��ġ
        for (int i = 0; i < monsterGeneratePosition.Length; i++)
        {
            GameObject monster = Instantiate(stageDataTables[stageLevel].monsterPrefab[i], monsterGeneratePosition[i].GetChild(1).GetChild(i));
        }
    }

    private void DeleteMonster()
    {
        // ���� ���������� ���� ���� ����
        for (int i = 0; i < monsterGeneratePosition.Length; i++)
        {
            GameObject monster = monsterGeneratePosition[i].GetChild(1).GetChild(i).gameObject;
            Destroy(monster);
        }
    }

    public void AccessMonster()
    {
        // ���� ���� ������������
        for (int i = 0; i < monsterGeneratePosition.Length; i++)
        {
            GameObject monster = Instantiate(stageDataTables[stageLevel].monsterPrefab[i], monsterGeneratePosition[i].GetChild(1).GetChild(i));
            currentMonsterData[i] = monster.GetComponent<MonsterSettings>().monsterDataTable;
        }
    }
}
