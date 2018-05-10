using DoozyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stacker
{
    public class PurchaseConfirmation : MonoBehaviour
    {
        [SerializeField] private UIButton yesButton;

        private string productIdCache = "";

        private void Start()
        {
            yesButton.OnClick.AddListener(YesButtonClicked);
        }

        public void ConfirmPurchase(string productID)
        {
            productIdCache = productID;
        }

        private void YesButtonClicked()
        {
            FindObjectOfType<Purchaser>().BuyProductID(productIdCache);
        }
    }
}