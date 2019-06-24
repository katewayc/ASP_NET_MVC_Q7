CREATE TABLE [dbo].[TodoList] (
    [TodoId]    INT           IDENTITY (1, 1) NOT NULL,
    [TodoWhat]  NVARCHAR (50) NULL,
    [Completed] BIT           DEFAULT ((0)) NOT NULL,
    [Deleted]   BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([TodoId] ASC)
);
