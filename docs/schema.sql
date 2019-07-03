USE [Training]
GO

/****** Object:  Table [dbo].[TodoList]    Script Date: 2019/7/3 下午 04:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TodoList](
	[TodoId] [int] IDENTITY(1,1) NOT NULL,
	[TodoWhat] [nvarchar](50) NULL,
	[Completed] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_TodoList] PRIMARY KEY CLUSTERED 
(
	[TodoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TodoList] ADD  CONSTRAINT [DF_TodoList_Completed]  DEFAULT ((0)) FOR [Completed]
GO

ALTER TABLE [dbo].[TodoList] ADD  CONSTRAINT [DF_TodoList_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TodoList', @level2type=N'COLUMN',@level2name=N'Completed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已刪除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TodoList', @level2type=N'COLUMN',@level2name=N'Deleted'
GO


