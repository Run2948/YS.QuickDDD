/* ==============================================================================
* 命名空间：Quick.Data
* 类 名 称：ViewAndProcedureInitializer
* 创 建 者：Qing
* 创建时间：2018-10-26 17:32:59
* CLR 版本：4.0.30319.42000
* 保存的文件名：ViewAndProcedureInitializer
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
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data
{
    public static class ViewAndProcedureInitializer
    {
        public static void Init(DefaultDbContext context)
        {
            #region 视图的创建

            // lbk_album_detail_view
            #region lbk_album_detail_view
            //context.Database.ExecuteSqlCommand("lbk_album_detail_view".DropTable());
            //context.Database.ExecuteSqlCommand("lbk_album_detail_view".CreateView("CREATE VIEW [dbo].[lbk_album_detail_view] AS SELECT dbo.lbk_album.id as album_id, dbo.lbk_album.album_name as album_name, dbo.lbk_album.album_type as album_type, dbo.lbk_album.store_id as store_id, dbo.lbk_album.member_id as member_id, dbo.lbk_album.album_desc as album_desc, dbo.lbk_album.album_sort as album_sort, dbo.lbk_album.album_cover as album_cover, dbo.lbk_album.create_time as create_time, dbo.lbk_album.is_default as is_default, dbo.lbk_album.is_delete as is_delete, dbo.lbk_store.member_name as member_name, dbo.lbk_store.store_name as store_name FROM dbo.lbk_album INNER JOIN dbo.lbk_store ON dbo.lbk_album.member_id = dbo.lbk_store.member_id"));
            #endregion

            #endregion

            #region 存储过程的创建

            #endregion
        }


        #region 辅助方法
        public static string DropTable(this string tableName)
        {
            return $"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{tableName}]') AND type in (N'U')) DROP TABLE [dbo].[{tableName}]";
        }

        public static string CreateView(this string viewName, string createSql)
        {
            return $"IF NOT EXISTS(SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[{viewName}]')) EXEC dbo.sp_executesql @statement = N'{createSql}'";
        }

        public static string CreateProcedure(this string proceName, string createSql)
        {
            return $"IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{proceName}]') AND type in (N'P', N'PC')) BEGIN EXEC dbo.sp_executesql @statement = N'{createSql}' END";
        }
        #endregion
    }
}
