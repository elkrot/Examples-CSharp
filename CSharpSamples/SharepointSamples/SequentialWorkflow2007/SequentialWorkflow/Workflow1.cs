using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WorkflowActions;
using System.Threading;

// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

namespace Microsoft.Samples.SequentialWorkflow
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }
        #region "Глобальные объявления"
        public Guid workflowId = default(System.Guid);
        public Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties workflowProperties = new Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties();
        public static DependencyProperty taskPropertiesProperty = DependencyProperty.Register("taskProperties", typeof(Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties), typeof(Microsoft.Samples.SequentialWorkflow.Workflow1));
        private bool taskCompleted;
        #endregion
        #region "TaskProps"
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]

        public Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties taskProperties
        {
            get
            {
                return ((Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties)(base.GetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.taskPropertiesProperty)));
            }
            set
            {
                base.SetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.taskPropertiesProperty, value);
            }
        }

        public static DependencyProperty TaskIDProperty = DependencyProperty.Register("TaskID", typeof(System.Guid), typeof(Microsoft.Samples.SequentialWorkflow.Workflow1));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public Guid TaskID
        {
            get
            {
                return ((System.Guid)(base.GetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.TaskIDProperty)));
            }
            set
            {
                base.SetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.TaskIDProperty, value);
            }
        }

        public static DependencyProperty afterTaskPropsProperty = DependencyProperty.Register("afterTaskProps", typeof(Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties), typeof(Microsoft.Samples.SequentialWorkflow.Workflow1));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties afterTaskProps
        {
            get
            {
                return ((Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties)(base.GetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.afterTaskPropsProperty)));
            }
            set
            {
                base.SetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.afterTaskPropsProperty, value);
            }
        }

        public static DependencyProperty beforeTaskPropsProperty = DependencyProperty.Register("beforeTaskProps", typeof(Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties), typeof(Microsoft.Samples.SequentialWorkflow.Workflow1));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties beforeTaskProps
        {
            get
            {
                return ((Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties)(base.GetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.beforeTaskPropsProperty)));
            }
            set
            {
                base.SetValue(Microsoft.Samples.SequentialWorkflow.Workflow1.beforeTaskPropsProperty, value);
            }
        }

        #endregion
        #region "Подпрограммы рабочего процесса"
        private void notCompleted(object sender, ConditionalEventArgs e)
        {
            //Для завершенной задачи результат должен быть 1,0
            e.Result = !taskCompleted;
        }

        private void onTaskChanged(object sender, ExternalDataEventArgs e)
        {
            //Проверка свойств задачи после внесения изменений.
            //Поиск значения 1,0 как признака выполненной задачи.
            if (afterTaskProps.PercentComplete == 1.0)
            {
                taskCompleted = true;
            }
        }

        private void TaskCreation(object sender, EventArgs e)
        {
            try
            {
                if (taskProperties == null)
                {
                    taskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
                }
                //У задачи должен быть GUID
                TaskID = Guid.NewGuid();
                //Настройка основных свойств задач
                taskProperties.PercentComplete = (float)0.0;
                taskProperties.AssignedTo = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                //Эта возможность очень удобна при встраивании форм InfoPath
                taskProperties.TaskType = 0;
                taskProperties.DueDate = DateTime.Now.AddDays(7);
                taskProperties.StartDate = DateTime.Now;
                taskProperties.Title = "SharePoint Workflow Task";
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }

}
