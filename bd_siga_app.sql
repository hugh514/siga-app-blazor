create database siga_app;
use siga_app;

create table professor (
    id_pro int auto_increment primary key,
    nome_pro varchar(150) not null,
    cpf_pro varchar(14) unique,
    email_pro varchar(120),
    telefone_pro varchar(20),
    disciplina_pro varchar(100),
    status_pro varchar(50) default 'Ativo',   
    data_cadastro_pro date,
    especialidade_pro varchar(100)
);



create table turma (
    id_tur int auto_increment primary key,
    nome_tur varchar(100),
    ano_tur varchar(100),    
    turno_tur varchar(50),
    capacidade_maxima_tur int
);


create table professor_turma(
	id_ptu int auto_increment primary key,
    id_pro_fk int not null,
    id_tur_fk int not null,
    
    foreign key (id_pro_fk) references professor(id_pro),
    foreign key (id_tur_fk) references turma(id_tur)
);

create table endereco(
	id_end int auto_increment primary key,
	cidade_end varchar(200),
	#uf = estado abreviado
    uf_end varchar(2),
    rua_end varchar(200),
    numero_end varchar(20),
    bairro_end varchar(50)
);

create table estudante(
	id_est int auto_increment primary key,
	nome_est varchar(300) not null,
	idade_est int,
    sexo_est varchar(20),
    data_nasc_est date,    
    telefone_est varchar(50),
    nome_pai_ou_resp_est varchar(300) not null,
    nome_mae_est varchar(300),
    situacao_est varchar(50) default 'cursando',
    id_end_fk int,
    id_tur_fk int,
    foreign key (id_end_fk) references endereco(id_end),
    foreign key (id_tur_fk) references turma(id_tur)
);


# -------------------------inserts
-- professor
insert into professor (nome_pro, cpf_pro, email_pro, telefone_pro, disciplina_pro, data_cadastro_pro, especialidade_pro)
values ('joão silva', '123.456.789-00', 'joao.silva@email.com', '(69) 99999-1111', 'matemática', '2025-01-10', 'álgebra');

insert into professor (nome_pro, cpf_pro, email_pro, telefone_pro, disciplina_pro, data_cadastro_pro, especialidade_pro)
values ('maria souza', '987.654.321-00', 'maria.souza@email.com', '(69) 98888-2222', 'história', '2025-01-12', 'história antiga');

-- turma
insert into turma (nome_tur, ano_tur, turno_tur, capacidade_maxima_tur)
values ('turma a', '1º ano', 'matutino', 40);

insert into turma (nome_tur, ano_tur, turno_tur, capacidade_maxima_tur)
values ('turma b', '2º ano', 'vespertino', 35);

-- professor_turma
insert into professor_turma (id_pro_fk, id_tur_fk)
values (1, 1);

insert into professor_turma (id_pro_fk, id_tur_fk)
values (2, 2);

-- endereco
insert into endereco (cidade_end, uf_end, rua_end, numero_end, bairro_end)
values ('ouro preto do oeste', 'ro', 'rua das flores', '123', 'centro');

insert into endereco (cidade_end, uf_end, rua_end, numero_end, bairro_end)
values ('ji-paraná', 'ro', 'av. brasil', '456', 'nova brasília');

-- estudante
insert into estudante (nome_est, idade_est, sexo_est, data_nasc_est, telefone_est, nome_pai_ou_resp_est, nome_mae_est, id_end_fk, id_tur_fk)
values ('carlos pereira', 15, 'masculino', '2010-05-20', '(69) 97777-3333', 'josé pereira', 'ana pereira', 1, 1);

insert into estudante (nome_est, idade_est, sexo_est, data_nasc_est, telefone_est, nome_pai_ou_resp_est, nome_mae_est, id_end_fk, id_tur_fk)
values ('fernanda lima', 16, 'feminino', '2009-08-14', '(69) 96666-4444', 'roberto lima', 'cláudia lima', 2, 2);

