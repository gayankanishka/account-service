SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [PlayGround]
GO
-- =======================================================
-- Author:		Gayan Kanishka Wijetunga
-- Create date: 2019/10/12
-- Description:	Inserts a AccountDto into Accounts table
-- =======================================================
CREATE PROCEDURE Select_Single
	@Id AS int
AS
BEGIN
    SELECT * FROM [dbo].[Accounts]
	WHERE [Id] = @Id
END
GO