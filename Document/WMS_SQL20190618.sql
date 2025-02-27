USE [master]
GO
/****** Object:  Database [WMS]    Script Date: 2019-6-18 16:49:27 ******/
CREATE DATABASE [WMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\WMS.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\WMS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WMS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [WMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WMS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WMS] SET RECOVERY FULL 
GO
ALTER DATABASE [WMS] SET  MULTI_USER 
GO
ALTER DATABASE [WMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WMS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [WMS]
GO
/****** Object:  StoredProcedure [dbo].[GetProductProc]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetProductProc]
(
@price decimal,              
@productName nvarchar(50)    
)                              
as
BEGIN

--不返回受影响的行数
    SET NOCOUNT ON;
 --SET XACT_ABORT ON是设置事务回滚的！
--当为ON时，如果你存储中的某个地方出了问题，整个事务中的语句都会回滚
--为OFF时，只回滚错误的地方
	SET XACT_ABORT ON;

	begin try 
	begin Tran  
	select [ID]
      ,[GUID]
      ,[StockID]
      ,[BarCodeID]
      ,[SkuID]
      ,[ProductName]
      ,[ProductStyle]
      ,[Price]
      ,[CreateTime]
      ,[Status]
      ,[Count]
      ,[ModifyTime]
	  ,[TimeStamp]  from [dbo].[Product] where ProductName like '%'+ @productName+'%' and Price>@price
	 commit tran 
	end try
	begin catch
		ROLLBACK  Tran
		--DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
  --      SELECT @ErrMsg = ERROR_MESSAGE(),
  --             @ErrSeverity = ERROR_SEVERITY()
		--RAISERROR (@ErrMsg, @ErrSeverity, 1)


	end catch
   SET NOCOUNT OFF;
end
GO
/****** Object:  StoredProcedure [dbo].[pageData]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[pageData]
(
@pageIndex int,
@pageSize int,
@totalCount int =0 OUTPUT         
)                              
as
BEGIN
	begin try 
		begin Tran  
              SELECT @totalCount = COUNT(ID)   from [dbo].[Product];
			  --select @totalCount TotalCount;
			  SELECT [ID] ,[GUID] ,[StockID] ,[BarCodeID] ,[SkuID] ,[ProductName],[ProductStyle]
                    ,[Price] ,[CreateTime],[Status] ,[Count],[ModifyTime] ,[TimeStamp]
              FROM [WMS].[dbo].[Product]
			       --order  by 1
				   order  by ID
				   OFFSET (@pageIndex-1)*@pageSize ROWS
				   FETCH NEXT @pageSize ROWS ONLY
		commit tran 
	end try
	begin catch
		ROLLBACK  Tran
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductOutParamProc]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateProductOutParamProc]
(
@productName nvarchar(100),
@id int,
@result int =0 OUTPUT         
)                              
as
BEGIN

--不返回受影响的行数
    SET NOCOUNT ON;
 --SET XACT_ABORT ON是设置事务回滚的！
--当为ON时，如果你存储中的某个地方出了问题，整个事务中的语句都会回滚
--为OFF时，只回滚错误的地方
	SET XACT_ABORT ON;

	begin try 
		begin Tran  
			 update [dbo].[Product] set ProductName=@productName where ID=@id
			 commit tran 
		SET @result=1
	end try
	begin catch
		ROLLBACK  Tran
		SET @result=0
	end catch
	SET NOCOUNT OFF;
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductProc]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateProductProc]
(
@productName nvarchar(100),
@id int          
)                              
as
BEGIN

--不返回受影响的行数
    SET NOCOUNT ON;
 --SET XACT_ABORT ON是设置事务回滚的！
--当为ON时，如果你存储中的某个地方出了问题，整个事务中的语句都会回滚
--为OFF时，只回滚错误的地方
	SET XACT_ABORT ON;

	begin try 
		begin Tran  
			update [dbo].[Product] set ProductName=@productName where ID=@id
		 commit tran 
		 --注意return 提交事务之后
		 return 1
	end try
	begin catch
		ROLLBACK  Tran
		--DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
  --      SELECT @ErrMsg = ERROR_MESSAGE(),
  --             @ErrSeverity = ERROR_SEVERITY()
		--RAISERROR (@ErrMsg, @ErrSeverity, 1)

		return 0


	end catch
 SET NOCOUNT OFF;
end
GO
/****** Object:  Table [dbo].[BarCode]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BarCode](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](100) NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_BarCode] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Check]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Check](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_Check] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InOutStock]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InOutStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Type] [smallint] NOT NULL,
 CONSTRAINT [PK_InOutStock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[OrderNumber] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[OrderType] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[DealPrice] [decimal](18, 0) NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[StockID] [int] NULL,
	[BarCodeID] [int] NULL,
	[SkuID] [int] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[ProductStyle] [nvarchar](100) NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Status] [smallint] NOT NULL,
	[Count] [int] NOT NULL,
	[ModifyTime] [datetime] NULL,
	[TimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sku]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sku](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[Unit] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Sku] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stock]    Script Date: 2019-6-18 16:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[StockName] [nvarchar](100) NOT NULL,
	[Location] [nvarchar](500) NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([ID], [GUID], [OrderNumber], [CreateTime], [OrderType], [Status]) VALUES (2, N'a9ac5081-ce24-49c6-b3cc-19671ca36fda', N'123', CAST(0x0000A9AB00E9341E AS DateTime), 1, 1)
INSERT [dbo].[Order] ([ID], [GUID], [OrderNumber], [CreateTime], [OrderType], [Status]) VALUES (12, N'006f6b6e-831e-4311-a9ab-f20f35339dd1', N'sddssdsdsdsd', CAST(0x0000A9B40104DC1B AS DateTime), 1, 1)
INSERT [dbo].[Order] ([ID], [GUID], [OrderNumber], [CreateTime], [OrderType], [Status]) VALUES (17, N'09b00607-45b5-44bb-ab11-600f7db09d39', N'Test2018', CAST(0x0000A9B500FACAD1 AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([ID], [GUID], [OrderID], [ProductID], [Count], [DealPrice], [Status]) VALUES (2, N'0011d7d5-c770-4216-9077-ac7582ce9e4e', 2, 45, 2, CAST(3 AS Decimal(18, 0)), 1)
INSERT [dbo].[OrderDetail] ([ID], [GUID], [OrderID], [ProductID], [Count], [DealPrice], [Status]) VALUES (10, N'6d8b67cf-1c40-47f3-a3c7-612138e4b239', 12, 50, 10, CAST(13 AS Decimal(18, 0)), 1)
INSERT [dbo].[OrderDetail] ([ID], [GUID], [OrderID], [ProductID], [Count], [DealPrice], [Status]) VALUES (11, N'95d8fb48-71c3-4466-b9c9-875682e792e1', 12, 59, 19, CAST(12 AS Decimal(18, 0)), 1)
INSERT [dbo].[OrderDetail] ([ID], [GUID], [OrderID], [ProductID], [Count], [DealPrice], [Status]) VALUES (18, N'a09ab2cc-5619-470a-ad18-6abdbf1dc01e', 17, 50, 27, CAST(3 AS Decimal(18, 0)), 1)
INSERT [dbo].[OrderDetail] ([ID], [GUID], [OrderID], [ProductID], [Count], [DealPrice], [Status]) VALUES (19, N'7117b064-b749-4ff7-847b-a8bb644189d2', 17, 59, 7, CAST(7 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (48, N'22ba63f5-ad30-4738-9b2f-f30d036786af', 1, NULL, 1, N'uplockTest', NULL, CAST(10 AS Decimal(18, 0)), CAST(0x0000A9AB00EDFFC8 AS DateTime), 1, 1, CAST(0x0000A9AB00EDFFC8 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (49, N'c5c52ab7-3ab1-4dd0-8d62-f13c747e9199', 1, NULL, 1, N'fancky123', NULL, CAST(13 AS Decimal(18, 0)), CAST(0x0000A9AB00EDFFC8 AS DateTime), 1, 1, CAST(0x0000A9AB00EDFFC8 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (50, N'3b7439c2-ae32-4dd3-aeca-f45b7424d72a', 1, NULL, 1, N'uplockTest11', NULL, CAST(13 AS Decimal(18, 0)), CAST(0x0000A9AB00EDFFC8 AS DateTime), 1, 1, CAST(0x0000A9AB00EDFFC8 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (59, N'd0e00788-d656-47e2-bf0f-75c17c687844', 1, NULL, 1, N'uplockTest', N'12222', CAST(123 AS Decimal(18, 0)), CAST(0x0000A9AE00000000 AS DateTime), 1, 12, CAST(0x0000AA7000E4978F AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (60, N'd30eee84-d88f-484a-aaf6-bcd4169cfcc6', 1, NULL, 1, N'uplockTest', N'', CAST(123 AS Decimal(18, 0)), CAST(0x0000A9AE00FB437E AS DateTime), 1, 12, CAST(0x0000AA7000E5B12E AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (61, N'2089e7dc-bcc4-4681-8c2d-a1f47d3dd776', 1, NULL, 1, N'uplockTest', NULL, CAST(66 AS Decimal(18, 0)), CAST(0x0000A9AE00FD104E AS DateTime), 1, 12, CAST(0x0000A9AE00FD104E AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1056, N'0e65d063-c79a-4f12-b163-5d2f89835932', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E000F842DF AS DateTime), 1, 12, CAST(0x0000A9E000F842DF AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1058, N'16eb253d-e110-489e-8e7a-ac68118343cb', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E000F9DC20 AS DateTime), 1, 12, CAST(0x0000A9E000F9DC20 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1068, N'4ff4ca43-ca26-427f-9773-8e2f4766e904', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E000FEA9C4 AS DateTime), 1, 12, CAST(0x0000A9E000FEA9C4 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1069, N'5eec0bee-6062-45a5-a5ce-d689012a144e', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E000FFE690 AS DateTime), 1, 12, CAST(0x0000A9E000FFE690 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1072, N'a30cc4d5-4822-43cf-9387-7be081f15971', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00103AB8E AS DateTime), 1, 12, CAST(0x0000A9E00103AB8E AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1073, N'c6c5e06f-0a50-4fe2-9def-aee48a660d33', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00103BDBE AS DateTime), 1, 12, CAST(0x0000A9E00103BDBE AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1075, N'bc04ba47-676e-4835-a643-847a1aa5259a', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E001052653 AS DateTime), 1, 12, CAST(0x0000A9E001052653 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1076, N'4f282e46-ded9-4037-bfdb-9704615960a2', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E001058E49 AS DateTime), 1, 12, CAST(0x0000A9E001058E49 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1077, N'a9310e12-02da-49ba-8b94-7844fadc286a', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00105991E AS DateTime), 1, 12, CAST(0x0000A9E00105991E AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1078, N'4cb4a85a-2c6c-42ec-934c-bd1c6de391b3', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00105ADE8 AS DateTime), 1, 12, CAST(0x0000A9E00105ADE8 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1079, N'18e786c4-7390-42a4-84df-282e2a0cd12b', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00105D8D0 AS DateTime), 1, 12, CAST(0x0000A9E00105D8D0 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1080, N'859d2567-47b7-459f-80d9-ded0ded6564d', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E001064A5F AS DateTime), 1, 12, CAST(0x0000A9E001064A5F AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1081, N'15819143-832b-43af-87ab-8d648d221dea', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E001079C65 AS DateTime), 1, 12, CAST(0x0000A9E001079C65 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1082, N'5791eecd-0c3b-45ec-92d3-2743fc99afd4', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00107B555 AS DateTime), 1, 12, CAST(0x0000A9E00107B555 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1087, N'71e75d18-c9d4-4389-9ce3-2c4135c633b0', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E001117892 AS DateTime), 1, 12, CAST(0x0000A9E001117893 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1088, N'0c84e321-928b-4bd3-9040-1e838715d3a9', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00112C5E6 AS DateTime), 1, 12, CAST(0x0000A9E00112C5E6 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1089, N'95745e75-3587-42a8-95cf-d99314f7edd4', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00112E1E8 AS DateTime), 1, 12, CAST(0x0000A9E00112E1E8 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1090, N'03c735de-178f-4917-b736-1c26742ffe66', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E001130C37 AS DateTime), 1, 12, CAST(0x0000A9E001130C37 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1091, N'5fc3a6a5-ebc8-412e-bb28-65731008fd3f', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00117B84E AS DateTime), 1, 12, CAST(0x0000A9E00117B84E AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1092, N'b4ef5572-1938-4dea-a1e0-b3f90ce1b48f', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E00117C846 AS DateTime), 1, 12, CAST(0x0000A9E00117C846 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1095, N'bf5f425c-718c-4179-88a1-dfbaded0d9a1', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000A9E3011363D6 AS DateTime), 1, 12, CAST(0x0000A9E3011363D6 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1096, N'ddbbb464-55dc-4bf1-bb59-ca1b4302e0f6', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600BC491E AS DateTime), 1, 12, CAST(0x0000AA1600BC491E AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1097, N'd2306f5e-b90d-4771-84c9-194988345c79', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600BD07BF AS DateTime), 1, 12, CAST(0x0000AA1600BD07BF AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1098, N'685633d2-8e5d-4ef8-96f3-e113ea4a6859', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600BD7AD1 AS DateTime), 1, 12, CAST(0x0000AA1600BD7AD1 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1099, N'a03080c9-7f60-48ef-8938-358eb696e24d', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600C60761 AS DateTime), 1, 12, CAST(0x0000AA1600C60761 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1100, N'ae87cf0c-5dc5-49ba-a70b-409d4b36d653', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600C64C72 AS DateTime), 1, 12, CAST(0x0000AA1600C64C72 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1101, N'996db5f0-4cb8-4c9f-9f08-a4239d701950', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600C69DE8 AS DateTime), 1, 12, CAST(0x0000AA1600C69DE8 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1102, N'e28e14ad-bbbb-4fb8-8306-713c693af437', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600C715ED AS DateTime), 1, 12, CAST(0x0000AA1600C715ED AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (1103, N'9618b7c8-1dc7-4c4b-82ce-186730dc9828', 1, NULL, 1, N'uplockTest', NULL, CAST(123 AS Decimal(18, 0)), CAST(0x0000AA1600C730A0 AS DateTime), 1, 12, CAST(0x0000AA1600C730A0 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2096, N'1a21f2c2-b30f-47b9-a03d-9fccadb49930', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D00CC0190 AS DateTime), 1, 1, CAST(0x0000AA1D00CC0190 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2097, N'a37eb266-24f6-4349-9ad3-428a5fabf4ec', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D00CC163C AS DateTime), 1, 1, CAST(0x0000AA1D00CC163C AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2098, N'41841a07-42a0-49a2-a2c3-c57c59bdf3b7', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D00CC47F9 AS DateTime), 1, 1, CAST(0x0000AA1D00CC47F9 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2099, N'76a71a46-6266-4305-842c-fbcaa0db84b3', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D00CC92D3 AS DateTime), 1, 1, CAST(0x0000AA1D00CC92D3 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2100, N'54a5d58a-3504-46b1-92ab-7e122bc80eb2', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D00CCF336 AS DateTime), 1, 1, CAST(0x0000AA1D00CCF336 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2101, N'd9c2dc89-56fc-454f-8440-b0c250ea7e98', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D00E3099A AS DateTime), 1, 1, CAST(0x0000AA1D00E3099A AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2102, N'29753a28-f428-475c-8177-ec8ec097cd74', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D00E645AF AS DateTime), 1, 1, CAST(0x0000AA1D00E645AF AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2107, N'2192b207-bba9-45dc-a26e-1f207319f9b9', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D01243EBA AS DateTime), 1, 1, CAST(0x0000AA1D01243EBA AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2108, N'e7deed1f-1d8e-48ce-95be-b1fba458859a', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D0124B6B2 AS DateTime), 1, 1, CAST(0x0000AA1D0124B6B2 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2113, N'46268ebe-318a-4bb5-8371-e5354a6e9286', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D012DFCCB AS DateTime), 1, 1, CAST(0x0000AA1D012DFCCB AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2114, N'78a0ab75-8901-4d54-b102-bda15dce59e3', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D012DFF95 AS DateTime), 1, 1, CAST(0x0000AA1D012DFF95 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2115, N'83cb76ca-b598-423e-a9f0-2929c0b86803', 1, NULL, 1, N'ProductName', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1D012E0E62 AS DateTime), 1, 1, CAST(0x0000AA1D012E0E62 AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2119, N'5a2b899e-c8fe-4811-9a38-03c93bbde0c8', 1, NULL, 1, N'ProductNameSuccess', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1F00AA18DE AS DateTime), 1, 1, CAST(0x0000AA1F00AA18DE AS DateTime))
INSERT [dbo].[Product] ([ID], [GUID], [StockID], [BarCodeID], [SkuID], [ProductName], [ProductStyle], [Price], [CreateTime], [Status], [Count], [ModifyTime]) VALUES (2120, N'648629c8-5b1c-413a-9638-28b4732ddcd2', 1, NULL, 1, N'ProductNameFail', NULL, CAST(11 AS Decimal(18, 0)), CAST(0x0000AA1F00AA1900 AS DateTime), 1, 1, CAST(0x0000AA1F00AA1900 AS DateTime))
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Sku] ON 

INSERT [dbo].[Sku] ([ID], [GUID], [Unit]) VALUES (1, N'00000000-0000-0000-0000-000000000000', N'包')
INSERT [dbo].[Sku] ([ID], [GUID], [Unit]) VALUES (2, N'00000000-0000-0000-0000-000000000000', N'11')
SET IDENTITY_INSERT [dbo].[Sku] OFF
SET IDENTITY_INSERT [dbo].[Stock] ON 

INSERT [dbo].[Stock] ([ID], [GUID], [StockName], [Location], [Status]) VALUES (1, N'6c8402a7-59a6-4142-a918-9e571a863efe', N'1', N'1', 0)
INSERT [dbo].[Stock] ([ID], [GUID], [StockName], [Location], [Status]) VALUES (2, N'121ed45c-57f0-4f0d-86f5-dbb6b8b5b6f4', N'2', N'32222222222222222222222222222222222222222222222222222222222222222222222222222222222222', 0)
SET IDENTITY_INSERT [dbo].[Stock] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [productName_Product_index]    Script Date: 2019-6-18 16:49:28 ******/
CREATE NONCLUSTERED INDEX [productName_Product_index] ON [dbo].[Product]
(
	[ProductName] ASC,
	[CreateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BarCode] ADD  CONSTRAINT [DF_BarCode_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Check] ADD  CONSTRAINT [DF_Check_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[Check] ADD  CONSTRAINT [DF_Check_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Check] ADD  CONSTRAINT [DF_Check_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[InOutStock] ADD  CONSTRAINT [DF_InOutStock_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[InOutStock] ADD  CONSTRAINT [DF_InOutStock_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[InOutStock] ADD  CONSTRAINT [DF_InOutStock_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Count]  DEFAULT ((0)) FOR [Count]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ModifyTime]  DEFAULT (getdate()) FOR [ModifyTime]
GO
ALTER TABLE [dbo].[Sku] ADD  CONSTRAINT [DF_Sku_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[Stock] ADD  CONSTRAINT [DF_Stock_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Check]  WITH NOCHECK ADD  CONSTRAINT [FK_Check_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Check] NOCHECK CONSTRAINT [FK_Check_Product]
GO
ALTER TABLE [dbo].[InOutStock]  WITH NOCHECK ADD  CONSTRAINT [FK_InOutStock_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[InOutStock] NOCHECK CONSTRAINT [FK_InOutStock_Product]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] NOCHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] NOCHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH NOCHECK ADD  CONSTRAINT [FK_Product_BarCode] FOREIGN KEY([BarCodeID])
REFERENCES [dbo].[BarCode] ([ID])
GO
ALTER TABLE [dbo].[Product] NOCHECK CONSTRAINT [FK_Product_BarCode]
GO
ALTER TABLE [dbo].[Product]  WITH NOCHECK ADD  CONSTRAINT [FK_Product_Sku] FOREIGN KEY([SkuID])
REFERENCES [dbo].[Sku] ([ID])
GO
ALTER TABLE [dbo].[Product] NOCHECK CONSTRAINT [FK_Product_Sku]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Stock]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0、删除 1、正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BarCode', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0、删除 1、正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Check', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盘点表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Check'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0、删除 1、正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InOutStock', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1、入库 2、出库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InOutStock', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出入库表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InOutStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1、销售单、2、退货单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0、删除 1、正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0、删除 1、正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderDetail', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0、删除 1、正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sku'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0、删除 1、正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stock', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库位表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stock'
GO
USE [master]
GO
ALTER DATABASE [WMS] SET  READ_WRITE 
GO
