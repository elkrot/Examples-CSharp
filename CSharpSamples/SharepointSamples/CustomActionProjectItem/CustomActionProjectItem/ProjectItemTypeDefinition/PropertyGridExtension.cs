// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using Microsoft.VisualStudio.SharePoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Contoso.SharePointProjectItems.CustomAction
{
    internal partial class CustomActionProvider
    {
        private void ProjectItemPropertiesRequested(object sender,
            SharePointProjectItemPropertiesRequestedEventArgs e)
        {
            CustomActionProperties properties;

            // Если объект свойств уже существует, получите его из аннотаций элемента проекта.
            if (!e.ProjectItem.Annotations.TryGetValue(out properties))
            {
                // В противном случае, создайте новый объект свойств и добавьте его к аннотациям.
                properties = new CustomActionProperties(e.ProjectItem);
                e.ProjectItem.Annotations.Add(properties);
            }

            e.PropertySources.Add(properties);
        }
    }

    internal class CustomActionProperties
    {
        private ISharePointProjectItem projectItem;
        private const string testPropertyId = "Contoso.CustomActionTestProperty";
        private const string propertyDefaultValue = "This is a test value.";

        internal CustomActionProperties(ISharePointProjectItem projectItem)
        {
            this.projectItem = projectItem;
        }

        // Получает или задает простое строковое свойство. Значение свойства хранится в свойстве ExtensionData
        // элемента проекта. Данные в свойстве ExtensionData сохраняются при закрытии проекта.
        [DisplayName("Custom Action Property")]
        [DescriptionAttribute("This is a test property for the Contoso Custom Action project item.")]
        [DefaultValue(propertyDefaultValue)]
        public string TestProperty
        {
            get
            {
                string propertyValue;

                // Получите текущее значение свойства, если оно уже существует; в противном случае, верните значение по умолчанию.
                if (!projectItem.ExtensionData.TryGetValue(testPropertyId, out propertyValue))
                {
                    propertyValue = propertyDefaultValue;
                }
                return propertyValue;
            }
            set
            {
                if (value != propertyDefaultValue)
                {
                    projectItem.ExtensionData[testPropertyId] = value;
                }
                else
                {
                    // Не сохраняйте значение по умолчанию.
                    projectItem.ExtensionData.Remove(testPropertyId);
                }
            }
        }
    }
}