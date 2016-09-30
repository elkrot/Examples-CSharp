// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;

namespace PasteXmlAsLinq {
    /// <summary>Объект для реализации надстройки.</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : Object, IDTExtensibility2, IDTCommandTarget {
        /// <summary>Реализация конструктора для объекта надстройки. Помещайте в этот метод ваш код инициализации.</summary>
        public Connect() {
        }

        /// <summary>Реализация метода OnConnection интерфейса IDTExtensibility2. Получение уведомления о загрузке надстройки.</summary>
        /// <param term='Application'>Корневой объект ведущего приложения.</param>
        /// <param term='ConnectMode'>Описание способа загрузки надстройки.</param>
        /// <param term='AddInInst'>Объект, представляющий данную надстройку.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object Application, ext_ConnectMode ConnectMode, object AddInInst, ref Array custom) {
            applicationObject = (DTE2)Application;
            addInInstance = (AddIn)AddInInst;
            if (ConnectMode == ext_ConnectMode.ext_cm_UISetup) {
                object[] contextGUIDS = new object[] { };
                Commands2 commands = (Commands2)applicationObject.Commands;

                try {
                    CommandBar menuBarCommandBar;
                    CommandBarControl toolsControl;
                    CommandBarPopup toolsPopup;
                    CommandBarControl commandBarControl;

                    //Добавление команды в коллекцию Commands:
                    Command command = commands.AddNamedCommand2(addInInstance, "PasteXmlAsLinq", "Paste XML as XElement", "Pastes the XML on the clipboard as C# Linq To Xml code", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                    String editMenuName;

                    //Поиск панели команд верхнего уровня MenuBar, в которой содержатся все элементы главного меню:
                    menuBarCommandBar = ((CommandBars)applicationObject.CommandBars)["MenuBar"];

                    try {

                        //  С помощью этого кода можно получить региональный язык и региональные параметры, добавить имя элемента меню,
                        //  а затем добавить команду в меню. Список всех меню верхнего уровня можно найти в файле
                        //  CommandBar.resx.
                        System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("PasteXmlAsLinq.CommandBar", System.Reflection.Assembly.GetExecutingAssembly());
                        System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
                        System.Globalization.CultureInfo cultureInfo = thread.CurrentUICulture;
                        editMenuName = resourceManager.GetString(String.Concat(cultureInfo.TwoLetterISOLanguageName, "Edit"));
                        toolsControl = menuBarCommandBar.Controls["Edit"];
                    }
                    catch (Exception) {
                        //  Мы пытаемся найти локализованную версию слова Edit, но безуспешно.
                        //  По умолчанию текст установлен в формат en-US, для используемого языка и региональных параметров этот вариант может оказаться приемлемым.
                        toolsControl = menuBarCommandBar.Controls["Edit"];
                    }

                    //Размещение команды в меню редактирования.
                    toolsPopup = (CommandBarPopup)toolsControl;
                    int pasteControlIndex = 1;

                    //Поиск элемента управления вставкой для добавления после него нового элемента.
                    foreach (CommandBarControl commandBar in toolsPopup.CommandBar.Controls) {
                        if (String.Compare(commandBar.Caption, "&Paste", StringComparison.OrdinalIgnoreCase) == 0) {
                            pasteControlIndex = commandBar.Index + 1;
                            break;
                        }
                    }

                    //Поиск подходящей панели команд на панели MenuBar:
                    commandBarControl = (CommandBarControl)command.AddControl(toolsPopup.CommandBar, pasteControlIndex);
                }
                catch (Exception) {
                }
            }
        }

        /// <summary>Реализация метода OnDisconnection интерфейса IDTExtensibility2. Получение уведомления о выгрузке надстройки.</summary>
        /// <param term='ConnectMode'>Описание способа выгрузки надстройки.</param>
        /// <param term='custom'>Массив параметров, характерных для ведущего приложения.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode RemoveMode, ref Array custom) {
        }

        /// <summary>Реализация метода OnAddInsUpdate интерфейса IDTExtensibility2. Получение уведомления при изменении коллекции надстройки.</summary>
        /// <param term='custom'>Массив параметров, характерных для ведущего приложения.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom) {
        }

        /// <summary>Реализация метода OnStartupComplete интерфейса IDTExtensibility2. Получение уведомления о завершении загрузки ведущего приложения.</summary>
        /// <param term='custom'>Массив параметров, характерных для ведущего приложения.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom) {
        }

        /// <summary>Реализация метода OnBeginShutdown интерфейса IDTExtensibility2. Получение уведомления о выгрузке ведущего приложения.</summary>
        /// <param term='custom'>Массив параметров, характерных для ведущего приложения.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom) {
        }

        /// <summary>Реализация метода QueryStatus интерфейса IDTCommandTarget. Этот метод вызывается при обновлении доступности команды</summary>
        /// <param term='CmdName'>Имя команды, для которой определяется состояние.</param>
        /// <param term='NeededText'>Текст, необходимый для команды.</param>
        /// <param term='StatusOption'>Состояние команды в интерфейсе пользователя.</param>
        /// <param term='CommandText'>Текст, запрошенный параметром NeededText.</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string CmdName, vsCommandStatusTextWanted NeededText, ref vsCommandStatus StatusOption, ref object CommandText) {
            if (NeededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone) {
                if (CmdName == "PasteXmlAsLinq.Connect.PasteXmlAsLinq") {
                    StatusOption = (vsCommandStatus)vsCommandStatus.vsCommandStatusUnsupported;
                    if (applicationObject.ActiveDocument != null) {
                        string xml = (string)Clipboard.GetDataObject().GetData(typeof(string));
                        if (xml != null && Converter.CanConvert(xml.Trim())) {
                            StatusOption = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                        }
                    }
                }
            }
        }

        /// <summary>Реализация метода Exec интерфейса IDTCommandTarget. Этот метод вызывается при запуске команды</summary>
        /// <param term='CmdName'>Имя выполняемой команды.</param>
        /// <param term='ExecuteOption'>Описание способа выполнения команды.</param>
        /// <param term='VariantIn'>Параметры, передаваемые вызывающим объектом в обработчик команды.</param>
        /// <param term='VariantOut'>Параметры, передаваемые обработчиком команды в вызывающий объект.</param>
        /// <param term='Handled'>Сообщение вызывающему объекту об обработке или не обработке команды.</param>
        /// <seealso class='Exec' />
        public void Exec(string CmdName, vsCommandExecOption ExecuteOption, ref object VariantIn, ref object VariantOut, ref bool Handled) {
            Handled = false;
            if (ExecuteOption == vsCommandExecOption.vsCommandExecOptionDoDefault) {
                if (CmdName == "PasteXmlAsLinq.Connect.PasteXmlAsLinq") {
                    Document doc = applicationObject.ActiveDocument;
                    if (doc != null) {
                        string xml = (string)Clipboard.GetDataObject().GetData(typeof(string));
                        if (xml != null) {
                            try {
                                string code = Converter.Convert(xml);
                                TextSelection s = (TextSelection)doc.Selection;
                                s.Insert(code, (int)vsInsertFlags.vsInsertFlagsContainNewText);
                                applicationObject.ExecuteCommand("Edit.FormatSelection", "");
                            }
                            catch (Exception e) {
                                MessageBox.Show("Clipboard does not contain valid XML.\r\n" + e.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    Handled = true;
                    return;
                }
            }
        }
        private DTE2 applicationObject;
        private AddIn addInInstance;
    }
}