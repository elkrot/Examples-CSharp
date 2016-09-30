// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.Model
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Тестирует поведение исправления версий объектов Pure POCO в модели
    /// </summary>
    [TestClass]
    public class BaseModelTypeFixupTests : FixupTestsBase
    {
        /// <summary>
        /// Возвращает экземпляр T, созданный конструктором по умолчанию
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый экземпляр типа T</returns>
        protected override T CreateObject<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
