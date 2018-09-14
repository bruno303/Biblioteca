USE [BIBLIOTECA]
GO
/****** Object:  StoredProcedure [dbo].[PRC_SEL_PRODUTO]    Script Date: 9/11/2018 4:31:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bruno Oliveira
-- Create date: 11/09/2018
-- Description:	Retorna produto cadastrado por id.
-- =============================================
CREATE PROCEDURE [dbo].[PRC_SEL_PRODUTO]
	@I_ID_PRODUTO INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT TOP(1) ID_PRODUTO FROM PRODUTO WITH(NOLOCK) WHERE ID_PRODUTO = @I_ID_PRODUTO) BEGIN

		SELECT ID_PRODUTO, DESCRICAO, ID_PRODUTO_TIPO, ID_AUTOR,
			ID_EDITORA, QUANTIDADE, ATIVO
		FROM PRODUTO WITH(NOLOCK)
		WHERE ID_PRODUTO = @I_ID_PRODUTO

	END
	ELSE BEGIN

		SELECT ID_PRODUTO, DESCRICAO, ID_PRODUTO_TIPO, ID_AUTOR,
			ID_EDITORA, QUANTIDADE, ATIVO
		FROM PRODUTO WITH(NOLOCK)

	END

	SET NOCOUNT OFF;
END
