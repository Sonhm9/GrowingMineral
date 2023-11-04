using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    private static object _lock = new object();
    private static StageManager _instance = null;
    public static StageManager instance
    {
        get
        {
            if (applicationQuitting)
            {
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("StageManager ");
                    obj.AddComponent<StageManager>();
                    _instance = obj.GetComponent<StageManager>();
                }
                return _instance;
            }
        }
        set
        {
            _instance = value;
        }
    }
    private static bool applicationQuitting = false;
    // �̱���
    private void Awake()
    {
        _instance = this;
        // �̱��� �ν��Ͻ�
    }
    private void OnDestroy()
    {
        applicationQuitting = true;
        // �ν��Ͻ� ����
    }

    public Transform[] monsterGeneratePosition = new Transform[5]; // ���� ������
    public SpriteRenderer[] stageBackgroundMap; // �������� �� ����̹���
    public StageDataTable[] stageDataTables; // �������� ���������̺� ����

    public MonsterSettings[] currentMonsterData = new MonsterSettings[5]; // ���� ������ ������

    private int stageLevel = 1; // �������� ����
    //private int mapLevel = 0; // �� ����


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
        // ���������� �´� ���͸� ��ġ �� ������ ����
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

    public void EndBattleMode()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.BattleModeEnd();
    }

}
