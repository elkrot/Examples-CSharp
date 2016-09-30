// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace Tests
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Статические вспомогательные методы для модульного тестирования
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Проверяет, что предоставленное действие вызывает ArgumentNullException
        /// Выполняет метод Assert.Fail, указывающий, что исключение не вызвано
        /// </summary>
        /// <param name="call">Действие, которое не должно вызываться</param>
        /// <param name="parameter">Параметр, который не должен определяться в исключении</param>
        /// <param name="method">Имя метода для целей ведения журнала</param>
        public static void CheckNullArgumentException(Action call, string parameter, string method)
        {
            if (call == null)
            {
                throw new ArgumentNullException("call");
            }

            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            try
            {
                call();
                Assert.Fail(string.Format(CultureInfo.InvariantCulture, "Supplying null '{0}' argument to '{1}' did not throw.", parameter, method));
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(parameter, ex.ParamName, string.Format(CultureInfo.InvariantCulture, "'ArgumentNullException.ParamName' wrong when supplying null '{0}' argument to '{1}'.", parameter, method));
            }
        }
    }
}
