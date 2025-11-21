create database siga_app;
use siga_app;

create table professor (
    id_pro int auto_increment primary key,
    nome_pro varchar(150) not null,
    cpf_pro varchar(14) unique,
    email_pro varchar(120),
    telefone_pro varchar(20),
    disciplina_pro varchar(100),
    status_pro varchar(100) default 'Ativo',   
    data_cadastro_pro DATE,
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

insert into professor 
(nome_pro, cpf_pro, email_pro, telefone_pro, disciplina_pro, status_pro, data_cadastro_pro, especialidade_pro)
values
('Carlos Henrique Silva', '123.456.789-00', 'carlos.silva@ifro.edu.br', '(69) 99912-3344', 'Matemática', 'Ativo', '2025-10-14', 'Geometria Analítica'),

('Fernanda Costa Oliveira', '987.654.321-00', 'fernanda.oliveira@ifro.edu.br', '(69) 99213-4455', 'Português', 'Ativo', '2025-10-14', 'Gramática e Redação'),

('Roberto Nunes Almeida', '654.321.987-00', 'roberto.almeida@ifro.edu.br', '(69) 99321-5566', 'Física', 'Ativo', '2025-10-14', 'Mecânica e Termodinâmica'),

('Juliana Pereira Santos', '321.987.654-00', 'juliana.santos@ifro.edu.br', '(69) 99432-6677', 'Química', 'Ativo', '2025-10-14', 'Química Orgânica'),

('André Luiz Souza', '741.852.963-00', 'andre.souza@ifro.edu.br', '(69) 99543-7788', 'Informática', 'Ativo', '2025-10-14', 'Desenvolvimento Web');
