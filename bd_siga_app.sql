create database siga_app;
use siga_app;

create table professor (
    id_pro int auto_increment primary key,
    nome_pro varchar(150) not null,
    cpf_pro varchar(14) unique,
    email_pro varchar(120),
    telefone_pro varchar(20),
    disciplina_pro varchar(100),
    status_pro varchar(10) default 'ativo',   
    data_cadastro_pro date,
    especialidade_pro varchar(100)
);

create table turma (
    id_tur int auto_increment primary key,
    nome_tur varchar(100),
    ano_tur varchar(100),    
    periodo_letivo_tur varchar(5),
    turno_tur varchar(50),
    status_tur varchar(10) default 'ativo',
    capacidade_maxima_tur int
);


create table professor_turma(
	id_ptu int auto_increment primary key,
    id_pro_fk int null,
    id_tur_fk int null,
    
    foreign key (id_pro_fk) references professor(id_pro) on delete set null,
    foreign key (id_tur_fk) references turma(id_tur) on delete set null
);


create table estudante(
	id_est int auto_increment primary key,
	nome_est varchar(300) not null,
	idade_est int,
    sexo_est varchar(20),
    data_nasc_est date,    
    telefone_est varchar(50),
    nome_resp_1_est varchar(300) not null,
    nome_resp_2_est varchar(300),
    situacao_est varchar(50) default 'cursando',
	cidade_est varchar(200),
	#uf = estado abreviado
    uf_est varchar(2),
    rua_est varchar(200),
    numero_est varchar(20),
    bairro_est varchar(50),
    id_tur_fk int null,
	foreign key (id_tur_fk) references turma(id_tur) on delete set null
);

# inserts
insert into professor 
(nome_pro, cpf_pro, email_pro, telefone_pro, disciplina_pro, status_pro, data_cadastro_pro, especialidade_pro)
values 
('joão silva', '123.456.789-00', 'joao.silva@email.com', '69999990001', 'matemática', 'ativo', '2025-11-24', 'álgebra');

insert into professor 
(nome_pro, cpf_pro, email_pro, telefone_pro, disciplina_pro, status_pro, data_cadastro_pro, especialidade_pro)
values 
('maria souza', '987.654.321-00', 'maria.souza@email.com', '69999990002', 'português', 'ativo', '2025-11-24', 'literatura brasileira');

insert into turma 
(nome_tur, ano_tur, periodo_letivo_tur, turno_tur, capacidade_maxima_tur)
values 
('turma alfa', '1° ano', '2025', 'matutino', 30);

insert into turma 
(nome_tur, ano_tur, periodo_letivo_tur, turno_tur, capacidade_maxima_tur)
values 
('turma beta', '2° ano', '2025', 'vespertino', 25);

insert into professor_turma 
(id_pro_fk, id_tur_fk)
values 
(1, 1);

insert into professor_turma 
(id_pro_fk, id_tur_fk)
values 
(2, 2);

insert into estudante 
(nome_est, idade_est, sexo_est, data_nasc_est, telefone_est, nome_resp_1_est, nome_resp_2_est, situacao_est, cidade_est, uf_est, rua_est, numero_est, bairro_est, id_tur_fk)
values 
('carlos pereira', 15, 'masculino', '2010-05-12', '69999990003', 'ana pereira', 'joão pereira', 'cursando', 'ouro preto do oeste', 'ro', 'rua das flores', '123', 'centro', 1);

insert into estudante 
(nome_est, idade_est, sexo_est, data_nasc_est, telefone_est, nome_resp_1_est, nome_resp_2_est, situacao_est, cidade_est, uf_est, rua_est, numero_est, bairro_est, id_tur_fk)
values 
('juliana santos', 14, 'feminino', '2011-08-20', '69999990004', 'paulo santos', 'marta santos', 'cursando', 'ji-paraná', 'ro', 'avenida brasil', '456', 'bairro novo', 2);

select * from turma;
select * from estudante;