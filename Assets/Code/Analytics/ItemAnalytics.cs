using Stacker.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Analytics
{
    public class ItemAnalytics : MonoBehaviour
    {
        public void OnItemPurchased(Theme item)
        {
            UnityEngine.Analytics.AnalyticsEvent.StoreItemClick(UnityEngine.Analytics.StoreType.Soft, item.ID.ToString());

            //UnityEngine.Analytics.AnalyticsEvent.StoreItemClick(UnityEngine.Analytics.StoreType.Soft, item.ID.ToString(),
            //        eventData: new Dictionary<string, object> {
            //        { "item_data", item }
            //    });
        }

        public void OnItemSelected(Theme item)
        {
            UnityEngine.Analytics.AnalyticsEvent.Custom("ItemSelected", new Dictionary<string, object>
            {
                {"item_id", item.ID.ToString() }
            });

            //UnityEngine.Analytics.AnalyticsEvent.Custom("ItemSelected", new Dictionary<string, object>
            //{
            //    {"item_data", item }
            //});
        }
    }
}