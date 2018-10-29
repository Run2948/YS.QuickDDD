# YS.QuickDDD
一世计科快速开发框架

#### 相关技术介绍
* 1.Asp.Net Mvc 5.2.6
* 2.Entity Framework 6.2
* 3.AutoFac
* 4.AutoMapper
* 5.log4net
* 6.SignalR
* 7.Redis
* 8.Hangfire

#### CodeFirst 生成数据库
* 第一步： 在控制台中输入Enable-Migrations(启动迁移)。注意：默认项目一定要是模型所在的项目。
    + 此时项目中会自动生成Migration文件夹
    + 它有两个方法：
        - a、Configuration():无参的构造方法。可以在这里配置迁移之前的操作，后文再详细描述。
        - b、Seed(DefaultDbContext):void:这个方法是在执行迁移成功之后执行，一般用来初始化数据库用的。例如：我要在生成数据库的时候就初始化一条记录。
* 第二步：在控制台输入：Add-Migration InitModel。(InitModel:为本次迁移起个名字）
    + 此时在项目的Migration文件夹中会自动生成迁移记录文件，文件名以“当前时间＿本次迁移的名字”作为类名。
    + 记录文件有一个设计类和一个资源类和一个迁移具体操作的方法:
       - 设计类：本次迁移的具体记录。自动生成、维护。
       - 资源类：默认架构和目标。自动生成、维护。
       - 具体操作：该类维护Up()和Down()两个方法，分别用于升级和降级。有时候需要我们自己定制它，因此要理解它，可以参考我写的注释。
* 第三步：在控制台输入Update-DataBase –script。生成Sql脚本。（在迁移过程中出现奇葩问题的时候，就生成sql脚本检查一下吧。）
* 下面继续在控制台输入：Update-DataBase –verbose(或者-v)。更新数据库，并且查看执行的具体sql语句。

#### 数据库切换步骤：
* 1.修改Quick.Data中的 QuickDbProvider 的 DataBaseProvider 默认值并开始编译
* 2.编译通过就转换T4模板生成 数据库对应的配置
* 3.修改Web.config下面 DbType
* 4.复制 Config 文件夹中 entityFramework 和 system.data 配置到Web.config文件中
* 5.在包管理器控制台 输入：Update-DataBase 命令生成数据库
* 6.Sqlite数据直接启动项目即可生成.db文件
* 7.Access数据库需要在指定的数据库目录下创建新的空白数据库文件。

#### 下一步扩展计划：
* 1.数据库读写分离
  * [asp.net实现数据库读写分离(SQLSERVER2005,ORACLE)](https://www.cnblogs.com/ToughGuy/p/3899968.html) 
  * [基于 EntityFramework 的数据库主从读写分离服务插件](https://www.cnblogs.com/cjw0511/p/4391092.html) 
* 2.SSO 单点登录 
  * [Asp.net单点登录解决方案](https://www.cnblogs.com/wu-jian/archive/2012/11/14/2756694.html)
  * [ASP.NET MVC SSO单点登录设计与实现](https://www.cnblogs.com/smartbooks/p/3800849.html) 
* 3.Nginx负载均衡
  * [ASP.NET中如何实现负载均衡](http://www.poluoluo.com/jzxy/201008/91869.html)
  * [使用Nginx负载均衡搭建高性能.NETweb应用程序一](https://blog.csdn.net/huangxiangec/article/details/41597481)
  * [使用Nginx负载均衡搭建高性能.NETweb应用程序二](https://blog.csdn.net/huangxiangec/article/details/41723491)
  * [.Net分布式架构(一)：Nginx实现负载均衡](https://blog.csdn.net/orichisonic/article/details/71122291)
  * [.Net分布式架构(二)：基于Redis的Session共享](https://blog.csdn.net/orichisonic/article/details/80937582)
* 4.数据库灾备
  * [SQL Server 2008 R2双机热备](https://blog.csdn.net/wangqi791975/article/details/50208071)
  * [Sql Server 2008 数据库实时同步复制](https://jingyan.baidu.com/article/9f7e7ec0bc3bb76f2915544c.html)
  * [SQL Server 2008 R2 主从数据库同步](https://www.cnblogs.com/tatsuya/p/5025583.html)