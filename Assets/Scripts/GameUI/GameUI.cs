using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StoneText; // ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI DiaText; // ���̾� �ؽ�Ʈ
    [SerializeField] Slider BossHPBar; // ���� ü�� ��
    [SerializeField] Slider TimerBar; // Ÿ�̸� ��
    [SerializeField] Button BossTryButton; // ���� �õ� ��ư
    [SerializeField] Image SettingWindow; // ���� â
    [SerializeField] Image BossFailWindow; // ���� ��� ���� â
    [SerializeField] Image GameExitWindow; // ���� ���� â
    [SerializeField] GameObject StoneUI; // ���� UI
    [SerializeField] GameObject EnhanceUI; // ��ȭ UI

    [SerializeField] Button StoneLv2; // ���� 2����(������)

    [SerializeField] Button Level1Upgrade; // 1 ������ ��ȭ
    [SerializeField] Button Level10Upgrade; // 10 ������ ��ȭ
    [SerializeField] Button Level100Upgrade; // 100 ������ ��ȭ

    public static float time = 30; // ���� ���� �ð� 30��
    bool TimerStart = false;

    // Update is called once per frame
    void Update()
    {
        if (TimerStart == true)
        {
            Timer();
            if (TimerBar.value == 0)
                BossFailWindow.gameObject.SetActive(true);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                GameExitWindow.gameObject.SetActive(true);
        }

        StoneUpgrade();
    }

    public void StoneUpgrade() // ���� ���� ���׷��̵�
    {
        if (ResourceManager.instance.Diamond >= 100)
            StoneLv2.interactable = true;
    }

    public void NormalEnemyHunting() // �Ϲ� ���� ��� ����
    {
        TimerStart = false;
        BossTryButton.gameObject.SetActive(true);
        BossHPBar.gameObject.SetActive(false);
        TimerBar.gameObject.SetActive(false);
        BossFailWindow.gameObject.SetActive(false);
    }

    public void BossTry() // ���� Ʈ���� ����
    {
        time = 30;
        TimerBar.value = float.MaxValue;
        TimerStart = true;
        BossTryButton.gameObject.SetActive(false);
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

    public void StoneUIEnabled() // ���� UI Ȱ��ȭ
    {
        EnhanceUI.SetActive(false);
        StoneUI.SetActive(true);
    }
    public void EnhanceUIEnabled() // ��ȭ UI Ȱ��ȭ
    {
        StoneUI.SetActive(false);
        EnhanceUI.SetActive(true);
    }
    public void SettingWindowEnabled() // ���� â Ȱ��ȭ
    {
        SettingWindow.gameObject.SetActive(true);
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

    public void GameExit() // ���� ������ Ȯ��
    {
        Application.Quit();
    }
    public void WindowExit() // ���� â ���� �� ���� ������ ���
    {
        SettingWindow.gameObject.SetActive(false);
        GameExitWindow.gameObject.SetActive(false);
    }
}
