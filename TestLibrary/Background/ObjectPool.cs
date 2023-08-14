using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BaseGameLibrary
{
    public interface IPoolable 
    {
        /// <summary>
        /// Clear any data that does not need to be stored as this item is pooled
        /// </summary>        

        public void Die();
    }
    public class NotMyObjectException : ArgumentException
    {
        public override string Message => "Alien object submitted into the pool";
    }

    sealed class ObjectPool<T> where T : IPoolable
    {


        Func<T> templateGenerator;
        Stack<T> pooledItems = new();
        public static ObjectPool<T> Instance { get; } = new();
        private ObjectPool() { }



        public void Init(Func<T> templateGenerator) => this.templateGenerator = templateGenerator;

        public void Submit(T item) => pooledItems.Push(item);

        public T Borrow() => pooledItems.TryPop(out var item) ? item : templateGenerator();
    }

    sealed class ReflectiveObjectPool<T>
        where T : IPoolable
    {
        #region Singleton
        public static ReflectiveObjectPool<T> Instance { get; } = new ReflectiveObjectPool<T>();
        static ReflectiveObjectPool() { }
        private ReflectiveObjectPool() { }
        #endregion Singleton

        private Dictionary<Type, (Func<T> createFunc, Queue<T> objects)> objectMap = new Dictionary<Type, (Func<T>, Queue<T>)>();
        private HashSet<T> borrowedObjects = new HashSet<T>();

        public TSubClass Borrow<TSubClass>()
            where TSubClass : T
        {
            Type type = typeof(TSubClass);
            var queue = objectMap[type].objects;

            if (queue.Count <= 0)
            {
                GrowPool(queue, objectMap[type].createFunc, queue.Count + borrowedObjects.Count);
            }

            T @object = queue.Dequeue();
            borrowedObjects.Add(@object);
            return (TSubClass)@object;
        }

        public void Return<TSubClass>(TSubClass @object)
            where TSubClass : T
        {
            if (!borrowedObjects.Contains(@object)) throw new NotMyObjectException();

            borrowedObjects.Remove(@object);
            Type type = typeof(TSubClass);

            objectMap[type].objects.Enqueue(@object);
        }

        public void Populate<TSubClass>(int populationSize, Func<TSubClass> createFunc)
            where TSubClass : T
        {
            Type type = typeof(TSubClass);

            var queue = new Queue<T>();
            Func<T> createFuncWrapper = () => createFunc();

            objectMap[type] = (createFuncWrapper, queue);

            for (int i = 0; i < populationSize; i++)
            {
                GrowPool(queue, createFuncWrapper);
            }
        }

        private void GrowPool(Queue<T> queue, Func<T> createFunc, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                queue.Enqueue(createFunc());
            }
        }

    }
}
