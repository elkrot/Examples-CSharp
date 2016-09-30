// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;

namespace SequentialWorkflow.Workflow1
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public Guid taskId1 = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public SPWorkflowTaskProperties taskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties afterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties beforeProperties1 = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        private bool taskCompleted;

        private void createTask1_MethodInvoking(object sender, EventArgs e)
        {
            //У задачи должен быть GUID
            taskId1 = Guid.NewGuid();
            //Настройка основных свойств задач
            taskProperties.PercentComplete = (float)0.0;
            taskProperties.AssignedTo = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            //Эта возможность очень удобна при встраивании форм InfoPath
            taskProperties.TaskType = 0;
            taskProperties.DueDate = DateTime.Now.AddDays(7);
            taskProperties.StartDate = DateTime.Now;
            taskProperties.Title = "SharePoint Workflow Task";
        }

        private void notCompleted(object sender, ConditionalEventArgs e)
        {
            //Для завершенной задачи результат должен быть 1,0
            e.Result = !taskCompleted;
        }

        private void onTaskChanged1_Invoked(object sender, ExternalDataEventArgs e)
        {
            //Проверка свойств задачи после внесения изменений.
            //Поиск значения 1,0 как признака выполненной задачи.
            if (afterProperties.PercentComplete == 1.0)
            {
                taskCompleted = true;
            }
        }
    }
}
