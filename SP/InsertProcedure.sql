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
CREATE PROCEDURE Insert_Account
	-- Add the parameters for the stored procedure here
	@Name AS varchar(50),
	@Email AS varchar(50)
AS
BEGIN
    -- Insert statements for procedure here
	INSERT INTO [dbo].[Accounts]([Name], [Email])
    VALUES (@Name, @Email)
END
GO
