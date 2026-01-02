using System;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Tank.Request
{
	// Token: 0x0200002E RID: 46
	[Database(Name = "Project_Member34")]
	public class DbMemberDataContext : DataContext
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000024FF File Offset: 0x000006FF
		public DbMemberDataContext() : base(ConfigurationManager.ConnectionStrings["Project_Member34ConnectionString"].ConnectionString, DbMemberDataContext.mappingSource)
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002522 File Offset: 0x00000722
		public DbMemberDataContext(string connection) : base(connection, DbMemberDataContext.mappingSource)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002532 File Offset: 0x00000732
		public DbMemberDataContext(IDbConnection connection) : base(connection, DbMemberDataContext.mappingSource)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002542 File Offset: 0x00000742
		public DbMemberDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000254E File Offset: 0x0000074E
		public DbMemberDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00008008 File Offset: 0x00006208
		public Table<Member_Info> Member_Infos
		{
			get
			{
				return base.GetTable<Member_Info>();
			}
		}

		// Token: 0x0400002C RID: 44
		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
