SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bruno Oliveira
-- Create date: 09/09/2018
-- Description:	Seleciona as editoras
-- =============================================
CREATE PROCEDURE dbo.PRC_SEL_EDITORA
	@I_ID_EDITORA INT = 0
AS
BEGIN
	SET NOCOUNT ON;

	IF (COALESCE(@I_ID_EDITORA, 0) = 0) BEGIN

		SELECT ID_EDITORA, NOME
		FROM EDITORA WITH(NOLOCK)

	END
	ELSE BEGIN

		SELECT ID_EDITORA, NOME
		FROM EDITORA WITH(NOLOCK)
		WHERE ID_EDITORA = @I_ID_EDITORA

	END

	SET NOCOUNT OFF;
END
GO
