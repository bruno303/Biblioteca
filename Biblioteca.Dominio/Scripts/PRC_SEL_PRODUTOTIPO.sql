SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bruno Oliveira
-- Create date: 09/09/2018
-- Description:	Seleciona informações do tipo de produto.
-- =============================================
CREATE PROCEDURE dbo.PRC_SEL_PRODUTOTIPO
	@I_ID_PRODUTOTIPO INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT PT.ID_PRODUTO_TIPO, PT.DESCRICAO, PT.PRAZO, PT.VL_MULTA, PT.ATIVO
	FROM PRODUTO_TIPO PT WITH(NOLOCK)
	WHERE ID_PRODUTO_TIPO = @I_ID_PRODUTOTIPO;

	SET NOCOUNT OFF;
END
GO
