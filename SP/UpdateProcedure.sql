SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [PlayGround]
GO
-- =======================================================
-- Author:		Gayan Kanishka Wijetunga
-- Create date: 2019/10/12
-- Description:	Updates a AccountDto in the Accounts table
-- =======================================================
CREATE PROCEDURE Update_Account
	-- Add the parameters for the stored procedure here
	@Id As bigint,
	@Name AS varchar(50),
	@Email AS varchar(50)
AS
BEGIN
    -- Insert statements for procedure here
	UPDATE [dbo].[Accounts]
	SET [Name] = @Name,
		[Email] = @Email
	WHERE [Id] = @Id
END
GO
