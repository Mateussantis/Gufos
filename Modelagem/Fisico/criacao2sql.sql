insert into Tipo_usuario (titulo) values ('Adm') , ('Aluno');

insert into Usuario (nome,email,senha,tipo_usuario_id) values ('Administrador', 'adm@adm.com', '123', 1) , ('Ariel', 'ariel@gmail.com', '123', 2);

insert into Localizacao(cnpj,razao_social, endereco ) values ('1234567', 'Escola Senai Informatica', 'Barao Limeira, 509');

insert into Categoria(titulo) values ('C#');

insert into Evento(titulo, categoria_id, acesso_livro, data_evento, localizacao_id) values ('Logica', 3, 1, GETDATE(), 15);

select * from Evento;

insert into Presenca(evento_id, usuario_id, presenca_status) values (1, 1, 'Aguardando') ;