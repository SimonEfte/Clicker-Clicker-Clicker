using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using System.Collections.Generic;

public class InAppPurchase : MonoBehaviour, IDetailedStoreListener
{
    IStoreController m_StoreController;

    public RemoveAds removeAdsItem;

    private void Start()
    {
        if(MobileScript.isMobile == true)
        {
            SetupBuilder();
            isAdsRemovedPlayerprefs = PlayerPrefs.GetInt("isAdsRemoved");
        }
    }

    #region All Remove ADS stuff
    [Serializable]
    public class RemoveAds
    {
        public string Name;
        public string Id;
        public string desc;
        public float price;
    }
    #endregion

    //[Obsolete]
    void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(removeAdsItem.Id, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
       // Debug.Log("Success");
        m_StoreController = controller;
        CheckNonConsumable(removeAdsItem.Id);
    }

    #region StoreListener Stuff
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //Debug.Log("Error" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        //Debug.Log("Error" + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //Debug.Log("Error");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        //Debug.Log("Error");
    }
    #endregion

    public void RemoveTheAd()
    {
        m_StoreController.InitiatePurchase(removeAdsItem.Id);
    }

    //processing purchase
    public GameObject adFrame, claimFrame, cliamGoldText, claimClickscensionText;

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        //Retrive the purchased product
        var product = purchaseEvent.purchasedProduct;

        if(product.definition.id == removeAdsItem.Id)
        {
            //Debug.Log("Purchase Complete " + product.definition.id);

            if(isAdsRemovedPlayerprefs == 0)
            {
                if (MobileScript.isGoldReward == true) { cliamGoldText.SetActive(true); claimClickscensionText.SetActive(false); }
                else { cliamGoldText.SetActive(false); claimClickscensionText.SetActive(true); }
            }

            MobileScript.isAdsRemoved = true;
            isAdsRemovedPlayerprefs = 1;
            PlayerPrefs.SetInt("isAdsRemoved", isAdsRemovedPlayerprefs);
            PlayerPrefs.Save();

            adFrame.SetActive(false); claimFrame.SetActive(true);
        }

        return PurchaseProcessingResult.Complete;
    }

    public static int isAdsRemovedPlayerprefs;

    void CheckNonConsumable(string id)
    {
        if(m_StoreController != null)
        {
            var product = m_StoreController.products.WithID(id);
            if(product != null)
            {
                if (product.hasReceipt)
                {
                    MobileScript.isAdsRemoved = true;
                    isAdsRemovedPlayerprefs = 1;
                    PlayerPrefs.SetInt("isAdsRemoved", isAdsRemovedPlayerprefs);
                    PlayerPrefs.Save();
                }
                else
                {
                    MobileScript.isAdsRemoved = false;
                    isAdsRemovedPlayerprefs = 0;
                }
            }
        }
    }
}
