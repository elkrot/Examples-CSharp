// � ���������� ���������� (Microsoft Corp.). ��� ����� ��������.
// ���� ��� ������� �� �������� 
// �������� �������� ���������� (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.SharePoint;

namespace Contoso.SharePointProjectItems.CustomAction
{
    // ��������� Visual Studio �������� � ��������� ��� ����������.
    [Export(typeof(ISharePointProjectItemTypeProvider))]

    // ������ ������������� ��� ������� ������ ���� �������� �������. ��� ������ ������ ��������� �� ��������� 
    // ������� ������� �������� ProjectItem � ���� .spdata ��� �������� �������.
    [SharePointProjectItemType("Contoso.CustomAction")]

    // ��������� ������, ������� ����� ������������ � ������ ��������� ������� � ������������ �������.
    [SharePointProjectItemIcon("ProjectItemDefinition.CustomAction_SolutionExplorer.ico")]

    // ���������� ����� ��� �������� �������, ������� ����� ������������ ��� �������� ����������������� �������� �� ����� SharePoint.
    internal partial class CustomActionProvider : ISharePointProjectItemTypeProvider
    {
        private ISharePointProjectService projectService;

        // ��������� IProjectItemTypeProvider.InitializeType. ����������� ��������� ���� �������� �������.
        public void InitializeType(ISharePointProjectItemTypeDefinition projectItemTypeDefinition)
        {
            projectItemTypeDefinition.Name = "CustomAction";
            projectItemTypeDefinition.SupportedDeploymentScopes =
                SupportedDeploymentScopes.Site | SupportedDeploymentScopes.Web;
            projectItemTypeDefinition.SupportedTrustLevels = SupportedTrustLevels.All;

            // �������� ������, ����� �� ��� ������������ ��������� ��� � ���� ������.
            projectService = projectItemTypeDefinition.ProjectService;

            // ����������� ��������� ������� �������� �������.
            projectItemTypeDefinition.ProjectItemInitialized += ProjectItemInitialized;
            projectItemTypeDefinition.ProjectItemNameChanged += ProjectItemNameChanged;
            projectItemTypeDefinition.ProjectItemDisposing += ProjectItemDisposing;

            // ����������� �������, ����� ������� ���������������� �������� � ������� ������������ ���� ��� ������� �������� �������.
            projectItemTypeDefinition.ProjectItemPropertiesRequested +=
                ProjectItemPropertiesRequested;
            projectItemTypeDefinition.ProjectItemMenuItemsRequested +=
                ProjectItemMenuItemsRequested;
        }

        private void ProjectItemInitialized(object sender, SharePointProjectItemEventArgs e)
        {
            // ����������� ������� �������.
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