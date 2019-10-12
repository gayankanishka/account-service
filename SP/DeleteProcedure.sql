SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [PlayGround]
GO
-- =======================================================
-- Author:		Gayan Kanishka Wijetunga
-- Create date: 2019/10/12
-- Description:	Deletes a AccountDto in the Accounts table
-- =======================================================
CREATE PROCEDURE Delete_Account
	-- Add the parameters for the stored procedure here
	@Id As int
AS
BEGIN
    -- Insert statements for procedure here
	DELETE FROM [dbo].[Accounts]
		WHERE [Id] = @Id
END
GO
