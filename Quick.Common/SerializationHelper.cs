/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：SerializationHelper
* 创 建 者：Qing
* 创建时间：2018-07-01 17:33:51
* CLR 版本：4.0.30319.42000
* 保存的文件名：SerializationHelper
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quick.Common
{
    public static class SerializationHelper
    {
        #region Serializate
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static object Load(this Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                // open the stream...
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs?.Close();
            }
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save(this object obj, string filename)
        {
            FileStream fs = null;
            // serialize it...
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs?.Close();
            }
        } 
        #endregion

        #region DeepCopy
        /// <summary>
        /// 对象深拷贝
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">Object</param>
        /// <returns>Object</returns>
        public static T DeepCopy<T>(this T obj)
        {
            /*
            *相关知识：
            *C# 支持两种类型：“值类型”和“引用类型”。 
            *值类型（Value Type)（如 char、int 和 float）、枚举类型和结构类型。 
            *引用类型(Reference Type) 包括类 (Class) 类型、接口类型、委托类型和数组类型。 
            *如何来划分它们？
            *以它们在计算机内存中如何分配来划分
            *1,值类型的变量直接包含其数据。
            *2,引用类型的变量则存储对象引用。
            *对于引用类型，两个变量可能引用同一个对象，因此对一个变量的操作可能影响另一个变量所引用的对象。
            *对于值类型，每个变量都有自己的数据副本，对一个变量的操作不可能影响另一个变量。
            *
            *堆栈(stack)是一种先进先出的数据结构，在内存中，变量会被分配在堆栈上来进行操作。
            *堆(heap)是用于为类型实例(对象)分配空间的内存区域，在堆上创建一个对象，会将对象的地址传给堆栈上的变量(反过来叫变量指向此对象，或者变量引用此对象)。
            *
            *浅拷贝：是指将对象中的所有字段逐字复杂到一个新对象
            *对值类型字段只是简单的拷贝一个副本到目标对象，改变目标对象中值类型字段的值不会反映到原始对象中，因为拷贝的是副本。
            *对引用型字段则是指拷贝他的一个引用到目标对象。改变目标对象中引用类型字段的值它将反映到原始对象中，因为拷贝的是指向堆是上的一个地址。
            *
            *深拷贝：深拷贝与浅拷贝不同的是对于引用字段的处理，深拷贝将会在新对象中创建一个新的对象和
            *原始对象中对应字段相同（内容相同）的字段，也就是说这个引用和原始对象的引用是不同， 我们改变新
            *对象中这个字段的时候是不会影响到原始对象中对应字段的内容。
            *
            *浅复制： 实现浅复制需要使用Object类的MemberwiseClone方法用于创建一个浅表副本
            *深复制： 须实现 ICloneable接口中的Clone方法，且需要需要克隆的对象加上[Serializable]特性
            *以上参考：http://www.kingreatwill.com/
            */
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException($"该类型:{typeof(T).FullName}不支持序列化", "obj");
            }
            if (obj == null)
            {
                return default(T);
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Clones the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List">The list.</param>
        /// <returns>List{``0}.</returns>
        public static List<T> DeepCopy<T>(this object List)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException($"该类型:{typeof(T).FullName}不支持序列化", "obj");
            }

            IFormatter formatter = new BinaryFormatter();
            using (Stream objectStream = new MemoryStream())
            {
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }
        #endregion
    }
}
