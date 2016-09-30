// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Fakes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Реализация IObjectSet на основании данных в памяти
    /// </summary>
    /// <typeparam name="TEntity">Тип данных, сохраняемых в наборе</typeparam>
    public sealed class FakeObjectSet<TEntity> : IObjectSet<TEntity> where TEntity : class
    {
        /// <summary>
        /// Базовые данные этого набора
        /// </summary>
        private HashSet<TEntity> data;

        /// <summary>
        /// Версия IQueryable базовых данных
        /// </summary>
        private IQueryable query;

        /// <summary>
        /// Инициализирует новый экземпляр класса FakeObjectSet.
        /// Экземпляр не содержит данных.
        /// </summary>
        public FakeObjectSet()
        {
            this.data = new HashSet<TEntity>();
            this.query = this.data.AsQueryable();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса FakeObjectSet.
        /// Экземпляр содержит предоставленные сведения о данных.
        /// </summary>
        /// <param name="testData">Данные, включаемые в набор</param>
        public FakeObjectSet(IEnumerable<TEntity> testData)
        {
            if (testData == null)
            {
                throw new ArgumentNullException("testData");
            }

            this.data = new HashSet<TEntity>(testData);
            this.query = this.data.AsQueryable();
        }

        /// <summary>
        /// Возвращает тип элементов в этом наборе
        /// </summary>
        Type IQueryable.ElementType
        {
            get { return this.query.ElementType; }
        }

        /// <summary>
        /// Возвращает дерево выражений для этого набора
        /// </summary>
        Expression IQueryable.Expression
        {
            get { return this.query.Expression; }
        }

        /// <summary>
        /// Возвращает поставщика запросов для этого набора
        /// </summary>
        IQueryProvider IQueryable.Provider
        {
            get { return this.query.Provider; }
        }

        /// <summary>
        /// Добавляет новый элемент в этот набор
        /// </summary>
        /// <param name="entity">Добавляемый элемент</param>
        public void AddObject(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.data.Add(entity);
        }

        /// <summary>
        /// Удаляет новый элемент из этого набора
        /// </summary>
        /// <param name="entity">Удаляемый элемент</param>
        public void DeleteObject(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.data.Remove(entity);
        }

        /// <summary>
        /// Прикрепляет новый элемент к этому набору
        /// </summary>
        /// <param name="entity">Прикрепляемый элемент</param>
        public void Attach(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.data.Add(entity);
        }

        /// <summary>
        /// Отделяет новый элемент от этого набора
        /// </summary>
        /// <param name="entity">Отделяемый элемент</param>
        public void Detach(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.data.Remove(entity);
        }

        /// <summary>
        /// Возвращает перечислитель для этого набора
        /// </summary>
        /// <returns>Возвращает перечислитель для всех элементов в этом наборе</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        /// <summary>
        /// Возвращает типизированный перечислитель для этого набора
        /// </summary>
        /// <returns>Возвращает перечислитель для всех элементов в этом наборе</returns>
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}