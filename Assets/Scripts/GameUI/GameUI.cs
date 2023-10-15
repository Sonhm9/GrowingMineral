using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StoneText; // ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI DiaText; // ���̾� �ؽ�Ʈ
    [SerializeField] UnityEngine.UI.Slider EnemyHPBar; // �Ϲ� �� ü�� ��
    [SerializeField] UnityEngine.UI.Slider BossHPBar; // ���� ü�� ��
    [SerializeField] UnityEngine.UI.Slider TimerBar; // Ÿ�̸� ��
    [SerializeField] UnityEngine.UI.Button BossTryButton; // ���� �õ� ��ư
    [SerializeField] UnityEngine.UI.Image BossFail; // ���� ��� ���� â
    [SerializeField] UnityEngine.UI.Image GameExit; // ���� ���� â
    [SerializeField] GameObject StoneUpgradeUI; // ���� ��ư
    [SerializeField] GameObject LevelUpgradeUI; // ��ȭ ��ư

    [SerializeField] UnityEngine.UI.Button StoneLv2; // ���� 2����(������)

    [SerializeField] UnityEngine.UI.Button Level1Upgrade; // 1 ������ ��ȭ
    [SerializeField] UnityEngine.UI.Button Level10Upgrade; // 10 ������ ��ȭ
    [SerializeField] UnityEngine.UI.Button Level100Upgrade; // 100 ������ ��ȭ

    public static float time = 30; // ���� ���� �ð� 30��
    bool TimerStart = false;

    // Start is called before the first frame update
    void Awake()
    {
        NormalEnemyHunting();
        LevelUpgradeEnabled();
        GameExit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerStart == true)
        {
            Timer();
            if (TimerBar.value == 0)
                BossFail.gameObject.SetActive(true);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                GameExit.gameObject.SetActive(true);
        }

        StoneUpgrade();
    }

    public void StoneUpgrade() // ���� ���� ���׷��̵�
    {
        if (ResourceManager.instance.Diamond >= 100)
            StoneLv2.interactable = true;
    }

    public void NormalEnemyHunting() // �Ϲ� �� ��� ����
    {
        TimerStart = false;
        BossTryButton.gameObject.SetActive(true);
        EnemyHPBar.gameObject.SetActive(true);
        BossHPBar.gameObject.SetActive(false);
        TimerBar.gameObject.SetActive(false);
        BossFail.gameObject.SetActive(false);
    }

    public void BossTry() // ���� Ʈ���� ����
    {
        time = 30;
        TimerBar.value = float.MaxValue;
        TimerStart = true;
        BossTryButton.gameObject.SetActive(false);
        EnemyHPBar.gameObject.SetActive(false);
        BossHPBar.gameObject.SetActive(true);
        TimerBar.gameObject.SetActive(true);
    }

    public void Timer() // Ÿ�̸�
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            TimerBar.value = time;
        }
    }

    public void StoneUpgradeEnabled() // ���� ���׷��̵� â Ȱ��ȭ
    {
        LevelUpgradeUI.SetActive(false);
        StoneUpgradeUI.SetActive(true);
    }
    public void LevelUpgradeEnabled() // ��ȭ â Ȱ��ȭ
    {
        StoneUpgradeUI.SetActive(false);
        LevelUpgradeUI.SetActive(true);
    }

    public void Level1UpgradeEnabled() // 1 ������ ��ȭ ��ư Ȱ��ȭ
    {
        Level1Upgrade.interactable = false;
        Level10Upgrade.interactable = true;
        Level100Upgrade.interactable = true;
    }
    public void Level10UpgradeEnabled() // 10 ������ ��ȭ ��ư Ȱ��ȭ
    {
        Level1Upgrade.interactable = true;
        Level10Upgrade.interactable = false;
        Level100Upgrade.interactable = true;
    }
    public void Level100UpgradeEnabled() // 100 ������ ��ȭ ��ư Ȱ��ȭ
    {
        Level1Upgrade.interactable = true;
        Level10Upgrade.interactable = true;
        Level100Upgrade.interactable = false;
    }

    public void GameExitOK() // ���� ������ Ȯ��
    {
        Application.Quit();
    }
    public void GameExitCancel() // ���� ������ ���
    {
        GameExit.gameObject.SetActive(false);
    }
}
