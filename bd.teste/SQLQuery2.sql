select * from Categoria;
select * from Localizacao;
select * from Evento;
select * from Tipo_Usuario;
select * from Usuario;
select * from Presenca;

SELECT	
	Evento.titulo,
	Usuario.nome,
	Categoria.titulo
FROM Presenca 
	INNER JOIN Evento ON Evento.evento_id = Presenca.presenca_id 
	INNER JOIN Usuario ON Usuario.usuario_id = Presenca.presenca_id
	INNER JOIN Categoria ON Categoria.categoria_id = Presenca.presenca_id; 