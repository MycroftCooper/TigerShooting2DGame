using System.Collections.Generic;
using UnityEngine;

namespace QuickCode {
    public class ObjectPool<T> where T : class, new() {
        private Queue<T> pool;
        /// <summary>
        /// 对象池大小(-1无限大)
        /// </summary>
        public int Size;
        /// <summary>
        /// 池内可用对象个数
        /// </summary>
        public int CanUseCount { get => pool.Count; }
        public int UsingCount;
        public ObjectPool(int size) {
            pool = new Queue<T>();
            Size = size;
            for (int i = 0; i < Size; i++) {
                pool.Enqueue(new T());
            }
            UsingCount = 0;
        }

        /// <summary>
        /// 从池里面取类对象
        /// </summary>
        /// <param name="createIfPoolEmpty">如果为空是否new出来</param>
        public T GetObject(bool createIfPoolEmpty) {
            T output = pool.Dequeue();
            if (output != null) { // 池子没空
                UsingCount++;
                return output;
            }
            if (createIfPoolEmpty || Size == -1) { // 池子空了
                UsingCount++;
                return new T();
            }
            return null;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj">要回收的对象</param>
        /// <returns></returns>
        public bool Recycle(T obj) {
            if (obj == null) return false;
            UsingCount--;
            if (Size > 0 && pool.Count >= Size) {
                obj = null;
                return false;
            }
            pool.Enqueue(obj);
            return true;
        }
    }

    public class GameObjectPool {
        private Queue<GameObject> pool;
        public int Size;
        public int CanUseCount { get => pool.Count; }
        public int UsingCount;
        public GameObject Prefab;
        public Transform Parent;
        public void InitPool(int size, GameObject prefab, Transform parent = null, bool setActive = false) {
            if (size < -1) {
                Debug.LogError("非法的对象池大小!");
                return;
            }

            pool = new Queue<GameObject>();
            Size = size;
            Parent = parent;
            Prefab = prefab;

            for (int i = 0; i < Size; i++) {
                pool.Enqueue(CreateNewGO(setActive));
            }
            UsingCount = 0;
        }
        private GameObject CreateNewGO(bool setActive) {
            GameObject newObject;
            if (Prefab != null)
                newObject = GameObject.Instantiate(Prefab);
            else
                newObject = new GameObject();
            newObject.transform.parent = Parent;
            if (Parent != null) newObject.transform.localPosition = Vector3.zero;
            newObject.SetActive(setActive);
            return newObject;
        }
        /// <summary>
        /// 从池里面取类对象
        /// </summary>
        /// <param name="createIfPoolEmpty">如果为空是否new出来</param>
        public GameObject GetObject(bool createIfPoolEmpty, bool setActive = true) {
            GameObject output = pool.Dequeue();
            if (output != null) { // 池子没空
                UsingCount++;
                output.gameObject.SetActive(setActive);
                return output;
            }
            if (createIfPoolEmpty || Size == -1) { // 池子空了
                UsingCount++;
                GameObject newObject = CreateNewGO(setActive);
                return newObject;
            }
            return null;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj">要回收的对象</param>
        /// <returns></returns>
        public bool Recycle(GameObject obj, bool setActive = false) {
            if (obj == null) return false;
            UsingCount--;
            if (Size > 0 && pool.Count >= Size) {
                GameObject.Destroy(obj, 0f);
                return false;
            }
            obj.gameObject.SetActive(setActive);
            pool.Enqueue(obj);
            return true;
        }
        /// <summary>
        /// 清空对象池
        /// </summary>
        public void CleanPool() {
            while (pool.Peek() != null) {
                GameObject go = pool.Dequeue();
                GameObject.Destroy(go, 0f);
            }
            UsingCount = 0;
        }
    }

    public class ComponentPool<T> where T : Behaviour, new() {
        public GameObject Parent;
        private Queue<T> pool;
        public int Size;
        /// <summary>
        /// 池内可用对象个数
        /// </summary>
        public int CanUseCount { get => pool.Count; }
        public int UsingCount;

        public bool InitPool(GameObject parent, int size = 10, bool setEnabled = false) {
            if (size < 1 || parent == null) {
                Debug.LogError("对象池大小或父物体出错!");
                return false;
            }
            pool = new Queue<T>();
            Size = size;
            Parent = parent;

            for (int i = 0; i < Size; i++) {
                pool.Enqueue(addNewComponentToParent(setEnabled));
            }
            UsingCount = 0;

            return true;
        }
        private T addNewComponentToParent(bool setEnabled = false) {
            T output = Parent.AddComponent<T>();
            if (output == null) {
                Debug.LogError("脚本出错!");
                return null;
            }
            output.enabled = setEnabled;
            return output;
        }

        /// <summary>
        /// 从池里面取类对象
        /// </summary>
        /// <param name="createIfPoolEmpty">如果为空是否new出来</param>
        public T GetObject(bool createIfPoolEmpty, bool setEnabled = true) {
            T output = pool.Dequeue();
            if (output != null) { // 池子没空
                UsingCount++;
                output.enabled = setEnabled;
                return output;
            }
            if (createIfPoolEmpty) { // 池子空了
                UsingCount++;
                output = addNewComponentToParent(setEnabled = false);
                return output;
            }
            return null;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj">要回收的对象</param>
        /// <returns></returns>
        public bool Recycle(T obj, bool setEnabled = false) {
            if (obj == null) return false;
            UsingCount--;
            if (pool.Count >= Size) {
                GameObject.Destroy(obj);
                return true;
            }
            obj.enabled = setEnabled;
            pool.Enqueue(obj);
            return true;
        }
        /// <summary>
        /// 清空对象池
        /// </summary>
        public void CleanPool() {
            while (pool.Peek() != null) {
                T obj = pool.Dequeue();
                GameObject.Destroy(obj);
            }
            UsingCount = 0;
        }
    }
}
