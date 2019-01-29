if ((select COUNT(1) from sys.databases where name = 'iwsystem2') = 0 )
BEGIN
	CREATE DATABASE iwsystem2;
END


USE [iwsystem]
GO

/****** Object:  Table [dbo].[userInfo]    Script Date: 2019/1/25 17:52:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[userInfo](
	[id] [NCHAR](36) NOT NULL,
	[name] [NVARCHAR](200) NULL,
	[phone] [NVARCHAR](50) NULL,
	[sex] [SMALLINT] NOT NULL CONSTRAINT [DF_userInfo_sex]  DEFAULT ((0)),
	[age] [SMALLINT] NOT NULL CONSTRAINT [DF_userInfo_age]  DEFAULT ((0)),
	[isEnable] [BIT] NOT NULL CONSTRAINT [DF_userInfo_isEnable]  DEFAULT ((1)),
	[createTime] [DATETIME] NOT NULL CONSTRAINT [DF_userInfo_createTime]  DEFAULT ((GETDATE())),
	[lastUpdateDate] [DATETIME] NULL,
 CONSTRAINT [PK_userInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Ix_name] ON [dbo].[userInfo]
(
	[name] DESC,
	[phone] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 95) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Ix_phone] ON [dbo].[userInfo]
(
	[phone] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 95) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Ix_enable] ON [dbo].[userInfo]
(
	[isEnable] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 95) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Ix_createTime] ON [dbo].[userInfo]
(
	[createTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 95) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'userInfo', @level2type=N'COLUMN',@level2name=N'name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'userInfo', @level2type=N'COLUMN',@level2name=N'Phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'userInfo', @level2type=N'COLUMN',@level2name=N'sex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'userInfo', @level2type=N'COLUMN',@level2name=N'age'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'userInfo', @level2type=N'COLUMN',@level2name=N'isEnable'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'userInfo', @level2type=N'COLUMN',@level2name=N'createTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'userInfo', @level2type=N'COLUMN',@level2name=N'lastUpdateDate'
GO


CREATE TABLE [dbo].[viewPageInfo](
	[pageId] [NVARCHAR](36) NOT NULL,
	[pageName] [NVARCHAR](200) NULL,
	[viewNum] [INT] NOT NULL CONSTRAINT [DF_viewPageInfo_num]  DEFAULT ((0)),
	[isEnable] [BIT] NOT NULL CONSTRAINT [DF_viewPageInfo_isEnable]  DEFAULT ((0)),
	[createTime] [DATETIME] NOT NULL CONSTRAINT [DF_viewPageInfo_createTime]  DEFAULT (GETDATE()),
	[lastUpdateDate] [DATETIME] NULL,
 CONSTRAINT [PK_viewPageInfo] PRIMARY KEY CLUSTERED 
(
	[pageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网页id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'viewPageInfo', @level2type=N'COLUMN',@level2name=N'pageId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网页名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'viewPageInfo', @level2type=N'COLUMN',@level2name=N'pageName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'viewPageInfo', @level2type=N'COLUMN',@level2name=N'viewNum'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'viewPageInfo', @level2type=N'COLUMN',@level2name=N'isEnable'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'viewPageInfo', @level2type=N'COLUMN',@level2name=N'createTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'viewPageInfo', @level2type=N'COLUMN',@level2name=N'lastUpdateDate'
GO



