using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtilleryUpgrade : MonoBehaviour
{
    public GameObject upgradePrefab;
    public GameObject upgradePanel;
    public Button selfUpgradeButton;
    public Button upgradeButton;
    private readonly float offset = 0.098f;
    private readonly string artilleryLayer = "Artillery";

    private void Start()
    {
        upgradePanel.SetActive(false);
        selfUpgradeButton.onClick.AddListener(SelfUpgrade);
        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void OnMouseDown()
    {
        if (gameObject.layer == LayerMask.NameToLayer(artilleryLayer))
        {
            upgradePanel.SetActive(true);
            upgradePanel.transform.position = transform.position;
        }
    }

    private void SelfUpgrade()
    {
        Vector3 newPosition = transform.position + new Vector3(0, offset, 0);
        Instantiate(gameObject, newPosition, Quaternion.identity);
        upgradePanel.SetActive(false);
    }

    private void Upgrade()
    {
        Instantiate(upgradePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        upgradePanel.SetActive(false);
    }
}
