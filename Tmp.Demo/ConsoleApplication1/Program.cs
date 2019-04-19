using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<ViewFieldsInfo> sdf=  DbHelper.GetViewFieldsList("Data Source=(local);Initial Catalog=Northwind;User ID=sa;Password=Mjcy1989", "Northwind", "Alphabetical list of products");
            var result = DbHelper.GetConstraintField("Data Source=(local);Initial Catalog=Northwind;User ID=sa;Password=Mjcy1989", "Northwind", "Alphabetical list of products");

        }
    }


    public class DbHelper
    {
        public static List<DbTable> GetDbTables(string connectionString, string database, string tables = null)
        {

            if (!string.IsNullOrEmpty(tables))
            {
                tables = string.Format(" and obj.name in ('{0}')", tables.Replace(",", "','"));
            }
            string sql = string.Format(@"SELECT
                obj.name tablename,
                schem.name schemname,
                idx.rows,
                CAST
                (
                CASE 
                WHEN (SELECT COUNT(1) FROM sys.indexes WHERE object_id= obj.OBJECT_ID AND is_primary_key=1) >=1 THEN 1
                ELSE 0
                END 
                AS BIT) HasPrimaryKey 
      ,(
    SELECT count(*)
      FROM SYSOBJECTS sysobj
INNER JOIN SYSINDEXES sysidx ON sysobj.[name] = sysidx.[name]
INNER JOIN SYSINDEXKEYS sysidxkey ON sysidx.indid = sysidxkey.indid
INNER JOIN SYSCOLUMNS syscol ON sysidxkey.colid = syscol.colid
INNER JOIN SYSOBJECTS systab ON syscol.id = systab.id
     WHERE sysobj.xtype = 'PK'
	   AND sysidx.id = syscol.id
	   AND sysidxkey.id = syscol.id
	   AND syscol.id = syscol.id
       AND systab.xtype = 'U'
	   AND systab.[name] <> 'dtproperties'
	   AND systab.[name] = obj.[name]
) AmountPK
                from {0}.sys.objects obj 
                inner join {0}.dbo.sysindexes idx on obj.object_id=idx.id and idx.indid<=1
                INNER JOIN {0}.sys.schemas schem ON obj.schema_id=schem.schema_id
                where type='U' {1}
                order by obj.name", database, tables);

            DataTable dt = GetDataTable(connectionString, sql);
            return dt.Rows.Cast<DataRow>().Select(row => new DbTable
            {
                TableName = row.Field<string>("tablename"),
                SchemaName = row.Field<string>("schemname"),
                Rows = row.Field<int>("rows"),
                HasPrimaryKey = row.Field<bool>("HasPrimaryKey"),
                AmountPK = row.Field<int>("AmountPK")
            }).ToList();
        }

        public static List<DbColumn> GetDbColumns(string connectionString, string database, string tableName, string schema = "dbo")
        {
            string sql = string.Format(@"
                WITH indexCTE AS
                (
                SELECT 
                ic.column_id,
                ic.index_column_id,
                ic.object_id 
                FROM {0}.sys.indexes idx
                INNER JOIN {0}.sys.index_columns ic ON idx.index_id = ic.index_id AND idx.object_id = ic.object_id
                WHERE idx.object_id =OBJECT_ID(@tableName) AND idx.is_primary_key=1
                )
                select
                colm.column_id ColumnID,
                CAST(CASE WHEN indexCTE.column_id IS NULL THEN 0 ELSE 1 END AS BIT) IsPrimaryKey,
                colm.name ColumnName,
                systype.name ColumnType,
                colm.is_identity IsIdentity,
                colm.is_nullable IsNullable,
                cast(colm.max_length as int) ByteLength,
                (
                case 
                when systype.name='nvarchar' and colm.max_length>0 then colm.max_length/2 
                when systype.name='nchar' and colm.max_length>0 then colm.max_length/2
                when systype.name='ntext' and colm.max_length>0 then colm.max_length/2 
                else colm.max_length
                end
                ) CharLength,
                cast(colm.precision as int) Precision,
                cast(colm.scale as int) Scale,
                prop.value Remark
                from {0}.sys.columns colm
                inner join {0}.sys.types systype on colm.system_type_id=systype.system_type_id and colm.user_type_id=systype.user_type_id
                left join {0}.sys.extended_properties prop on colm.object_id=prop.major_id and colm.column_id=prop.minor_id
                LEFT JOIN indexCTE ON colm.column_id=indexCTE.column_id AND colm.object_id=indexCTE.object_id 
                where colm.object_id=OBJECT_ID(@tableName)
                order by colm.column_id", database);

            SqlParameter param = new SqlParameter("@tableName", SqlDbType.NVarChar, 100) { Value = string.Format("{0}.{1}.{2}", database, schema, tableName) };
            DataTable dt = GetDataTable(connectionString, sql, param);
            return dt.Rows.Cast<DataRow>().Select(row => new DbColumn()
            {
                ColumnID = row.Field<int>("ColumnID"),
                IsPrimaryKey = row.Field<bool>("IsPrimaryKey"),
                ColumnName = row.Field<string>("ColumnName"),
                ColumnType = row.Field<string>("ColumnType"),
                IsIdentity = row.Field<bool>("IsIdentity"),
                IsNullable = row.Field<bool>("IsNullable"),
                ByteLength = row.Field<int>("ByteLength"),
                CharLength = row.Field<int>("CharLength"),
                Scale = row.Field<int>("Scale"),
                Remark = row["Remark"].ToString()
            }).ToList();
        }

        /// <summary>
        /// 获取数据库视图列表
        /// </summary>
        public static List<ViewStruckInfo> GetViewDescStruct(string connectionString, string database, string type = "V")
        {
            string sql = string.Format(@"
                SELECT a.[name]
                      ,a.[type]
                      ,b.[definition]
                  FROM {0}.sys.all_objects a
            INNER JOIN {0}.sys.sql_modules b ON a.object_id = b.object_id
                 WHERE a.is_ms_shipped=0
                   AND a.[type] = @type
              ORDER BY a.[name] ASC
            --字段Type表示对象类型，常用的类型有：
            --AF = 聚合函数
            -- P = SQL 存储过程
            -- V = 视图
            --TT = 表类型
            -- U = 表（用户定义类型）", database);

            SqlParameter param = new SqlParameter("@type", SqlDbType.Char, 5) { Value = type };
            DataTable dt = GetDataTable(connectionString, sql, param);
            return dt.Rows.Cast<DataRow>().Select(row => new ViewStruckInfo
            {
                name = row.Field<string>("name"),
                type = row.Field<string>("type"),
                definition = row.Field<string>("definition")
            }).ToList();
        }

        /// <summary>
        /// 获取数据库视图字段详细信息
        /// </summary>
        public static List<ViewFieldsInfo> GetViewFieldsList(string connectionString, string database, string viewname)
        {
            string sql = string.Format(@"
			  SELECT o.[Name] As ObjectsName
			        ,c.[name] As ColumnsName
					,c.isnullable AS IsNull
			        ,t.[name] As ColumnsType
			        ,c.[length] As ColumnsLength
			    FROM {0}.sys.SysObjects As o
			        ,{0}.sys.SysColumns As c
			        ,{0}.sys.SysTypes As t
			   WHERE o.type IN ('V')
			     AND o.id = c.id
			     AND c.xtype = t.xtype
				 AND t.[name] <> 'sysname'
				 AND o.Name = '{1}'
			ORDER BY o.[name]
			        ,c.[name]
			        ,t.[name]
			        ,c.Length
			--字段Type表示对象类型，常用的类型有：
			--AF = 聚合函数
			-- P = SQL 存储过程
			-- V = 视图
			--TT = 表类型
			-- U = 表（用户定义类型）", database, viewname);

            DataTable dt = GetDataTable(connectionString, sql);
            return dt.Rows.Cast<DataRow>().Select(row => new ViewFieldsInfo
            {
                ObjectsName = row.Field<string>("ObjectsName"),
                ColumnsName = row.Field<string>("ColumnsName"),
                IsNull = row.Field<int>("IsNull") == 1 ? true : false,
                ColumnsType = row.Field<string>("ColumnsType"),
                ColumnsLength = row.Field<int>("ColumnsLength")
            }).ToList();
        }

        /// <summary>
        /// 获取数据库视图字段约束信息
        /// </summary>
        public static List<ViewPkIdentityInfo> GetConstraintField(string connectionString, string database, string viewname)
        {
            string sql = string.Format(@"
                WITH indexCTE AS
                (
                    SELECT ic.column_id
                          ,ic.index_column_id
                          ,ic.object_id
                      FROM {0}.sys.indexes idx
                INNER JOIN {0}.sys.index_columns ic
                        ON idx.index_id = ic.index_id
                       AND idx.object_id = ic.object_id
                INNER JOIN (
                SELECT referenced_entity_name
                      ,referenced_minor_name
                  FROM sys.dm_sql_referenced_entities('[dbo].{1}','OBJECT')
                 WHERE referenced_minor_name IS NULL) ref
                        ON ic.object_id = OBJECT_ID(ref.referenced_entity_name)
                     WHERE 1 = 1--idx.object_id = OBJECT_ID('数据库表名')
                	   AND idx.is_primary_key = 1
                )
                    SELECT ref.referenced_entity_name
                          ,colm.column_id ColumnID
                          ,CAST(CASE WHEN indexCTE.column_id IS NULL THEN 0 ELSE 1 END AS BIT) IsPrimaryKey
                          ,colm.name ColumnName
                          ,colm.is_identity IsIdentity
                      FROM {0}.sys.columns colm
                INNER JOIN {0}.sys.types systype
                        ON colm.system_type_id = systype.system_type_id
                       AND colm.user_type_id = systype.user_type_id
                 LEFT JOIN {0}.sys.extended_properties prop
                        ON colm.object_id = prop.major_id
                       AND colm.column_id = prop.minor_id
                 LEFT JOIN indexCTE
                        ON colm.column_id = indexCTE.column_id
                       AND colm.object_id = indexCTE.object_id
                INNER JOIN (
                SELECT referenced_entity_name
                      ,referenced_minor_name
                  FROM sys.dm_sql_referenced_entities('[dbo].{1}','OBJECT')
                 WHERE referenced_minor_name IS NULL) ref
                        ON colm.object_id = OBJECT_ID(ref.referenced_entity_name)
                     WHERE 1 = 1 --colm.object_id = OBJECT_ID('数据库表名')
                	   AND (indexCTE.column_id IS NOT NULL
                	    OR colm.is_identity = 1)
                  ORDER BY ref.referenced_entity_name,colm.column_id", database, viewname);

            DataTable dt = GetDataTable(connectionString, sql);
            return dt.Rows.Cast<DataRow>().Select(row => new ViewPkIdentityInfo
            {
                referenced_entity_name = row.Field<string>("referenced_entity_name"),
                ColumnID = row.Field<int>("ColumnID"),
                IsPrimaryKey = row.Field<bool>("IsPrimaryKey"),
                ColumnName = row.Field<string>("ColumnName"),
                IsIdentity = row.Field<bool>("IsIdentity")
            }).ToList();
        }

        public static DataTable GetDataTable(string connectionString, string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = commandText;
                command.Parameters.AddRange(parms);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }

    }


    /// <summary>
    /// 表结构
    /// </summary>
    public sealed class DbTable
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表的架构
        /// </summary>
        public string SchemaName { get; set; }
        /// <summary>
        /// 表的记录数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 是否含有主键
        /// </summary>
        public bool HasPrimaryKey { get; set; }

        /// <summary>
        /// 数据库表中主键的个数
        /// </summary>
		public int AmountPK { get; set; }
    }

    /// <summary>
    /// 表字段结构
    /// </summary>
    public sealed class DbColumn
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 数据库类型对应的C#类型
        /// </summary>
        public string CSharpType
        {
            get
            {
                return SqlServerDbTypeMap.MapCsharpType(ColumnType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type CommonType
        {
            get
            {
                return SqlServerDbTypeMap.MapCommonType(ColumnType);
            }
        }

        /// <summary>
        /// 字节长度
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// 字符长度
        /// </summary>
        public int CharLength { get; set; }

        /// <summary>
        /// 小数位
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 是否自增列
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 视图结构
    /// </summary>
	public sealed class ViewStruckInfo
    {
        /// <summary>
        /// 视图名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 视图创建脚本
        /// </summary>
        public string definition { get; set; }
    }

    /// <summary>
    /// 视图字段结构
    /// </summary>
	public sealed class ViewFieldsInfo
    {
        /// <summary>
        /// 视图名称
        /// </summary>
        public string ObjectsName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnsName { get; set; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool IsNull { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnsType { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int ColumnsLength { get; set; }

        /// <summary>
        /// 数据库类型对应的C#类型
        /// </summary>
        public string CSharpType
        {
            get
            {
                return SqlServerDbTypeMap.MapCsharpType(ColumnsType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type CommonType
        {
            get
            {
                return SqlServerDbTypeMap.MapCommonType(ColumnsType);
            }
        }
    }

    /// <summary>
    /// 视图主键以及自增长列信息
    /// </summary>
    public class ViewPkIdentityInfo
    {
        /// <summary>
        /// 视图依赖表名
        /// </summary>
        public string referenced_entity_name { get; set; }
        /// <summary>
        /// 列序号标识
        /// </summary>
        public int ColumnID { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 是否自增长
        /// </summary>
        public bool IsIdentity { get; set; }
    }

    public class SqlServerDbTypeMap
    {
        public static string MapCsharpType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return dbtype;
            dbtype = dbtype.ToLower();
            string csharpType = "object";
            switch (dbtype)
            {
                case "bigint": csharpType = "long"; break;
                case "binary": csharpType = "byte[]"; break;
                case "bit": csharpType = "bool"; break;
                case "char": csharpType = "string"; break;
                case "date": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "datetime2": csharpType = "DateTime"; break;
                case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                case "decimal": csharpType = "decimal"; break;
                case "float": csharpType = "double"; break;
                case "image": csharpType = "byte[]"; break;
                case "int": csharpType = "int"; break;
                case "money": csharpType = "decimal"; break;
                case "nchar": csharpType = "string"; break;
                case "ntext": csharpType = "string"; break;
                case "numeric": csharpType = "decimal"; break;
                case "nvarchar": csharpType = "string"; break;
                case "real": csharpType = "Single"; break;
                case "smalldatetime": csharpType = "DateTime"; break;
                case "smallint": csharpType = "short"; break;
                case "smallmoney": csharpType = "decimal"; break;
                case "sql_variant": csharpType = "object"; break;
                case "sysname": csharpType = "object"; break;
                case "text": csharpType = "string"; break;
                case "time": csharpType = "TimeSpan"; break;
                case "timestamp": csharpType = "byte[]"; break;
                case "tinyint": csharpType = "byte"; break;
                case "uniqueidentifier": csharpType = "Guid"; break;
                case "varbinary": csharpType = "byte[]"; break;
                case "varchar": csharpType = "string"; break;
                case "xml": csharpType = "string"; break;
                default: csharpType = "object"; break;
            }
            return csharpType;
        }

        public static Type MapCommonType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return Type.Missing.GetType();
            dbtype = dbtype.ToLower();
            Type commonType = typeof(object);
            switch (dbtype)
            {
                case "bigint": commonType = typeof(long); break;
                case "binary": commonType = typeof(byte[]); break;
                case "bit": commonType = typeof(bool); break;
                case "char": commonType = typeof(string); break;
                case "date": commonType = typeof(DateTime); break;
                case "datetime": commonType = typeof(DateTime); break;
                case "datetime2": commonType = typeof(DateTime); break;
                case "datetimeoffset": commonType = typeof(DateTimeOffset); break;
                case "decimal": commonType = typeof(decimal); break;
                case "float": commonType = typeof(double); break;
                case "image": commonType = typeof(byte[]); break;
                case "int": commonType = typeof(int); break;
                case "money": commonType = typeof(decimal); break;
                case "nchar": commonType = typeof(string); break;
                case "ntext": commonType = typeof(string); break;
                case "numeric": commonType = typeof(decimal); break;
                case "nvarchar": commonType = typeof(string); break;
                case "real": commonType = typeof(Single); break;
                case "smalldatetime": commonType = typeof(DateTime); break;
                case "smallint": commonType = typeof(short); break;
                case "smallmoney": commonType = typeof(decimal); break;
                case "sql_variant": commonType = typeof(object); break;
                case "sysname": commonType = typeof(object); break;
                case "text": commonType = typeof(string); break;
                case "time": commonType = typeof(TimeSpan); break;
                case "timestamp": commonType = typeof(byte[]); break;
                case "tinyint": commonType = typeof(byte); break;
                case "uniqueidentifier": commonType = typeof(Guid); break;
                case "varbinary": commonType = typeof(byte[]); break;
                case "varchar": commonType = typeof(string); break;
                case "xml": commonType = typeof(string); break;
                default: commonType = typeof(object); break;
            }
            return commonType;
        }
    }

}
