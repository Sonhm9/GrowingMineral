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
    private PlayerController playerController;
    public Transform[] monsterGeneratePosition = new Transform[5]; // ���� ������
    public SpriteRenderer[] stageBackgroundMap; // �������� �� ����̹���
    public StageDataTable[] stageDataTables; // �������� ���������̺� ����

    public MonsterSettings[] currentMonsterData = new MonsterSettings[5]; // ���� ������ ������

    public bool bossMonsterAble = true; // ���� ���� ��ȯ ���ɻ���

    [HideInInspector]
    public int stageLevel = 0; // �������� ����
    [HideInInspector]
    public float bossMonsterHP = 0; // ���� ���� HP
    [HideInInspector]
    public float bossMonsterDropResource = 0; // ���� ���� HP



    //private int mapLevel = 0; // �� ����


    public TextMeshProUGUI stageLevelText;
    public Transform mainMap;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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
        bossMonsterAble = true;
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
            MonsterDataTable monsterData = monster.GetComponent<MonsterSettings>().monsterDataTable;
            bossMonsterHP += monsterData.monsterHP;
            bossMonsterDropResource += (monsterData.dropResorceAmount * 2);
        }
    }

    private void DeleteMonster()
    {
        // ���� ���������� ���� ���� ����
        for (int i = playerController.targerPosition; i < monsterGeneratePosition.Length; i++)
        {
            GameObject monster = monsterGeneratePosition[i].GetChild(1).GetChild(i).GetChild(0).gameObject;
            if(monster!=null) Destroy(monster);
        }
    }

    public void EndBattleMode()
    {
        // ���� ��� ����
        playerController.BattleModeEnd();
    }

    public void TryBossStage()
    {
        // ������� �õ�
        if (bossMonsterAble)
        {
            DeleteMonster();
            GameObject bossMonster = Instantiate(stageDataTables[stageLevel].bossMonsterPrefab, monsterGeneratePosition[4].GetChild(1).GetChild(4));
            playerController.BattleModeEnd();
            playerController.BossBattleModeStart();
        }
    }

    public void BossModeFailed()
    {
        // ������� ���� �� ó��
        BossMonsterController bossMonsterController = FindObjectOfType<BossMonsterController>();
        Destroy(bossMonsterController.gameObject);
        SetStage();
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.targerPosition = 0;
        playerController.BattleModeStart();
    }




}
