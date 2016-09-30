// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.SharePoint;

namespace Contoso.SharePointProjectItems.CustomAction
{
    // Позволяет Visual Studio находить и загружать это расширение.
    [Export(typeof(ISharePointProjectItemTypeProvider))]

    // Задает идентификатор для данного нового типа элемента проекта. Эта строка должна совпадать со значением 
    // Введите атрибут элемента ProjectItem в файл .spdata для элемента проекта.
    [SharePointProjectItemType("Contoso.CustomAction")]

    // Указывает значок, который будет отображаться с данным элементом проекта в обозревателе решений.
    [SharePointProjectItemIcon("ProjectItemDefinition.CustomAction_SolutionExplorer.ico")]

    // Определяет новый тип элемента проекта, который можно использовать для создания пользовательского действия на сайте SharePoint.
    internal partial class CustomActionProvider : ISharePointProjectItemTypeProvider
    {
        private ISharePointProjectService projectService;

        // Реализует IProjectItemTypeProvider.InitializeType. Настраивает поведение типа элемента проекта.
        public void InitializeType(ISharePointProjectItemTypeDefinition projectItemTypeDefinition)
        {
            projectItemTypeDefinition.Name = "CustomAction";
            projectItemTypeDefinition.SupportedDeploymentScopes =
                SupportedDeploymentScopes.Site | SupportedDeploymentScopes.Web;
            projectItemTypeDefinition.SupportedTrustLevels = SupportedTrustLevels.All;

            // Получите службу, чтобы ее мог использовать остальной код в этом классе.
            projectService = projectItemTypeDefinition.ProjectService;

            // Обработайте некоторые события элемента проекта.
            projectItemTypeDefinition.ProjectItemInitialized += ProjectItemInitialized;
            projectItemTypeDefinition.ProjectItemNameChanged += ProjectItemNameChanged;
            projectItemTypeDefinition.ProjectItemDisposing += ProjectItemDisposing;

            // Обработайте события, чтобы создать пользовательское свойство и элемент контекстного меню для данного элемента проекта.
            projectItemTypeDefinition.ProjectItemPropertiesRequested +=
                ProjectItemPropertiesRequested;
            projectItemTypeDefinition.ProjectItemMenuItemsRequested +=
                ProjectItemMenuItemsRequested;
        }

        private void ProjectItemInitialized(object sender, SharePointProjectItemEventArgs e)
        {
            // Обработайте событие проекта.
            e.ProjectItem.Project.PropertyChanged += ProjectPropertyChanged;
        }

        private void ProjectItemNameChanged(object sender, NameChangedEventArgs e)
        {
            ISharePointProjectItem projectItem = (ISharePointProjectItem)sender;
            string message = String.Format("The name of the {0} item changed to: {1}",
                e.OldName, projectItem.Name);
            projectService.Logger.WriteLine(message, LogCategory.Message);
        }

        private void ProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ISharePointProject project = (ISharePointProject)sender;
            string message = String.Format("The following property of the {0} project was changed: {1}",
                    project.Name, e.PropertyName);
            projectService.Logger.WriteLine(message, LogCategory.Message);
        }

        private void ProjectItemDisposing(object sender, SharePointProjectItemEventArgs e)
        {
            e.ProjectItem.Project.PropertyChanged -= ProjectPropertyChanged;
        }
    }
}