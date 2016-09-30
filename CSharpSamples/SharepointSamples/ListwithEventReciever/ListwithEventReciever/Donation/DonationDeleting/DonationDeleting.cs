using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Diagnostics;
// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
namespace Donation.DonationDeleting
{
    /// <summary>
    /// События элемента списка
    /// </summary>
    public class DonationDeleting : SPItemEventReceiver
    {
       /// <summary>
        /// Образец EventHandler, который не позволяет пользователю удалять элемент в списке пожертвований, который имеет значение больше 0
       /// </summary>
       public override void ItemDeleting(SPItemEventProperties properties)
       {
           try
           {
               double amount = (double)properties.ListItem["Amount"];
               if (amount > 0)
               {
                   properties.ErrorMessage = "You cannot delete donations with an amount greater than 0. We want to keep the money!";
                   properties.Cancel = true;
               }
           }
           catch (Exception ex)
           {
               EventLog.WriteEntry("SP Event Handler", ex.Message);
           }
       }


    }
}
