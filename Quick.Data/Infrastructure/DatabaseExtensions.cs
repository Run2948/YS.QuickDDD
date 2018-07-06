/* ==============================================================================
* 命名空间：Quick.Data.Infrastructure
* 类 名 称：DatabaseExtensions
* 创 建 者：Qing
* 创建时间：2018-07-06 12:56:26
* CLR 版本：4.0.30319.42000
* 保存的文件名：DatabaseExtensions
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Infrastructure
{
    //SqlQueryForDynamic的扩展方法

    public static class DatabaseExtensions
    {
        public static DataTable SqlQueryForDataTable(this Database db, string sql, params object[] parameters)
        {           
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = db.Connection.ConnectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    if (parameters != null && parameters.Length > 0)
                    {
                        foreach (var item in parameters)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        public static IEnumerable SqlQueryForDynamic(this Database db, string sql, params object[] parameters)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = db.Connection.ConnectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    using (IDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (!dataReader.Read())
                        {
                            return null; //无结果返回Null
                        }

                        #region 构建动态字段

                        TypeBuilder builder = DatabaseExtensions.CreateTypeBuilder("EF_DynamicModelAssembly", "DynamicModule", "DynamicType");

                        int fieldCount = dataReader.FieldCount;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            //dic.Add(i, dataReader.GetName(i));
                            //Type type = dataReader.GetFieldType(i);
                            CreateAutoImplementedProperty(builder, dataReader.GetName(i), dataReader.GetFieldType(i));
                        }
                        #endregion

                        Type returnType = builder.CreateType();
                        if (parameters != null && parameters.Length > 0)
                        {
                            return db.SqlQuery(returnType, sql, parameters);
                        }
                        else
                        {
                            return db.SqlQuery(returnType, sql);
                        }
                    }
                }              
            }
        }

        public static TypeBuilder CreateTypeBuilder(string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run).DefineDynamicModule(moduleName).DefineType(typeName, TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            return typeBuilder;
        }

        public static void CreateAutoImplementedProperty(TypeBuilder builder, string propertyName, Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = builder.DefineField(string.Concat(PrivateFieldPrefix, propertyName), propertyType, FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = builder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.HasDefault, propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = builder.DefineMethod(string.Concat(GetterPrefix, propertyName), propertyMethodAttributes, propertyType, Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = builder.DefineMethod(string.Concat(SetterPrefix, propertyName), propertyMethodAttributes, null, new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
    }
}
