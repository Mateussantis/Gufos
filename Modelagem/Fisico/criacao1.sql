create database Gufos;

use Gufos;

create table Tipo_usuario(
	tipo_usuario_id int identity primary key,
	titulo varchar(255) unique not null
);

create table Usuario(
	usuario_id int identity primary key,
	nome varchar(255) not null,
	email varchar(255) unique not null,
	senha varchar(255) not null,
	tipo_usuario_id int foreign key references Tipo_usuario(tipo_usuario_id)
);

create table Localizacao(
	localizacao_id int identity primary key,
	cnpj varchar(14) unique not null,
	razao_social varchar(255) unique not null ,
	endereco varchar(255) not null
);

create table Categoria(
	categoria_id int identity primary key,
	titulo varchar(255) unique not null
);

create table Evento(
	evento_id int identity primary key,
	titulo varchar(255) unique not null,
	categoria_id int foreign key references Categoria(categoria_id),
	acesso_livro bit default(1) not null,
	data_evento datetime  not null,
	localizacao_id int foreign key references Localizacao(localizacao_id)
);

create table Presenca(
	presenca_id int identity primary key,
	evento_id int foreign key references Evento(evento_id),
	usuario_id int foreign key references Usuario(usuario_id),
	presenca_status  varchar(255) not null
);










