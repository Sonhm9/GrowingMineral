using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoneUpgrade : MonoBehaviour
{
    public List<Button> upgradeButtons; // ���׷��̵� �� ��ư
    public List<Button> purchasedButtons; // ������ ��ư

    public GameObject[] stonePrefab; // ���� ������

    public int[] upgradePrices; // ���׷��̵� ����
    private int currentUpgradeLevel=0; // ���� ���׷��̵� ����

    public Transform player; // �÷��̾�

    private void Start()
    {
        upgradeButtons[0].interactable = true;
    }
    public void PurchasedButtonRemove()
    {
        // ������ ��ư�� ����
        purchasedButtons[0].gameObject.SetActive(false);
        purchasedButtons.Remove(purchasedButtons[0]);

    }

    public void PurchaseButton()
    {
        ResourceManager.instance.CheckResourceAmount(ResourceManager.ResourceType.Diamond, upgradePrices[currentUpgradeLevel]);
        if (ResourceManager.instance.consumeAble)
        {
            ResourceManager.instance.RemoveResource(ResourceManager.ResourceType.Diamond, upgradePrices[currentUpgradeLevel]); // �ڿ� ����
            TextMeshProUGUI stoneText = upgradeButtons[0].GetComponentInChildren<TextMeshProUGUI>();
            stoneText.text = "������"; // �ؽ�Ʈ "������" ��ȯ

            Image stoneImage = stoneText.GetComponentInChildren<Image>();
            stoneImage.gameObject.SetActive(false); // ���̾� �̹��� ��Ȱ��ȭ

            purchasedButtons.Add(upgradeButtons[0]);
            upgradeButtons.Remove(upgradeButtons[0]); // ������ ��ư ����Ʈ�� �̵�

            if (upgradeButtons.Count > 0)
            {
                upgradeButtons[0].interactable = true;
            }
            PurchasedButtonRemove();
            purchasedButtons[0].interactable = false;


            PlayerStoneUpgrade();

            currentUpgradeLevel++;

        }
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
