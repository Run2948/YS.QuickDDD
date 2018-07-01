/* ==============================================================================
* 命名空间：Quick.Data.Infrastructure
* 类 名 称：EntityClassInfo
* 创 建 者：Qing
* 创建时间：2018-06-01 14:34:48
* CLR 版本：4.0.30319.42000
* 保存的文件名：EntityClassInfo
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Infrastructure
{
    public class EntityClassInfo
    {
        public EntityClassInfo()
        {
            List<string> classNameList = new List<string>();

            PropertyInfo[] properties = typeof(DefaultDbContext).GetProperties();    // 获得对象所有属性
            foreach (var property in properties)
            {
                string propertyType = property.PropertyType.Name;   // 获得属性类型名称
                if (propertyType.Contains("DbSet"))     // 判断是否为实体集合
                {
                    Type[] genericTypes = property.PropertyType.GenericTypeArguments;   // 获得泛型类型数组
                    foreach (var type in genericTypes)
                    {
                        classNameList.Add(type.Name);   // 获得泛型类型名称 并添加到集合中
                    }
                }
            }
            this.EntitiesList = classNameList;
        }

        public List<string> EntitiesList { get; set; }
    }
}
