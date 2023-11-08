using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StoneUpgrade : MonoBehaviour
{
    public List<Button> upgradeButtons; // ���׷��̵� �� ��ư
    public List<Button> purchasedButtons; // ������ ��ư
    private Button currentButton; // ���� �������� ��ư

    public GameObject[] stonePrefab; // ���� ������

    public int[] upgradePrices; // ���׷��̵� ����
    private int currentUpgradeLevel = 0; // ���� ���׷��̵� ����

    public Transform player; // �÷��̾�

    private void Start()
    {
        upgradeButtons[0].interactable = true;
    }

    public void PurchaseButton()
    {
        currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>(); // ���� Ŭ���� ��ư ��������

        if ((upgradeButtons.Count > 0) && upgradeButtons.Contains(currentButton)) // ������ ó�� ������ ���� ����
        {
            ResourceManager.instance.CheckResourceAmount(ResourceManager.ResourceType.Diamond, upgradePrices[currentUpgradeLevel+1]);
            if (ResourceManager.instance.consumeAble)
            {
                purchasedButtons.Add(upgradeButtons[0]); // ������ ��ư ����Ʈ�� �̵�
                ChangeStone();

                ResourceManager.instance.RemoveResource(ResourceManager.ResourceType.Diamond, upgradePrices[currentUpgradeLevel]); // �ڿ� ����
                TextMeshProUGUI upgradeText = upgradeButtons[0].GetComponentInChildren<TextMeshProUGUI>();
                upgradeText.text = "������"; // �ؽ�Ʈ "������" ��ȯ
                upgradeText.rectTransform.anchoredPosition = new Vector3(upgradeText.rectTransform.anchoredPosition.x - 40, 0, 0); // �ؽ�Ʈ ����� ���߱�

                Image[] removeImages = upgradeText.GetComponentsInChildren<Image>();
                removeImages[0].gameObject.SetActive(false); // ���̾� �̹��� ��Ȱ��ȭ
                removeImages[1].gameObject.SetActive(false); // �Ƿ翧 �̹��� ��Ȱ��ȭ

                upgradeButtons.Remove(upgradeButtons[0]);
                upgradePrices[currentUpgradeLevel] = 0; // ������ ���� ������ ����� ����

                if (upgradeButtons.Count != 0)
                    upgradeButtons[0].interactable = true; // ���� ���� ��ư Ȱ��ȭ
            }
            else
                return; // ���� �Ұ��� �Լ� ����
        }
        else // �̹� ������ ������ ��
            ChangeStone();

        ChangeText();
        PlayerStoneUpgrade();
    }
    public void ChangeStone() // ������ ���� ����
    {
        switch (purchasedButtons.IndexOf(currentButton))
        {
            case var _ when currentButton == purchasedButtons[0]:
                currentUpgradeLevel = 0;
                    break;
            case var _ when currentButton == purchasedButtons[1]:
                currentUpgradeLevel = 1;
                break;
            case var _ when currentButton == purchasedButtons[2]:
                currentUpgradeLevel = 2;
                break;
            case var _ when currentButton == purchasedButtons[3]:
                currentUpgradeLevel = 3;
                break;
            case var _ when currentButton == purchasedButtons[4]:
                currentUpgradeLevel = 4;
                break;
            case var _ when currentButton == purchasedButtons[5]:
                currentUpgradeLevel = 5;
                break;
            case var _ when currentButton == purchasedButtons[6]:
                currentUpgradeLevel = 6;
                break;
            default:
                return;
        }
    }

    public void ChangeText() // ���ŵ� ��ư�� �ؽ�Ʈ ����
    {
        for (int i = 0; i < purchasedButtons.Count; i++)
        {
            purchasedButtons[i].interactable = true; // �������� �ƴ� ��ư ��� Ȱ��ȭ
            purchasedButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "����";
        }
        currentButton.interactable = false; // �������� ��ư�� ��Ȱ��ȭ
        currentButton.GetComponentInChildren<TextMeshProUGUI>().text = "������";
    }

    public void PlayerStoneUpgrade()
    {
        // ���� ���׷��̵�
        int childCount = player.childCount;
        if (childCount > 0)
        {
            Transform childToBeDeleted = player.GetChild(0);
            Destroy(childToBeDeleted.gameObject);

            GameObject newChild = Instantiate(stonePrefab[currentUpgradeLevel], player);
            newChild.transform.localPosition = Vector3.zero;

            Animator newAnimator = newChild.GetComponent<Animator>();
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.animator = newAnimator;
        }
        else
        {
            Debug.Log("�ڽ� ������Ʈ�� �����ϴ�.");
        }
    }


}
