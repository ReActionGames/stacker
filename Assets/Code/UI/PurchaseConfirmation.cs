using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stacker
{
    public class PurchaseConfirmation : MonoBehaviour
    {
        [SerializeField] private GameObject confirmPanel;
        [SerializeField] private Button yesButton;

        private string productIdCache = "";

        private void Start()
        {
            yesButton.onClick.AddListener(YesButtonClicked);
            confirmPanel.SetActive(false);
        }

        public void ConfirmPurchase(string productID)
        {
            productIdCache = productID;
            confirmPanel.SetActive(true);
        }

        private void YesButtonClicked()
        {
            FindObjectOfType<Purchaser>().BuyProductID(productIdCache);
            confirmPanel.SetActive(false);
        }
    }
}