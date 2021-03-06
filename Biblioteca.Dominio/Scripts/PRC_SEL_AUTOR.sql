SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bruno Oliveira
-- Create date: 09/09/2018
-- Description:	Seleciona os dados de autor.
-- =============================================
CREATE PROCEDURE dbo.PRC_SEL_AUTOR
	@I_ID_AUTOR INT = 0
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS (SELECT TOP(1) * FROM AUTOR WITH(NOLOCK) WHERE ID_AUTOR = @I_ID_AUTOR) BEGIN

		SELECT ID_AUTOR, NOME, DT_NASCIMENTO
		FROM AUTOR WITH(NOLOCK)
		WHERE ID_AUTOR = @I_ID_AUTOR

	END
	ELSE BEGIN

		SELECT ID_AUTOR, NOME, DT_NASCIMENTO
		FROM AUTOR WITH(NOLOCK)

	END

	SET NOCOUNT OFF;
END
GO
