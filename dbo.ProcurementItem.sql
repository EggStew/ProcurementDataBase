CREATE TABLE [dbo].[ProcurementItem] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [CompName] NVARCHAR (MAX)  NULL,
    [Price]    DECIMAL (18, 2) NULL,
    [Quantity] INT             NULL,
    [HireTime] NVARCHAR (MAX)  NULL,
    [Source]   NVARCHAR (MAX)  NULL,
    [Date]     NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_ProcurementItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);

