# YS.QuickDDD
一世计科快速开发框架

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