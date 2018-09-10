SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bruno Oliveira
-- Create date: 07/09/2018
-- Description:	Retorna usuário com login e senha especificados, se houver
-- =============================================
CREATE PROCEDURE dbo.PRC_SEL_USUARIO
	@LOGIN VARCHAR(20),
	@SENHA VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT ID_USUARIO
	FROM USUARIO WITH(NOLOCK)
	WHERE LOGIN = UPPER(@LOGIN) AND SENHA = @SENHA

	SET NOCOUNT OFF;
END
GO
